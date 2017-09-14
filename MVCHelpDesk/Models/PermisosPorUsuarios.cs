using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVCHelpDesk.Models
{
    public class PermisosPorUsuarios
    {
        [Key]
        public int PermisoUsuarioID { get; set; }

        public int PermisoID { get; set; }

        public string UsuarioID { get; set; }

        public int ModuloID { get; set; }

        public virtual Modulos Modulos { get; set; }

        public virtual Permisos Permisos { get; set; }
        //SE DEBE PONER ForeignKey SI QUEREMOS ESTABLECER UNA RELACION CON UN CAMPO EN ESPECIFICO
        [ForeignKey("UsuarioID")]
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}