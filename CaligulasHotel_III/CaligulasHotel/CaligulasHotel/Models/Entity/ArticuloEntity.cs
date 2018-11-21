using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CaligulasHotel.Models.Entity
{
    [Table("Articulo")]
    public class ArticuloEntity
    {
        [Key]
        public string ArticuloId { get; set; }

        [StringLength(50)]
        public string SKU { get; set; }
        [StringLength(100)]
        public string Nombre { get; set; }
        [StringLength(255)]
        public string Descripcion { get; set; }
        public string ImageUrl { get; set; }
        public double PrecioUnitario { get; set; }
        public double PrecioCompra { get; set; }
        public int Stock { get; set; }
        public int UnidadesCompradas { get; set; }
        public DateTime FechaCreacion { get; set; }

        public CategoriaEntity Categoria { get; set; }
        public MarcaEntity Marca { get; set; }
    }
}