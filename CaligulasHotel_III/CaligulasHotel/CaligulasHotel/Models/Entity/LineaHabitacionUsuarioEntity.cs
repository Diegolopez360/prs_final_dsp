using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CaligulasHotel.Models.Entity
{
    [Table("LineaHabitacionUsuario")]
    public class LineaHabitacionUsuarioEntity
    {
        [Key]
        public string Id { get; set; }
        [DataType(DataType.Date)]
        public DateTime FechaContrato { get; set; }
        [DataType(DataType.Date)]
        public DateTime FechaVencimiento { get; set; }
        public int NumeroNoches { get; set; }
        public bool Habilitado { get; set; }
        public double TotalPago { get; set; }

        public ApplicationUser User { get; set; }
        public HabitacionEntity Habitacion { get; set; }
    }
}