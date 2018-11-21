using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CaligulasHotel.Models.Entity
{
    [Table("LineaRentaVehiculo")]
    public class LineaRentaVehiculoEntity
    {
        [Key]
        public string Id { get; set; }
        public DateTime FechaRenta { get; set; }
        public DateTime FechaExpiracion { get; set; }
        public double TotalPago { get; set; }
        public int NumeroHoras { get; set; }
        public bool Activo { get; set; }

        public ApplicationUser User { get; set; }
        public RentaVehiculoEntity RentaVehiculo { get; set; }
    }
}