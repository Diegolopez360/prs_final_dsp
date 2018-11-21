using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Threading.Tasks;

namespace CaligulasHotel.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly Models.ApplicationDbContext _db = new Models.ApplicationDbContext();

        // GET: Cart
        public ActionResult Index()
        {
            var user = GetCurrentUser();
            var cart = _db.ShoppingCarts.Where(x => x.User.Id.Equals(user.Id)).FirstOrDefault();
            var lineacart = _db.LineaShoppingCarts.Include(x => x.Articulo).Where(x => x.ShoppingCart.CartId.Equals(cart.CartId) && x.Cancelado == false).OrderByDescending(x => x.FechaAgregado).ToArray();
            return View(lineacart);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Checkout(string CartId)
        {
            var lineacarrito = _db.LineaShoppingCarts.Include(x => x.Articulo).Where(x => x.ShoppingCart.CartId.Equals(CartId) && x.Cancelado == false).ToArray();
            var user = GetCurrentUser();

            var factura = new Models.Entity.FacturaEntity
            {
                FacturaId = Guid.NewGuid().ToString(),
                Descuento = 0.0,
                NumeroArticulos = lineacarrito.Sum(x => x.NumeroArticulos),
                TotalPagar = lineacarrito.Sum(x => x.TotalPagar),
                FechaCreacion = DateTime.Now,
                NumeroTarjeta = user.CardNumber,
                User = user
            };

            List<Models.Entity.LineaFacturaEntity> lineaFacturas = new List<Models.Entity.LineaFacturaEntity>();
            foreach (var linea in lineacarrito)
            {
                lineaFacturas.Add(new Models.Entity.LineaFacturaEntity
                {
                    Id = Guid.NewGuid().ToString(),
                    Articulo = linea.Articulo,
                    NumeroArticulos = linea.NumeroArticulos,
                    Factura = factura
                });
            }
            lineacarrito.Where(x => x.Cancelado == false).ToList().ForEach(l => { l.Cancelado = true; l.FechaCancelado = DateTime.Now; });

            _db.Facturas.Add(factura);
            _db.LineaFacturas.AddRange(lineaFacturas);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<ActionResult> Delete(string ItemId)
        {
            var lineacarrito = await _db.LineaShoppingCarts.Include(x => x.Articulo).Where(x => x.LineaId.Equals(ItemId)).FirstOrDefaultAsync();
            var articulo = lineacarrito.Articulo;
            articulo.Stock = articulo.Stock + lineacarrito.NumeroArticulos;

            _db.LineaShoppingCarts.Remove(lineacarrito);
            _db.Entry(articulo).State = EntityState.Modified;

            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        #region utilitarios controller
        protected virtual Models.ApplicationUser GetCurrentUser()
        {
            var user = _db.Users.Where(x => x.UserName.Equals(User.Identity.Name)).FirstOrDefault();
            var shoppingcart = _db.ShoppingCarts.Where(x => x.User.Id.Equals(user.Id)).FirstOrDefault();
            if (shoppingcart == null)
            {
                shoppingcart = new Models.Entity.ShoppingCartEntity
                {
                    CartId = Guid.NewGuid().ToString(),
                    FechaCreacion = DateTime.Today,
                    MaxItems = 10,
                    User = user
                };

                _db.ShoppingCarts.Add(shoppingcart);
                _db.SaveChanges();
            }
            return user;
        }
        #endregion
    }
}