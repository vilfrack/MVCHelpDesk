using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCHelpDesk.ViewModel
{
    public class ViewPrueba
    {
        public int? ModuloID { get; set; }
        public string ModuloDescripcion { get; set; }
        public string RolID { get; set; }

        public int? PermisoID { get; set; }
        public string PermisoDescripcion { get; set; }
        public bool Cheek { get; set; }
    }
    public class ViewPrueba2
    {
        public int? PermisoID { get; set; }
        public string PermisoDescripcion { get; set; }
        public bool Cheek { get; set; }
    }
}