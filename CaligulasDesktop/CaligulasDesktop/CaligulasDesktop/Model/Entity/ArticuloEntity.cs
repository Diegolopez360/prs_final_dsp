using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaligulasDesktop.Model.Entity
{
    class ArticuloEntity
    {
        public string ArticuloId { get; set; }
        public string Sku { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string ImageUrl { get; set; }
        public double PrecioUnitario { get; set; }
        public double PrecioCompra { get; set; }
        public int Stock { get; set; }
        public int UnidadesCompradas { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string CategoriaId { get; set; }
        public string MarcaId { get; set; }
    }
}
