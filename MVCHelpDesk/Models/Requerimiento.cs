using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCHelpDesk.Models
{
    public class Requerimiento
    {
        [Key]
        public int TaskID { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaFinalizacion { get; set; }//fecha en el que se deberia finalizar el task

        [Required(ErrorMessage = "You must enter the field {0}")]
        [StringLength(100, ErrorMessage = "The fiel {0} must contain between {2} and {1} characters", MinimumLength = 4)]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "You must enter the field {0}")]
        [StringLength(200, ErrorMessage = "The fiel {0} must contain between {2} and {1} characters", MinimumLength = 10)]
        public string Descripcion { get; set; }

        public int? StatusID { get; set; }



        //public virtual ICollection<Files> Files { get; set; }

        //public virtual Status Status { get; set; }
    }
}