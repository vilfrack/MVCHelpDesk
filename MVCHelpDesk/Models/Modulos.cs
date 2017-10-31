using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCHelpDesk.Models
{
    public class Modulos
    {
        [Key]
        public int? ModuloID { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<PermisoPorRol> PermisoPorRol { get; set; }
    }
}