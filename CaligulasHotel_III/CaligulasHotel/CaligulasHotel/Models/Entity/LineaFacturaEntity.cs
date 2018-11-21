using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CaligulasHotel.Models.Entity
{
    [Table("LineaFactura")]
    public class LineaFacturaEntity
    {
        [Key]
        public string Id { get; set; }
        public int NumeroArticulos { get; set; }

        public ArticuloEntity Articulo { get; set; }
        public FacturaEntity Factura { get; set; }
    }
}