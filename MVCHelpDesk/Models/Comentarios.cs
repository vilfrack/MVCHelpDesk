using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVCHelpDesk.Models
{
    public class Comentarios
    {
        [Key]
        public int IDComentario { get; set; }

        public string Comentario { get; set; }

        public string UsuarioID { get; set; }

        public int TaskID { get; set; }

        public DateTime Fecha { get; set; }

        [ForeignKey("TaskID")]
        public virtual Tasks Tasks { get; set; }

        [ForeignKey("UsuarioID")]
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}