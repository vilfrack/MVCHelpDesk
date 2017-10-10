using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCHelpDesk.ViewModel
{
    public class ViewPermisos
    {
         public int? PermisoID { get; set; }
         public string PermisoDescripcion { get; set; }
         public string UsuarioID { get; set; }
         public string RolID { get; set; }
         public bool CheekRol { get; set; }
         public bool CheekUsuarios { get; set; }
         public int? ModuloIDPorUsuarios { get; set; }
         public int? ModuloIDPorRol { get; set; }
         public string ModuloRolDes { get; set; }
         public string ModuloUsuDes { get; set; }
         public int? ModuloUsuarioID { get; set; }
        public string ModuloDescripcion { get; set; }
        public int? ModuloID { get; set; }
    }
    public class ViewGetPermisos
    {
        public string PermisoID { get; set; }
        public string moduloID { get; set; }
        public string UsuarioPermisoID { get; set; }
        public string UsuarioModuloID { get; set; }
        public string IDUsuario { get; set; }
        public string IDRol { get; set; }
    }
}