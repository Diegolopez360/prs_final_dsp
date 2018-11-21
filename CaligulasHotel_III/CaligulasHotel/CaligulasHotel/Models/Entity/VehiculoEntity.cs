using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CaligulasHotel.Models.Entity
{
    [Table("Vehiculo")]
    public class VehiculoEntity
    {
        [Key]
        public string VehiculoId { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Matricula { get; set; }
        public string ImageUrl { get; set; }
        public int NumeroPersonas { get; set; }
        public DateTime FechaFabricacion { get; set; }

        public TipoVehiculoEntity TipoVehiculo { get; set; }
    }
}