using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CaligulasHotel.Models.Entity
{
    [Table("TipoHabitacion")]
    public class TipoHabitacionEntity
    {
        [Key]
        public string TipoHabitacionId { get; set; }
        public string Nombre { get; set; }

        public ICollection<HabitacionEntity> Habitaciones { get; set; }
    }
}