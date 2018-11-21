using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CaligulasHotel.Models.Entity
{
    [Table("ShoppingCart")]
    public class ShoppingCartEntity
    {
        [Key]
        public string CartId { get; set; }
        [DataType(DataType.Date)]
        public DateTime FechaCreacion { get; set; }
        public int MaxItems { get; set; }

        public ApplicationUser User { get; set; }
    }
}