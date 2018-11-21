using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CaligulasHotel.Models.Entity
{
    [Table("Factura")]
    public class FacturaEntity
    {
        [Key]
        public string FacturaId { get; set; }
        public double TotalPagar { get; set; }
        public double Descuento { get; set; }
        public int NumeroArticulos { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string NumeroTarjeta { get; set; }

        public ApplicationUser User { get; set; }
        public ICollection<LineaFacturaEntity> LineaFacturas { get; set; }
    }
}