using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CaligulasHotel.Models.Entity
{
    [Table("TipoVehiculo")]
    public class TipoVehiculoEntity
    {
        [Key]
        public string TipoVehiculoId { get; set; }
        [StringLength(50)]
        public string Nombre { get; set; }

        public ICollection<VehiculoEntity> Vehiculos { get; set; }
    }
}