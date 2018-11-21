using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using CaligulasHotel.Models;

namespace CaligulasHotel.Migrations
{
    internal sealed class Configuration : CreateDatabaseIfNotExists <CaligulasHotel.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            //AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(CaligulasHotel.Models.ApplicationDbContext context)
        {
            if (!context.Roles.Any())
            {
                context.Roles.Add(new IdentityRole("Administrador"));
                context.Roles.Add(new IdentityRole("Cliente"));
            }
            if (!context.Users.Any())
            {
                var container = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(container);
                var adminuser = new ApplicationUser { FirstName = "Administrador", LastName = "Administrador", FullName = "sysadmin", Email = "sysadmin@caligulashotel.com.sv", UserName = "sysadmin@caligulashotel.com.sv", BirthDate = DateTime.Now, CreateDate = DateTime.Now };
                manager.Create(adminuser, "Pa$$w0rd");
                manager.AddToRole(adminuser.Id, "Administrador");
            }
            if (!context.TipoHabitaciones.Any())
            {
                context.TipoHabitaciones.Add(new Models.Entity.TipoHabitacionEntity { TipoHabitacionId = Guid.NewGuid().ToString(), Nombre = "Estándar" });
                context.TipoHabitaciones.Add(new Models.Entity.TipoHabitacionEntity { TipoHabitacionId = Guid.NewGuid().ToString(), Nombre = "Estándar Vista al Mar" });
                context.TipoHabitaciones.Add(new Models.Entity.TipoHabitacionEntity { TipoHabitacionId = Guid.NewGuid().ToString(), Nombre = "De Lujo Vista al Mar" });
                context.TipoHabitaciones.Add(new Models.Entity.TipoHabitacionEntity { TipoHabitacionId = Guid.NewGuid().ToString(), Nombre = "Familiar" });
            }
        }
    }
}
