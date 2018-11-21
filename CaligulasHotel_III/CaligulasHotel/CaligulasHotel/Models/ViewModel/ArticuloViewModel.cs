using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CaligulasHotel.Models.ViewModel
{
    public class ArticuloViewModel
    {
        public string ArticuloId { get; set; }

        [Display(Name = "SKU")]
        [Required]
        public string SKU { get; set; }

        [Required]
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string ImageUrl { get; set; }

        [Required]
        public double PrecioUnitario { get; set; }

        [Required]
        public double PrecioCompra { get; set; }

        [Required]
        public int Stock { get; set; }

        [Required]
        public int UnidadesCompradas { get; set; }

        [Required]
        public string Categoria { get; set; }

        [Required]
        public string Marca { get; set; }

        [Display(Name = "Imagen de Artículo")]
        public HttpPostedFileBase File { get; set; }
    }
}