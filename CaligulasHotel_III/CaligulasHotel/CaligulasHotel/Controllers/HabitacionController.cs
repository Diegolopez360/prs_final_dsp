using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace CaligulasHotel.Controllers
{
    public class HabitacionController : Controller
    {
        private readonly Models.ApplicationDbContext _db = new Models.ApplicationDbContext();

        // GET: Habitacion
        public ActionResult Index()
        {
            return View(_db.Habitaciones.ToArray());
        }

        public ActionResult Detalle(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            var habitacion = _db.Habitaciones.Include(x => x.TipoHabitacion).Where(x => x.HabitacionId.Equals(id)).FirstOrDefault();

            if (habitacion == null)
            {
                return HttpNotFound();
            }
            return View(habitacion);
        }

        [Authorize]
        public ActionResult Reserva(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            var user = GetUserLogIn();
            var habitacion = GetHabitacionByHabitacionId(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            if (habitacion == null)
            {
                return HttpNotFound();
            }

            return View(new Models.ViewModel.ReservaHabitacionViewModel { FechaContrato = DateTime.Today, FechaVencimiento = DateTime.Today.AddDays(1), CardNumber = user.CardNumber, HabitacionId = habitacion.HabitacionId, ImageUrl = habitacion.ImageUrl, NombreHabitacion = habitacion.Nombre, PrecioHabitacion = habitacion.PrecioNoche });
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Reserva(Models.ViewModel.ReservaHabitacionViewModel model)
        {
            var user = GetUserLogIn();
            var habitacion = GetHabitacionByHabitacionId(model.HabitacionId);

            if (model.FechaContrato < DateTime.Today)
            {
                ModelState.AddModelError("FechaContrato", "La fecha de ingreso no puede ser menor a la fecha actual");
            }
            if (model.FechaContrato > model.FechaVencimiento)
            {
                ModelState.AddModelError("FechaVencimiento", "La fecha de salida no puede ser menor a la fecha de ingreso");
            }
            if (ModelState.IsValid)
            {
                if (model.RememberCardNumber)
                {
                    user.CardNumber = model.CardNumber;
                    _db.Entry(user).State = EntityState.Modified;
                }
                var reserva = new Models.Entity.LineaHabitacionUsuarioEntity
                {
                    Id = Guid.NewGuid().ToString(),
                    FechaContrato = model.FechaContrato,
                    FechaVencimiento = model.FechaVencimiento,
                    Habilitado = true,
                    Habitacion = habitacion,
                    User = user,
                    NumeroNoches = (model.FechaVencimiento - model.FechaContrato).Days
                };
                reserva.TotalPago = habitacion.PrecioNoche * reserva.NumeroNoches;
                habitacion.HabitacionesDisponibles = habitacion.HabitacionesDisponibles - 1;
                _db.Entry(habitacion).State = EntityState.Modified;
                _db.LineaHabitacionUsuarios.Add(reserva);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(new Models.ViewModel.ReservaHabitacionViewModel { FechaContrato = model.FechaContrato, FechaVencimiento = model.FechaVencimiento, CardNumber = model.CardNumber, HabitacionId = habitacion.HabitacionId, ImageUrl = habitacion.ImageUrl, NombreHabitacion = habitacion.Nombre, PrecioHabitacion = habitacion.PrecioNoche });
        }

        #region utilitarios controller
        protected virtual Models.ApplicationUser GetUserLogIn()
        {
            return _db.Users.Where(x => x.UserName.Equals(User.Identity.Name)).FirstOrDefault();
        }

        protected virtual Models.Entity.HabitacionEntity GetHabitacionByHabitacionId(string id)
        {
            return _db.Habitaciones.Where(x => x.HabitacionId.Equals(id)).FirstOrDefault();
        }
        #endregion
    }
}