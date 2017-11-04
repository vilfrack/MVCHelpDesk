using MVCHelpDesk.Models;
using MVCHelpDesk.Permisos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MVCHelpDesk.Attribute
{
    public class ModuloAttribute : ActionFilterAttribute
    {
        public AllModulos modulo { get; set; }
        public AllPermisos permisos { get; set; }
        ApplicationDbContext db = new ApplicationDbContext();

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            if (!PermisoByRol() && !PermisoByUser())
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    controller = "Account",
                    action = "Login"
                }));
            }
        }
        public bool PermisoByRol()
        {
            int intModulo = (int)modulo;
            int intPermisos = (int)permisos;

            var permisoRol = (from rol in db.Roles
                              join pByRol in db.PermisoPorRol on rol.Id equals pByRol.RoleID
                              join p in db.Permisos on pByRol.PermisoID equals p.PermisoID
                              join m in db.Modulos on pByRol.ModuloID equals m.ModuloID
                              where m.ModuloID == intModulo && pByRol.PermisoID== intPermisos
                              select new { permisos = pByRol.PermisoID }).Any();

            return permisoRol;
        }
        public bool PermisoByUser()
        {
            int intModulo = (int)modulo;
            int intPermisos = (int)permisos;

            var VarPermisoUsuario = (from user in db.Users
                                     join permisoUsuario in db.PermisosPorUsuarios on user.Id equals permisoUsuario.UsuarioID
                                     join per in db.Permisos on permisoUsuario.PermisoID equals per.PermisoID
                                     join m in db.Modulos on permisoUsuario.ModuloID equals m.ModuloID
                                     where m.ModuloID == intModulo && permisoUsuario.PermisoID == intPermisos
                                     select new { permisos = permisoUsuario.PermisoID }).Any();

            return VarPermisoUsuario;
        }
    }
}