using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CaligulasHotel.Models.Entity
{
    [Table("Categoria")]
    public class CategoriaEntity
    {
        [Key]
        public string CategoriaId { get; set; }
        [StringLength(50)]
        public string Nombre { get; set; }

        public ICollection<ArticuloEntity> Articulos { get; set; }
    }
}