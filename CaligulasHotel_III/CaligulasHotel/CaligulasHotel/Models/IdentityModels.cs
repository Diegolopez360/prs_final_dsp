using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;

namespace CaligulasHotel.Models
{
    // Para agregar datos de perfil del usuario, agregue más propiedades a su clase ApplicationUser. Visite https://go.microsoft.com/fwlink/?LinkID=317594 para obtener más información.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            return userIdentity;
        }

        [StringLength(50)]
        public string FirstName { get; set; }
        [StringLength(50)]
        public string LastName { get; set; }
        [StringLength(100)]
        public string FullName { get; set; }
        [StringLength(255)]
        public string Address { get; set; }
        [DataType(DataType.Date)]
        public System.DateTime BirthDate { get; set; }
        public System.DateTime CreateDate { get; set; }
        [StringLength(50)]
        public string CardNumber { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext() : base("CaligulasHotelConnection", throwIfV1Schema: false)
        {
            Database.SetInitializer(new Migrations.Configuration());
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<Entity.ArticuloEntity> Articulos { get; set; }
        public DbSet<Entity.CategoriaEntity> Categorias { get; set; }
        public DbSet<Entity.FacturaEntity> Facturas { get; set; }
        public DbSet<Entity.LineaFacturaEntity> LineaFacturas { get; set; }
        public DbSet<Entity.LineaRentaVehiculoEntity> LineaRentaVehiculos { get; set; }
        public DbSet<Entity.MarcaEntity> Marcas { get; set; }
        public DbSet<Entity.RentaVehiculoEntity> RentaVehiculos { get; set; }
        public DbSet<Entity.TipoVehiculoEntity> TipoVehiculos { get; set; }
        public DbSet<Entity.VehiculoEntity> Vehiculos { get; set; }
        public DbSet<Entity.HabitacionEntity> Habitaciones { get; set; }
        public DbSet<Entity.TipoHabitacionEntity> TipoHabitaciones { get; set; }
        public DbSet<Entity.LineaHabitacionUsuarioEntity> LineaHabitacionUsuarios { get; set; }
        public DbSet<Entity.ShoppingCartEntity> ShoppingCarts { get; set; }
        public DbSet<Entity.LineaShoppingCartEntity> LineaShoppingCarts { get; set; }
    }
}