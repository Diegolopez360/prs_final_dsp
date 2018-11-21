using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace CaligulasHotel.Controllers
{
    public class MarketController : Controller
    {
        private readonly Models.ApplicationDbContext _db = new Models.ApplicationDbContext();

        // GET: Market
        public ActionResult Index()
        {
            return View(_db.Categorias.Include(x => x.Articulos).Where(x => x.Articulos.Count > 0).ToArray());
        }

        public ActionResult Detalle(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            var articulo = _db.Articulos.Include(x => x.Marca).Include(x => x.Categoria).Where(x => x.ArticuloId.Equals(id)).FirstOrDefault();
            if (articulo == null)
            {
                return HttpNotFound();
            }
            return View(articulo);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult ItemShoppingCart(FormCollection collection)
        {
            int numeroarticulos = int.Parse(collection["NumeroArticulos"]);
            string articuloid = collection["ArticuloId"];

            var articulo = _db.Articulos.Where(x => x.ArticuloId.Equals(articuloid)).FirstOrDefault();
            var shoppingcart = GetCurrentShoppingCarByUserLog();
            if (articulo == null)
            {
                return HttpNotFound();
            }
            var lineashopcar = new Models.Entity.LineaShoppingCartEntity
            {
                Articulo = articulo,
                NumeroArticulos = numeroarticulos,
                ShoppingCart = shoppingcart,
                LineaId = Guid.NewGuid().ToString(),
                TotalPagar = articulo.PrecioUnitario * numeroarticulos,
                PrecioUnitario = articulo.PrecioUnitario,
                Cancelado = false,
                FechaAgregado = DateTime.Now,
                FechaCancelado = DateTime.Now.AddYears(-100)
            };
            articulo.Stock = articulo.Stock - numeroarticulos;

            _db.LineaShoppingCarts.Add(lineashopcar);
            _db.Entry(articulo).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        #region utilitario controller
        protected virtual Models.Entity.ShoppingCartEntity GetCurrentShoppingCarByUserLog()
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
            return shoppingcart;
        }
        #endregion
    }
}