using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCHelpDesk.Permisos;
using System.Web.Routing;
using MVCHelpDesk.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MVCHelpDesk.Attribute
{
    public class PermisoAttribute : ActionFilterAttribute
    {
        public  AllPermisos permisos { get; set; }
        ApplicationDbContext db = new ApplicationDbContext();
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            if (!PermisoByRol(this.permisos) && !PermisoByUser(this.permisos))
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    controller = "Home",
                    action = "Index"
                }));
            }
        }
        public bool PermisoByRol(AllPermisos valor)
        {
            int intValor = (int)valor;

            var permisoRol = (from rol in db.Roles
                              join pByRol in db.PermisoPorRol on rol.Id equals pByRol.RoleID
                              join p in db.Permisos on pByRol.PermisoID equals p.PermisoID
                              where p.PermisoID== intValor
                              select new { permisos = p.PermisoID }).Any();

            return permisoRol;
        }
        public bool PermisoByUser(AllPermisos valor)
        {
            int intValor = (int)valor;
            var s = (from user in db.Users
                     join permisoUsuario in db.PermisosPorUsuarios on user.Id equals permisoUsuario.UsuarioID
                     join per in db.Permisos on permisoUsuario.PermisoID equals per.PermisoID
                     where permisoUsuario.PermisoID == intValor
                     select new { permisos = per.PermisoID }).Any();

            return s;
        }
    }
}

  //public class FrontUser
  //  {
  //      public static bool TienePermiso(Permisos valor)
  //      {
  //          MVCIdentityEntities db = new MVCIdentityEntities();
  //          //  var usuario = FrontUser.Get();

  //          int intValor = (int)valor;

  //          var s = (from user in db.AspNetUsers
  //                   join rolUser in db.AspNetUserRoles on user.Id equals rolUser.UserId
  //                   join permisosRol in db.PermisoPorRol on rolUser.RoleId equals permisosRol.RoleId
  //                   join per in db.Permiso on permisosRol.PermisoID equals per.PermisoID
  //                   where permisosRol.PermisoID == intValor
  //                   select new { permisos = per.PermisoID }).Any();


  //            usuario = ctx.Usuario.Include("Rol")
  //                                     .Include("Rol.Permiso")
  //                                     .Where(x => x.id == id).SingleOrDefault();

//            return s;
//        }
//        public static bool PermisoPorUser(Permisos valor)
//{
//    MVCIdentityEntities db = new MVCIdentityEntities();
    //  var usuario = FrontUser.Get();

    //int intValor = (int)valor;

    //var s = (from user in db.AspNetUsers
    //         join permisoUsuario in db.PermisoPorUser on user.Id equals permisoUsuario.UserId
    //         join per in db.Permiso on permisoUsuario.PermisoID equals per.PermisoID
    //         where permisoUsuario.PermisoID == intValor
    //         select new { permisos = per.PermisoID }).Any();


    /*   usuario = ctx.Usuario.Include("Rol")
                               .Include("Rol.Permiso")
                               .Where(x => x.id == id).SingleOrDefault();*/

//    return s;
//}
//    }
