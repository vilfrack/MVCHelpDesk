using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCHelpDesk.ViewModel
{
    public class ViewAsignar
    {
        public string UsuarioID { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string UsuarioAsignado { get; set; }
        public string rutaImg { get; set; }
        public string Asignado { get; set; }
        public int TaskID { get; set; }
    }
}