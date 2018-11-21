using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CaligulasHotel.Models.Entity
{
    [Table("RentaVehiculo")]
    public class RentaVehiculoEntity
    {
        [Key]
        public string Id { get; set; }
        public double CostoHora { get; set; }
        public int MaxHoras { get; set; }
        public int MinHoras { get; set; }
        public int MinMinutos { get; set; }
        public int MaxMinutos { get; set; }
        public bool Habilitado { get; set; }

        public VehiculoEntity Vehiculo { get; set; }
    }
}