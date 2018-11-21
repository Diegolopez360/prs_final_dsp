using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaligulasHotel.Models.ViewModel
{
    public class LeaseViewModel
    {
        public ICollection<Entity.LineaRentaVehiculoEntity> RentaVehiculo { get; set; }
        public ICollection<Entity.LineaHabitacionUsuarioEntity> Habitaciones { get; set; }
    }
}