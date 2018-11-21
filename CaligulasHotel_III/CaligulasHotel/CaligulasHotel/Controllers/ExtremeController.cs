using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace CaligulasHotel.Controllers
{
    public class ExtremeController : Controller
    {
        private readonly Models.ApplicationDbContext _db = new Models.ApplicationDbContext();

        // GET: Extreme
        public ActionResult Index()
        {
            return View(_db.Vehiculos.ToArray());
        }

        public ActionResult Detalle(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            var renta = _db.RentaVehiculos.Include(x => x.Vehiculo).Where(x => x.Vehiculo.VehiculoId.Equals(id)).FirstOrDefault();
            
            if (renta == null)
            {
                return HttpNotFound();
            }
            if (!renta.Habilitado)
            {
                var linearenta = _db.LineaRentaVehiculos.Where(x => x.RentaVehiculo.Id.Equals(renta.Id) && x.Activo).FirstOrDefault();
                ViewBag.minutos = (linearenta.FechaExpiracion - DateTime.Now).Minutes;
                ViewBag.horas = (linearenta.FechaExpiracion - DateTime.Now).Hours;
            }
            return View(renta);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RentalRegister(FormCollection collection)
        {
            int numerohoras = int.Parse(collection["NumeroHoras"]);
            string rentaid = collection["RentaPassId"];

            var user = GetUserLogIn();
            var renta = _db.RentaVehiculos.Where(x => x.Id.Equals(rentaid)).FirstOrDefault();

            if (renta == null)
            {
                return HttpNotFound();
            }
            var linearenta = new Models.Entity.LineaRentaVehiculoEntity
            {
                Id = Guid.NewGuid().ToString(),
                FechaRenta = DateTime.Now,
                FechaExpiracion = DateTime.Now.AddHours(numerohoras),
                NumeroHoras = numerohoras,
                Activo = true,
                User = user,
                RentaVehiculo = renta,
                TotalPago = renta.CostoHora * numerohoras
            };
            renta.Habilitado = false;

            _db.Entry(renta).State = EntityState.Modified;
            _db.LineaRentaVehiculos.Add(linearenta);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        #region utilitarios controller
        protected virtual Models.ApplicationUser GetUserLogIn()
        {
            return _db.Users.Where(x => x.UserName.Equals(User.Identity.Name)).FirstOrDefault();
        }
        #endregion
    }
}