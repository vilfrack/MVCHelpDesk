using MVCHelpDesk.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MVCHelpDesk.ViewModel
{
    public class ViewComentario
    {
        public string TaskID { get; set; }

        [Required(ErrorMessage = "You must enter the field {0}")]
        [StringLength(500, ErrorMessage = "The fiel {0} must contain between {2} and {1} characters", MinimumLength = 5)]
        public string Comentario { get; set; }
        List<ViewComentarioPerfiles> comentarioPerfiles { get; set; }

    }
    public class ViewComentarioPerfiles {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string rutaImg { get; set; }
        public string Comentario { get; set; }
    }
}