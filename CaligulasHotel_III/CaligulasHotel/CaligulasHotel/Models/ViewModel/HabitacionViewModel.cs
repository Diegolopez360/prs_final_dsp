using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CaligulasHotel.Models.ViewModel
{
    public class HabitacionViewModel
    {
        public string HabitacionId { get; set; }

        [Display(Name = "Nombre")]
        [Required]
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        [Display(Name = "Número de Personas")]
        [Required]
        public int NumeroPersonas { get; set; }

        [Display(Name = "Imagen de Habitación")]
        public string ImageUrl { get; set; }

        [Display(Name = "Precio por Noche")]
        [Required]
        public double PrecioNoche { get; set; }

        [Display(Name = "Disponible")]
        [Required]
        public bool Disponible { get; set; }

        [Display(Name = "Tipo de Habitación")]
        [Required]
        public string TipoHabitacion { get; set; }

        public HttpPostedFileBase File { get; set; }

        [Display(Name = "Número de Habitaciones Existentes")]
        [Required]
        public int NumeroHabitaciones { get; set; }

        [Display(Name = "Número de Habitaciones Disponible")]
        [Required]
        public int HabitacionesDisponibles { get; set; }

        [Display(Name = "Servicios de Habitación")]
        public string ServiciosHabitacion { get; set; }
    }
}