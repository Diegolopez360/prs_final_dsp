using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace CaligulasHotel.Controllers
{
    public class HomeController : Controller
    {
        private readonly Models.ApplicationDbContext _db = new Models.ApplicationDbContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Evento()
        {
            return View();
        }

        [Authorize]
        public ActionResult Account()
        {
            var user = GetCurrentUser();
            var shoppingcart = _db.ShoppingCarts.Where(x => x.User.Id.Equals(user.Id)).FirstOrDefault();

            if (shoppingcart != null)
            {

            }
            else
            {

            }
            var model = new Models.ViewModel.AccountViewModel
            {
                LineaShoppings = _db.LineaShoppingCarts.Include(x => x.Articulo).Where(x => x.ShoppingCart.CartId.Equals(shoppingcart.CartId)).ToArray(),
                Habitaciones = _db.LineaHabitacionUsuarios.Include(x => x.Habitacion).Where(x => x.User.Id.Equals(user.Id)).ToArray(),
                RentaVehiculos = _db.LineaRentaVehiculos.Include(x => x.RentaVehiculo.Vehiculo).Where(x => x.User.Id.Equals(user.Id)).ToArray(),
                Facturas = _db.Facturas.Where(x => x.User.Id.Equals(user.Id)).ToArray()
            };
            return View(model);
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