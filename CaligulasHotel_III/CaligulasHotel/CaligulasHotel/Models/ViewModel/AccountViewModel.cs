using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaligulasHotel.Models.ViewModel
{
    public class AccountViewModel
    {
        public ICollection<Entity.LineaShoppingCartEntity> LineaShoppings { get; set; }
        public ICollection<Entity.LineaHabitacionUsuarioEntity> Habitaciones { get; set; }
        public ICollection<Entity.LineaRentaVehiculoEntity> RentaVehiculos { get; set; }
        public ICollection<Entity.FacturaEntity> Facturas { get; set; }
    }
}