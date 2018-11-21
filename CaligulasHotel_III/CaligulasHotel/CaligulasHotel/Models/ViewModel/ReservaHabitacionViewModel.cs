using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CaligulasHotel.Models.ViewModel
{
    public class ReservaHabitacionViewModel
    {
        public string Id { get; set; }

        [Display(Name = "Fecha Entrada")]
        [DataType(DataType.Date)]
        public DateTime FechaContrato { get; set; }

        [Display(Name = "Fecha Salida")]
        [DataType(DataType.Date)]
        public DateTime FechaVencimiento { get; set; }

        [Required]
        [Display(Name = "Número de Tarjeta Credito/Debito")]
        public string CardNumber { get; set; }

        public string HabitacionId { get; set; }
        public string NombreHabitacion { get; set; }
        public string ImageUrl { get; set; }
        public double PrecioHabitacion { get; set; }

        [Display(Name = "Recordar número de tarjeta para la siguiente compra?")]
        public bool RememberCardNumber { get; set; }
    }
}