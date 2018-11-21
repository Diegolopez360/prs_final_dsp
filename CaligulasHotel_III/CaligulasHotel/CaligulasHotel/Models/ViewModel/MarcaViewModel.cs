using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CaligulasHotel.Models.ViewModel
{
    public class MarcaViewModel
    {
        public string MarcaId { get; set; }

        [Required]
        [StringLength(50)]
        public string Nombre { get; set; }
    }
}