using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CaligulasHotel.Models.Entity
{
    [Table("LineaShoppingCart")]
    public class LineaShoppingCartEntity
    {
        [Key]
        public string LineaId { get; set; }
        public int NumeroArticulos { get; set; }
        public double TotalPagar { get; set; }
        public double PrecioUnitario { get; set; }
        public bool Cancelado { get; set; }
        public DateTime FechaAgregado { get; set; }
        public DateTime? FechaCancelado { get; set; }

        public ArticuloEntity Articulo { get; set; }
        public ShoppingCartEntity ShoppingCart { get; set; }
    }
}