using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CaligulasHotel.Models.Entity
{
    [Table("Habitacion")]
    public class HabitacionEntity
    {
        [Key]
        public string HabitacionId { get; set; }
        public string Nombre { get; set; }
        public int NumeroPersonas { get; set; }
        public string Descripcion { get; set; }
        public string ImageUrl { get; set; }
        public double PrecioNoche { get; set; }
        public int NumeroHabitaciones { get; set; }
        public int HabitacionesDisponibles { get; set; }
        public string ServiciosHabitacion { get; set; }
        public bool Disponible { get; set; }

        public TipoHabitacionEntity TipoHabitacion { get; set; }
    }
}