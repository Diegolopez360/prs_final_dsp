using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CaligulasHotel.Models.Entity
{
    [Table("Marca")]
    public class MarcaEntity
    {
        [Key]
        public string MarcaId { get; set; }
        [StringLength(50)]
        public string Nombre { get; set; }

        public ICollection<ArticuloEntity> Articulos { get; set; }
    }
}