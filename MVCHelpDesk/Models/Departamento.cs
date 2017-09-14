using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCHelpDesk.Models
{
    public class Departamento
    {
        [Key]
        public int IDDepartamento { get; set; }

        [Display(Name = "Departamento")]
        [Required(ErrorMessage = "El campo {0} No puede quedar vacio")]//el {0} copia el nombre del campo
        [StringLength(20, ErrorMessage = "El campo {0} debe contener entre {2} y {1} caracteres", MinimumLength = 5)]
        public string departamento { get; set; }

    }
}