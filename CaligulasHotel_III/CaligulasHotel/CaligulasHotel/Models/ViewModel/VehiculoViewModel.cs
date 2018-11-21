using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CaligulasHotel.Models.ViewModel
{
    public class VehiculoViewModel
    {
        public string VehiculoId { get; set; }

        [Display(Name = "Nombre")]
        [Required]
        [StringLength(50)]
        public string Nombre { get; set; }

        [Display(Name = "Descripción")]
        [StringLength(255)]
        public string Descripcion { get; set; }

        [Display(Name = "Matrícula")]
        [Required]
        [StringLength(25)]
        public string Matricula { get; set; }

        public string FileName { get; set; }
        [Display(Name = "Imagen")]
        public HttpPostedFileBase File { get; set; }

        [Display(Name = "Número de Personas")]
        [Required]
        public int NumeroPersonas { get; set; }

        [Display(Name = "Fecha de Fabricación")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FechaFabricacion { get; set; }

        [Display(Name = "Tipo Vehículo")]
        [Required]
        public string TipoVehiculo { get; set; }

        [Display(Name = "Costo Hora")]
        [Required]
        public double CostoHora { get; set; }

        [Display(Name = "Mínimo de Horas")]
        [Required]
        public int MinimoHoras { get; set; }

        [Display(Name = "Máximo de Horas")]
        [Required]
        public int MaximoHoras { get; set; }

        [Display(Name = "Mínimo de Minutos")]
        [Required]
        public int MinimoMinutos { get; set; }

        [Display(Name = "Máximo de Minutos")]
        [Required]
        public int MaximoMinutos { get; set; }

        [Display(Name = "Este vehículo esta disponible para rentar?")]
        [Required]
        public bool Habilitado { get; set; }
    }
}