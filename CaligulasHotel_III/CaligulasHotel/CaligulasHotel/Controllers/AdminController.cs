using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Data.Entity;
using System.IO;

namespace CaligulasHotel.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class AdminController : Controller
    {
        private readonly Models.ApplicationDbContext _db = new Models.ApplicationDbContext();

        public ActionResult Index()
        {
            return View();
        }

        #region categorias
        public ActionResult Categoria()
        {
            ViewBag.Message = TempData["Message"];
            return View(_db.Categorias.ToArray());
        }

        public ActionResult CreateCategory()
        {
            return View();
        }

        public ActionResult EditCategory(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var categoria = _db.Categorias.Where(x => x.CategoriaId.Equals(id)).FirstOrDefault();
            if (categoria == null)
            {
                return HttpNotFound();
            }
            return View(new Models.ViewModel.CategoriaViewModel { CategoriaId = categoria.CategoriaId, Nombre = categoria.Nombre });
        }

        public ActionResult DeleteCategory(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var categoria = _db.Categorias.Where(x => x.CategoriaId.Equals(id)).FirstOrDefault();
            if (categoria == null)
            {
                return HttpNotFound();
            }
            return View(new Models.ViewModel.CategoriaViewModel { CategoriaId = categoria.CategoriaId, Nombre = categoria.Nombre });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteCategoryPost(string CategoriaId)
        {
            var categoria = _db.Categorias.Where(x => x.CategoriaId.Equals(CategoriaId)).FirstOrDefault();
            if (categoria == null)
            {
                return HttpNotFound();
            }
            _db.Categorias.Remove(categoria);
            _db.SaveChanges();
            TempData["Message"] = "Los datos fueron eliminados con éxito";
            return RedirectToAction("Categoria");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCategory(Models.ViewModel.CategoriaViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(model.CategoriaId))
                {
                    var categoria = new Models.Entity.CategoriaEntity
                    {
                        CategoriaId = Guid.NewGuid().ToString(),
                        Nombre = model.Nombre
                    };
                    _db.Categorias.Add(categoria);
                    TempData["Message"] = "Se ha ingresado un nuevo registro";
                }
                else
                {
                    var categoria = new Models.Entity.CategoriaEntity
                    {
                        CategoriaId = model.CategoriaId,
                        Nombre = model.Nombre
                    };
                    _db.Entry(categoria).State = EntityState.Modified;
                    TempData["Message"] = "Los datos fueron editados con éxito";
                }
                _db.SaveChanges();
                return RedirectToAction("Categoria");
            }
            return View(model);
        }
        #endregion

        #region marcas
        public ActionResult Marca()
        {
            ViewBag.Message = TempData["Message"];
            return View(_db.Marcas.ToArray());
        }

        public ActionResult CreateBrand()
        {
            return View();
        }

        public ActionResult EditBrand(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var marca = _db.Marcas.Where(x => x.MarcaId.Equals(id)).FirstOrDefault();
            if (marca == null)
            {
                return HttpNotFound();
            }
            return View(new Models.ViewModel.MarcaViewModel { MarcaId = marca.MarcaId, Nombre = marca.Nombre });
        }

        public ActionResult DeleteBrand(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var marca = _db.Marcas.Where(x => x.MarcaId.Equals(id)).FirstOrDefault();
            if (marca == null)
            {
                return HttpNotFound();
            }
            return View(new Models.ViewModel.MarcaViewModel { MarcaId = marca.MarcaId, Nombre = marca.Nombre });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteBrandPost(string MarcaId)
        {
            var marca = _db.Marcas.Where(x => x.MarcaId.Equals(MarcaId)).FirstOrDefault();
            if (marca == null)
            {
                return HttpNotFound();
            }
            _db.Marcas.Remove(marca);
            _db.SaveChanges();
            TempData["Message"] = "Los datos fueron eliminados con éxito";
            return RedirectToAction("Marca");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateBrand(Models.ViewModel.MarcaViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(model.MarcaId))
                {
                    var marca = new Models.Entity.MarcaEntity
                    {
                        MarcaId = Guid.NewGuid().ToString(),
                        Nombre = model.Nombre
                    };
                    _db.Marcas.Add(marca);
                    TempData["Message"] = "Se ha ingresado un nuevo registro";
                }
                else
                {
                    var marca = new Models.Entity.MarcaEntity
                    {
                        MarcaId = model.MarcaId,
                        Nombre = model.Nombre
                    };
                    _db.Entry(marca).State = EntityState.Modified;
                    TempData["Message"] = "Los datos fueron editados con éxito";
                }
                _db.SaveChanges();
                return RedirectToAction("Marca");
            }
            return View(model);
        }
        #endregion

        #region vehiculos
        public ActionResult Vehiculo()
        {
            ViewBag.Message = TempData["Message"];
            return View(_db.Vehiculos.ToArray());
        }

        public ActionResult CreateVehiculo()
        {
            ViewBag.CmbTipoVehiculo = _db.TipoVehiculos.OrderBy(x => x.Nombre).ToArray();
            return View();
        }

        public ActionResult EditVehiculo(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var vehiculo = _db.Vehiculos.Include(x => x.TipoVehiculo).Where(x => x.VehiculoId.Equals(id)).FirstOrDefault();
            var _rentavehiculo = _db.RentaVehiculos.Where(x => x.Vehiculo.VehiculoId.Equals(vehiculo.VehiculoId)).FirstOrDefault();

            if (vehiculo == null)
            {
                return HttpNotFound();
            }
            ViewBag.CmbTipoVehiculo = _db.TipoVehiculos.OrderBy(x => x.Nombre).ToArray();
            return View(new Models.ViewModel.VehiculoViewModel { VehiculoId = vehiculo.VehiculoId, Nombre = vehiculo.Nombre, Descripcion = vehiculo.Descripcion, Matricula = vehiculo.Matricula, NumeroPersonas = vehiculo.NumeroPersonas, FechaFabricacion = vehiculo.FechaFabricacion, TipoVehiculo = vehiculo.TipoVehiculo.TipoVehiculoId, FileName = vehiculo.ImageUrl, CostoHora = _rentavehiculo.CostoHora, MinimoHoras = _rentavehiculo.MinHoras, MaximoHoras = _rentavehiculo.MaxHoras, MaximoMinutos = _rentavehiculo.MaxMinutos, MinimoMinutos = _rentavehiculo.MinMinutos, Habilitado = _rentavehiculo.Habilitado });
        }

        public ActionResult DeleteVehiculo(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var vehiculo = _db.Vehiculos.Include(x => x.TipoVehiculo).Where(x => x.VehiculoId.Equals(id)).FirstOrDefault();
            if (vehiculo == null)
            {
                return HttpNotFound();
            }
            return View(new Models.ViewModel.VehiculoViewModel { VehiculoId = vehiculo.VehiculoId, Nombre = vehiculo.Nombre, Descripcion = vehiculo.Descripcion, Matricula = vehiculo.Matricula, NumeroPersonas = vehiculo.NumeroPersonas, FechaFabricacion = vehiculo.FechaFabricacion, TipoVehiculo = vehiculo.TipoVehiculo.Nombre });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteVehiculoPost(string VehiculoId)
        {
            var vehiculo = _db.Vehiculos.Where(x => x.VehiculoId.Equals(VehiculoId)).FirstOrDefault();
            var rentavehiculo = _db.RentaVehiculos.Where(x => x.Vehiculo.VehiculoId.Equals(vehiculo.VehiculoId)).FirstOrDefault();

            if (vehiculo == null)
            {
                return HttpNotFound();
            }
            if (rentavehiculo != null)
            {
                _db.RentaVehiculos.Remove(rentavehiculo);
            }
            _db.Vehiculos.Remove(vehiculo);
            _db.SaveChanges();
            TempData["Message"] = "Los datos fueron eliminados con éxito";
            return RedirectToAction("Vehiculo");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateVehiculo(Models.ViewModel.VehiculoViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.File != null && model.File.ContentLength > 0)
                {
                    model.File.SaveAs(Path.Combine(Server.MapPath("~/Uploads"), model.File.FileName));
                }

                if (string.IsNullOrEmpty(model.VehiculoId))
                {
                    var vehiculo = new Models.Entity.VehiculoEntity
                    {
                        VehiculoId = Guid.NewGuid().ToString(),
                        Nombre = model.Nombre,
                        Descripcion = model.Descripcion,
                        NumeroPersonas = model.NumeroPersonas,
                        Matricula = model.Matricula,
                        ImageUrl = model?.File.FileName,
                        FechaFabricacion = model.FechaFabricacion,
                        TipoVehiculo = _db.TipoVehiculos.Where(x => x.TipoVehiculoId.Equals(model.TipoVehiculo)).FirstOrDefault()
                    };
                    var rentavehiculo = new Models.Entity.RentaVehiculoEntity
                    {
                        Id = Guid.NewGuid().ToString(),
                        CostoHora = model.CostoHora,
                        MaxHoras = model.MaximoHoras,
                        MinHoras = model.MinimoHoras,
                        MaxMinutos = model.MaximoMinutos,
                        MinMinutos = model.MinimoMinutos,
                        Habilitado = model.Habilitado,
                        Vehiculo = vehiculo
                    };
                    _db.Vehiculos.Add(vehiculo);
                    _db.RentaVehiculos.Add(rentavehiculo);
                    TempData["Message"] = "Se ha ingresado un nuevo registro";
                }
                else
                {
                    var vehiculo = _db.Vehiculos.Where(x => x.VehiculoId.Equals(model.VehiculoId)).FirstOrDefault();
                    var rentavehiculo = _db.RentaVehiculos.Where(x => x.Vehiculo.VehiculoId.Equals(vehiculo.VehiculoId)).FirstOrDefault();

                    vehiculo.Nombre = model.Nombre;
                    vehiculo.Descripcion = model.Descripcion;
                    vehiculo.NumeroPersonas = model.NumeroPersonas;
                    vehiculo.Matricula = model.Matricula;
                    vehiculo.FechaFabricacion = model.FechaFabricacion;
                    vehiculo.TipoVehiculo = _db.TipoVehiculos.Where(x => x.TipoVehiculoId.Equals(model.TipoVehiculo)).FirstOrDefault();

                    rentavehiculo.CostoHora = model.CostoHora;
                    rentavehiculo.MinHoras = model.MinimoHoras;
                    rentavehiculo.MinMinutos = model.MinimoMinutos;
                    rentavehiculo.MaxHoras = model.MaximoHoras;
                    rentavehiculo.MaxMinutos = model.MaximoMinutos;
                    rentavehiculo.Habilitado = model.Habilitado;

                    _db.Entry(vehiculo).State = EntityState.Modified;
                    _db.Entry(rentavehiculo).State = EntityState.Modified;
                    TempData["Message"] = "Los datos fueron editados con éxito";
                }
                _db.SaveChanges();
                
                return RedirectToAction("Vehiculo");
            }
            ViewBag.CmbTipoVehiculo = _db.TipoVehiculos.OrderBy(x => x.Nombre).ToArray();
            return View(model);
        }
        #endregion

        #region tipovehiculos
        public ActionResult TipoVehiculo()
        {
            ViewBag.Message = TempData["Message"];
            return View(_db.TipoVehiculos.ToArray());
        }

        public ActionResult CreateTipoVehiculo()
        {
            return View();
        }

        public ActionResult EditTipoVehiculo(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var tipovehiculo = _db.TipoVehiculos.Where(x => x.TipoVehiculoId.Equals(id)).FirstOrDefault();
            if (tipovehiculo == null)
            {
                return HttpNotFound();
            }
            return View(new Models.ViewModel.TipoVehiculoViewModel { TipoVehiculoId = tipovehiculo.TipoVehiculoId, Nombre = tipovehiculo.Nombre });
        }

        public ActionResult DeleteTipoVehiculo(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var tipovehiculo = _db.TipoVehiculos.Where(x => x.TipoVehiculoId.Equals(id)).FirstOrDefault();
            if (tipovehiculo == null)
            {
                return HttpNotFound();
            }
            return View(new Models.ViewModel.TipoVehiculoViewModel { TipoVehiculoId = tipovehiculo.TipoVehiculoId, Nombre = tipovehiculo.Nombre });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteTipoVehiculoPost(string TipoVehiculoId)
        {
            var tipovehiculo = _db.TipoVehiculos.Where(x => x.TipoVehiculoId.Equals(TipoVehiculoId)).FirstOrDefault();
            if (tipovehiculo == null)
            {
                return HttpNotFound();
            }
            _db.TipoVehiculos.Remove(tipovehiculo);
            _db.SaveChanges();
            TempData["Message"] = "Los datos fueron eliminados con éxito";
            return RedirectToAction("TipoVehiculo");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateTipoVehiculo(Models.ViewModel.TipoVehiculoViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(model.TipoVehiculoId))
                {
                    var tipovehiculo = new Models.Entity.TipoVehiculoEntity
                    {
                        TipoVehiculoId = Guid.NewGuid().ToString(),
                        Nombre = model.Nombre
                    };
                    _db.TipoVehiculos.Add(tipovehiculo);
                    TempData["Message"] = "Se ha ingresado un nuevo registro";
                }
                else
                {
                    var tipovehiculo = new Models.Entity.TipoVehiculoEntity
                    {
                        TipoVehiculoId = model.TipoVehiculoId,
                        Nombre = model.Nombre
                    };
                    _db.Entry(tipovehiculo).State = System.Data.Entity.EntityState.Modified;
                    TempData["Message"] = "Los datos fueron editados con éxito";
                }
                _db.SaveChanges();
                return RedirectToAction("TipoVehiculo");
            }
            return View(model);
        }
        #endregion

        #region articulos
        public ActionResult Articulo()
        {
            ViewBag.Message = TempData["Message"];
            return View(_db.Articulos.OrderBy(x => x.FechaCreacion).ToArray());
        }

        public ActionResult CreateArticle()
        {
            ViewBag.Cmbcategorias = _db.Categorias.ToArray();
            ViewBag.Cmbmarcas = _db.Marcas.ToArray();
            return View();
        }

        public ActionResult EditArticle(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var articulo = _db.Articulos.Where(x => x.ArticuloId.Equals(id)).FirstOrDefault();
            if (articulo == null)
            {
                return HttpNotFound();
            }
            ViewBag.Cmbcategorias = _db.Categorias.ToArray();
            ViewBag.Cmbmarcas = _db.Marcas.ToArray();
            return View(new Models.ViewModel.ArticuloViewModel { ArticuloId = articulo.ArticuloId, Nombre = articulo.Nombre, SKU = articulo.SKU, Descripcion = articulo.Descripcion, ImageUrl = articulo.ImageUrl, PrecioCompra = articulo.PrecioCompra, PrecioUnitario = articulo.PrecioUnitario, Stock = articulo.Stock, UnidadesCompradas = articulo.UnidadesCompradas, Categoria = articulo.Categoria.CategoriaId, Marca = articulo.Marca.MarcaId });
        }

        public ActionResult DeleteArticle(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var articulo = _db.Articulos.Include(a => a.Marca).Include(a => a.Categoria).Where(x => x.ArticuloId.Equals(id)).FirstOrDefault();
            if (articulo == null)
            {
                return HttpNotFound();
            }
            return View(articulo);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult DeleteArticlePost(string ArticuloId)
        {
            if (string.IsNullOrEmpty(ArticuloId))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var articulo = _db.Articulos.Where(x => x.ArticuloId.Equals(ArticuloId)).FirstOrDefault();
            if (articulo == null)
            {
                return HttpNotFound();
            }
            _db.Articulos.Remove(articulo);
            _db.SaveChanges();

            System.IO.File.SetAttributes(Path.Combine(Server.MapPath("~/Uploads"), articulo.ImageUrl), FileAttributes.Normal);
            System.IO.File.Delete(Path.Combine(Server.MapPath("~/Uploads"), articulo.ImageUrl));
            TempData["Message"] = "Los datos fueron eliminados con éxito";
            return RedirectToAction("Articulo");
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult CreateArticle(Models.ViewModel.ArticuloViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(model.ArticuloId))
                {
                    var articulo = new Models.Entity.ArticuloEntity
                    {
                        ArticuloId = Guid.NewGuid().ToString(),
                        Nombre = model.Nombre,
                        Descripcion = model.Descripcion,
                        SKU = model.SKU,
                        PrecioCompra = model.PrecioCompra,
                        PrecioUnitario = model.PrecioUnitario,
                        Stock = model.Stock,
                        UnidadesCompradas = model.UnidadesCompradas,
                        FechaCreacion = DateTime.Now,
                        Categoria = _db.Categorias.Where(x => x.CategoriaId.Equals(model.Categoria)).FirstOrDefault(),
                        Marca = _db.Marcas.Where(x => x.MarcaId.Equals(model.Marca)).FirstOrDefault(),
                        ImageUrl = model.File?.FileName
                    };
                    if (model.File != null && model.File.ContentLength > 0)
                    {
                        model.File.SaveAs(Path.Combine(Server.MapPath("~/Uploads"), model.File.FileName));
                    }
                    _db.Articulos.Add(articulo);
                    TempData["Message"] = "Se ha ingresado un nuevo registro";
                }
                else
                {
                    var articulo = _db.Articulos.Include(a => a.Marca).Include(a => a.Categoria).Where(x => x.ArticuloId.Equals(model.ArticuloId)).FirstOrDefault();
                    articulo.Nombre = model.Nombre;
                    articulo.Descripcion = model.Descripcion;
                    articulo.SKU = model.SKU;
                    articulo.PrecioCompra = model.PrecioCompra;
                    articulo.PrecioUnitario = model.PrecioUnitario;
                    articulo.Stock = model.Stock;
                    articulo.UnidadesCompradas = model.UnidadesCompradas;
                    if (!articulo.Marca.MarcaId.Equals(model.Marca))
                    {
                        articulo.Marca = _db.Marcas.Where(x => x.MarcaId.Equals(model.Marca)).FirstOrDefault();
                    }
                    if (!articulo.Categoria.CategoriaId.Equals(model.Categoria))
                    {
                        articulo.Categoria = _db.Categorias.Where(x => x.CategoriaId.Equals(model.Categoria)).FirstOrDefault();
                    }
                    if (model.File != null && model.File.ContentLength > 0)
                    {
                        articulo.ImageUrl = model.File?.FileName;
                        model.File.SaveAs(Path.Combine(Server.MapPath("~/Uploads"), model.File.FileName));
                    }
                    _db.Entry(articulo).State = EntityState.Modified;
                    TempData["Message"] = "Los datos fueron editados con éxito";
                }
                _db.SaveChanges();
                return RedirectToAction("Articulo");
            }
            ViewBag.Cmbcategorias = _db.Categorias.ToArray();
            ViewBag.Cmbmarcas = _db.Marcas.ToArray();
            return View(model);
        }
        #endregion

        #region habitaciones
        public ActionResult Habitacion()
        {
            ViewBag.Message = TempData["Message"];
            return View(_db.Habitaciones.Include(x => x.TipoHabitacion).ToArray());
        }

        public ActionResult CreateHabitacion()
        {
            ViewBag.Cmbtipohabitacion = _db.TipoHabitaciones.ToArray();
            return View();
        }

        public ActionResult EditHabitacion(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var habitacion = _db.Habitaciones.Include(h => h.TipoHabitacion).Where(x => x.HabitacionId.Equals(id)).FirstOrDefault();
            if (habitacion == null)
            {
                return HttpNotFound();
            }
            ViewBag.Cmbtipohabitacion = _db.TipoHabitaciones.ToArray();
            return View(new Models.ViewModel.HabitacionViewModel { HabitacionId = habitacion.HabitacionId, Nombre = habitacion.Nombre, Descripcion = habitacion.Descripcion, NumeroPersonas = habitacion.NumeroPersonas, PrecioNoche = habitacion.PrecioNoche, ImageUrl = habitacion.ImageUrl, Disponible = habitacion.Disponible, TipoHabitacion = habitacion.TipoHabitacion.TipoHabitacionId, NumeroHabitaciones = habitacion.NumeroHabitaciones, HabitacionesDisponibles = habitacion.HabitacionesDisponibles, ServiciosHabitacion = habitacion.ServiciosHabitacion });
        }

        public ActionResult DeleteHabitacion(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var habitacion = _db.Habitaciones.Include(h => h.TipoHabitacion).Where(x => x.HabitacionId.Equals(id)).FirstOrDefault();
            if (habitacion == null)
            {
                return HttpNotFound();
            }
            return View(habitacion);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult DeleteHabitacionPost(string HabitacionId)
        {
            if (string.IsNullOrEmpty(HabitacionId))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var habitacion = _db.Habitaciones.Where(x => x.HabitacionId.Equals(HabitacionId)).FirstOrDefault();
            if (habitacion == null)
            {
                return HttpNotFound();
            }
            _db.Habitaciones.Remove(habitacion);
            _db.SaveChanges();
            DeleteFileUploadsByFileName(habitacion.ImageUrl);
            TempData["Message"] = "Los datos fueron eliminados con éxito";
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult CreateHabitacion(Models.ViewModel.HabitacionViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(model.HabitacionId))
                {
                    var habitacion = new Models.Entity.HabitacionEntity
                    {
                        HabitacionId = Guid.NewGuid().ToString(),
                        Nombre = model.Nombre,
                        Descripcion = model.Descripcion,
                        NumeroPersonas = model.NumeroPersonas,
                        PrecioNoche = model.PrecioNoche,
                        Disponible = model.Disponible,
                        ImageUrl = model.File?.FileName,
                        TipoHabitacion = _db.TipoHabitaciones.Where(x => x.TipoHabitacionId.Equals(model.TipoHabitacion)).FirstOrDefault(),
                        NumeroHabitaciones = model.NumeroHabitaciones,
                        HabitacionesDisponibles = model.HabitacionesDisponibles,
                        ServiciosHabitacion = model.ServiciosHabitacion
                    };
                    if (model.File != null && model.File.ContentLength > 0)
                    {
                        SaveServerFileUpload(model.File);
                    }
                    _db.Habitaciones.Add(habitacion);
                    TempData["Message"] = "Se ha ingresado un nuevo registro";
                }
                else
                {
                    var habitacion = _db.Habitaciones.Include(h => h.TipoHabitacion).Where(x => x.HabitacionId.Equals(model.HabitacionId)).FirstOrDefault();
                    habitacion.Nombre = model.Nombre;
                    habitacion.Descripcion = model.Descripcion;
                    habitacion.NumeroPersonas = model.NumeroPersonas;
                    habitacion.PrecioNoche = model.PrecioNoche;
                    habitacion.Disponible = model.Disponible;
                    habitacion.NumeroHabitaciones = model.NumeroHabitaciones;
                    habitacion.HabitacionesDisponibles = model.HabitacionesDisponibles;
                    habitacion.ServiciosHabitacion = model.ServiciosHabitacion;

                    if (!habitacion.TipoHabitacion.TipoHabitacionId.Equals(model.TipoHabitacion))
                    {
                        habitacion.TipoHabitacion = _db.TipoHabitaciones.Where(x => x.TipoHabitacionId.Equals(model.TipoHabitacion)).FirstOrDefault();
                    }
                    if (model.File != null && model.File.ContentLength > 0)
                    {
                        habitacion.ImageUrl = model.File?.FileName;
                        SaveServerFileUpload(model.File);
                    }
                    _db.Entry(habitacion).State = EntityState.Modified;
                    TempData["Message"] = "Los datos fueron editados con éxito";
                }
                _db.SaveChanges();
                return RedirectToAction("Habitacion");
            }
            return View();
        }
        #endregion

        #region lease
        public ActionResult Lease()
        {
            ViewBag.Message = TempData["Message"];
            var model = new Models.ViewModel.LeaseViewModel
            {
                Habitaciones = _db.LineaHabitacionUsuarios.Include(x => x.Habitacion).Include(x => x.User).Where(x => x.Habilitado).ToArray(),
                RentaVehiculo = _db.LineaRentaVehiculos.Include(x => x.RentaVehiculo.Vehiculo).Include(x => x.User).Where(x => x.Activo).ToArray()
            };
            return View(model);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult DisableLease(string Type, string Identifier)
        {
            switch (Type)
            {
                case "Habitacion":
                    var lineahabitacion = _db.LineaHabitacionUsuarios.Include(x => x.Habitacion).Where(x => x.Id.Equals(Identifier)).FirstOrDefault();
                    var habitacion = lineahabitacion.Habitacion;
                    lineahabitacion.Habilitado = false;
                    habitacion.HabitacionesDisponibles = habitacion.HabitacionesDisponibles + 1;
                    break;
                case "Vehiculo":
                    var linearenta = _db.LineaRentaVehiculos.Include(x=>x.RentaVehiculo).Where(x => x.Id.Equals(Identifier)).FirstOrDefault();
                    var renta = linearenta.RentaVehiculo;
                    linearenta.Activo = false;
                    renta.Habilitado = true;
                    break;
            }
            _db.SaveChanges();
            TempData["Message"] = "Los datos fueron editados con éxito";
            return RedirectToAction("Lease");
        }
        #endregion

        #region utilitarios controller
        protected virtual void DeleteFileUploadsByFileName(string filename)
        {
            if (!string.IsNullOrEmpty(filename))
            {
                System.IO.File.SetAttributes(Path.Combine(Server.MapPath("~/Uploads"), filename), FileAttributes.Normal);
                System.IO.File.Delete(Path.Combine(Server.MapPath("~/Uploads"), filename));
            }

        }

        protected virtual void SaveServerFileUpload(HttpPostedFileBase file)
        {
            file.SaveAs(Path.Combine(Server.MapPath("~/Uploads"), file.FileName));
        }
        #endregion
    }
}