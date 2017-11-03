using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using MVCHelpDesk.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using MVCHelpDesk.Helper;
using System.Data.Entity.Validation;
using MVCHelpDesk.ViewModel;
namespace MVCHelpDesk.Controllers
{
    // [Authorize, ModuloAttribute(modulo = Permisos.AllModulos.Permiso, permisos = Permisos.AllPermisos.ver)]
    public class PermisosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private UserIdentity userIdentity = new UserIdentity();
        private RolIdentity rolIdentity = new RolIdentity();
        public ActionResult Index()
        {
            /*CREAMOS LOS DropDown*/
            ViewBag.DropRoles = new SelectList(db.Roles.ToList(), "Id", "Name");
            ViewBag.DropUsuario = new SelectList(db.Users.ToList(), "Id", "UserName");
            ViewBag.Tab = "1";

            return View();

        }
        [HttpPost]
        public ActionResult Index(string DropRoles, string DropUsuario)
        {


            List<dynamic> listPermisosRolUser = new List<dynamic>();
            List<ViewPermisos> viewPermisos = new List<ViewPermisos>();
            if (DropRoles != string.Empty)
            {
                viewPermisos.AddRange(GeRol(DropRoles));
                ViewBag.Tab = "1";
            }
            if (DropUsuario != string.Empty)
            {
                viewPermisos.AddRange(GetUsuario(DropUsuario));
                ViewBag.Tab = "2";
            }
            ViewBag.DropRoles = new SelectList(db.Roles.ToList(), "Id", "Name");
            ViewBag.DropUsuario = new SelectList(db.Users.ToList(), "Id", "UserName");
            return View(viewPermisos.ToList());

        }
        [HttpPost]
        public JsonResult Save(List<ViewGetPermisos> list) {
                if (list==null)
                {
                    return Json(new { success = false, JsonRequestBehavior.AllowGet });
                }

                foreach (var varRoles in list)
                {
                    if (varRoles.IDRol != null && varRoles.check == true)
                    {
                        PermisoPorRol permisoRol = new PermisoPorRol();
                        permisoRol.ModuloID = Convert.ToInt32(varRoles.moduloID);
                        permisoRol.PermisoID = Convert.ToInt32(varRoles.PermisoID);
                        permisoRol.RoleID = varRoles.IDRol;
                        db.PermisoPorRol.Add(permisoRol);
                        db.SaveChanges();
                    }
                    else
                    {
                        if (varRoles.IDRol == null && varRoles.check == false)
                        {
                            PermisoPorRol permisoRol = db.PermisoPorRol.Where(w => w.ModuloID == varRoles.moduloID && w.PermisoID == varRoles.PermisoID).SingleOrDefault();
                            if (permisoRol != null)
                            {
                                db.PermisoPorRol.Remove(permisoRol);
                                db.SaveChanges();
                            }

                        }
                    }

                }
                foreach (var varUsuario in list)
                {
                    if (varUsuario.IDUsuario != null && varUsuario.check == true)
                    {
                        PermisosPorUsuarios permisoUsuario = new PermisosPorUsuarios();
                        permisoUsuario.ModuloID = Convert.ToInt32(varUsuario.UsuarioModuloID);
                        permisoUsuario.PermisoID = Convert.ToInt32(varUsuario.UsuarioPermisoID);
                        permisoUsuario.UsuarioID = varUsuario.IDUsuario;
                        db.PermisosPorUsuarios.Add(permisoUsuario);
                        db.SaveChanges();
                    }
                    else
                    {
                        if (varUsuario.check == false)
                        {
                            PermisosPorUsuarios permisoUsuario = db.PermisosPorUsuarios.Where(w => w.ModuloID == varUsuario.UsuarioModuloID && w.PermisoID == varUsuario.UsuarioPermisoID && w.UsuarioID == varUsuario.IDUsuario).SingleOrDefault();
                            if (permisoUsuario != null)
                            {
                                db.PermisosPorUsuarios.Remove(permisoUsuario);
                                db.SaveChanges();
                            }
                        }
                    }
                }
                return Json(new { success = true, JsonRequestBehavior.AllowGet });
        }
        public List<ViewPermisos> GetUsuario(string usuario) {
            List<IdentityRole> ListRolByUser = rolIdentity.GetRolByUser();

            List<dynamic> listPermisosRolUser = new List<dynamic>();
            // PermisosPorRoles
            foreach (var item in ListRolByUser)
            {
                var PermisosRolUser = (from p in db.Permisos
                                       join pU in db.PermisosPorUsuarios on p.PermisoID equals pU.PermisoID
                                       //SE ALMACENA EN UNA VARIABLE TEMPORAL tempPorUsuarios
                                       into tempPorUsuarios
                                       //SE HACE EL SEGUNDO JOIN
                                       join pR in db.PermisoPorRol on p.PermisoID equals pR.PermisoID
                                       //SE ALMACENA EN UNA VARIABLE TEMPORAL tempPorRol
                                       into tempPorRol
                                       //SE HACE LA CONSULTA tempPorUsuarios
                                       from lastPorUsuarios in tempPorUsuarios.DefaultIfEmpty()
                                           // EL RESULTADO DE lastPorUsuarios SE RELACIONA CON MODULOS
                                       join mU in db.Modulos on lastPorUsuarios.ModuloID equals mU.ModuloID
                                       //SE HACE LA CONSULTA tempPorRol
                                       from lastPorRol in tempPorRol.DefaultIfEmpty()
                                       join mR in db.Modulos on lastPorRol.ModuloID equals mR.ModuloID
                                       //se hacen los where
                                       where lastPorUsuarios.UsuarioID == usuario
                                       select new
                                       {
                                           PermisoID = p.PermisoID,
                                           Descripcion = p.Descripcion,
                                           UsuarioID = lastPorUsuarios.UsuarioID == null ? default(string) : lastPorUsuarios.UsuarioID,
                                           RolID = lastPorRol.RoleID == null ? default(string) : lastPorRol.RoleID,
                                           //chek para validar si tienen permisos o no
                                           CheekRol = lastPorRol.RoleID == null ? false : true,
                                           CheekUsuarios = lastPorUsuarios.UsuarioID == null ? false : true,
                                           //MODULOS POR USUARIO Y POR ROL
                                           ModuloIDPorUsuarios = lastPorUsuarios.ModuloID.Equals(null) ? default(int) : lastPorUsuarios.ModuloID,
                                           ModuloIDPorRol = lastPorRol.ModuloID.Equals(null) ? default(int) : lastPorRol.ModuloID,
                                           ModuloRolDes = mR.Descripcion,
                                           ModuloUsuDes = mU.Descripcion,
                                           ModuloID = mU.ModuloID
                                       }).ToList();

                var Varmodulos = db.Modulos.ToList();

                var VarModulosPermisos = (from modulos in Varmodulos
                                          join pRu in PermisosRolUser on modulos.ModuloID equals pRu.ModuloID into temp
                                          from tempModulos in temp.DefaultIfEmpty()
                                          select new
                                          {
                                              Descripcion = modulos.Descripcion,
                                              ID = modulos.ModuloID,
                                              Permisos = tempModulos,
                                          }).ToList();

                listPermisosRolUser.AddRange(VarModulosPermisos);
            }
            List<ViewPermisos> viewPermisos = new List<ViewPermisos>();
            var varPermisos = db.Permisos.ToList();
            foreach (var item in listPermisosRolUser)
            {
                foreach (var itemPermiso in varPermisos)
                {
                    viewPermisos.Add(new ViewPermisos
                    {
                        ModuloID = item.ID,
                        ModuloDescripcion = item.Descripcion,
                        CheekRol = false,
                        CheekUsuarios = false,
                        PermisoID = itemPermiso.PermisoID,
                        PermisoDescripcion = itemPermiso.Descripcion,
                        RolID = string.Empty,
                        UsuarioID = string.Empty
                    });
                }
            }
            var PermisosUser = (from p in db.Permisos
                                   join pU in db.PermisosPorUsuarios on p.PermisoID equals pU.PermisoID
                                   //SE ALMACENA EN UNA VARIABLE TEMPORAL tempPorUsuarios
                                   into tempPorUsuarios
                                   ////SE HACE LA CONSULTA tempPorUsuarios
                                   from lastPorUsuarios in tempPorUsuarios.DefaultIfEmpty()
                                       // EL RESULTADO DE lastPorUsuarios SE RELACIONA CON MODULOS
                                   join mU in db.Modulos on lastPorUsuarios.ModuloID equals mU.ModuloID
                                   where lastPorUsuarios.UsuarioID == usuario
                                   select new
                                   {
                                       PermisoID = p.PermisoID,
                                       Descripcion = p.Descripcion,
                                       UsuarioID = lastPorUsuarios.UsuarioID == null ? default(string) : lastPorUsuarios.UsuarioID,
                                       CheekUsuarios = lastPorUsuarios.UsuarioID == null ? false : true,
                                       ModuloIDPorUsuarios = lastPorUsuarios.ModuloID.Equals(null) ? default(int) : lastPorUsuarios.ModuloID,
                                       ModuloUsuDes = mU.Descripcion,
                                       ModuloID = mU.ModuloID
                                   }).ToList();


            var indicador = PermisosUser.Where(w => w.CheekUsuarios == true).Select(d => new {
                CheekUsuarios = d.CheekUsuarios,
                PermisoID = d.PermisoID,
                ModuloID = d.ModuloID
            }).Distinct().ToList();

            for (int i = 0; i < indicador.Count; i++)
            {
                foreach (var item in viewPermisos.Where(d => d.PermisoID == indicador[i].PermisoID && d.ModuloID == indicador[i].ModuloID).ToList())
                {
                    item.CheekUsuarios = true;
                }
            }
            /***************************************************/

            ViewBag.Permisos = (from s in viewPermisos
                                select new
                                {
                                    PermisoID = s.PermisoID,
                                    PermisoDescripcion = s.PermisoDescripcion
                                }).Distinct();

            ViewBag.modulo = (from s in viewPermisos
                              select new
                              {
                                  ModuloID = s.ModuloID,
                                  ModuloDescripcion = s.ModuloDescripcion
                              }).Distinct().ToList();
            /*CREAMOS LOS DropDown*/
            ViewBag.DropRoles = new SelectList(db.Roles.ToList(), "Id", "Name");
            ViewBag.DropUsuario = new SelectList(db.Users.ToList(), "Id", "UserName");
            ViewBag.Tab = "1";


            return viewPermisos.ToList();
        }
        public List<ViewPermisos> GeRol(string Rol)
        {
            // SE OBTIENE LOS ROLES POR USUARIO POR MEDIO DE LA CLASE QUE CREAMOS
            List<IdentityRole> ListRolByUser = rolIdentity.GetRolByUser();

            List<dynamic> listPermisosRolUser = new List<dynamic>();
            // PermisosPorRoles
            foreach (var item in ListRolByUser)
            {
                var PermisosRolUser = (from p in db.Permisos
                                       join pU in db.PermisosPorUsuarios on p.PermisoID equals pU.PermisoID
                                       //SE ALMACENA EN UNA VARIABLE TEMPORAL tempPorUsuarios
                                       into tempPorUsuarios
                                       //SE HACE EL SEGUNDO JOIN
                                       join pR in db.PermisoPorRol on p.PermisoID equals pR.PermisoID
                                       //SE ALMACENA EN UNA VARIABLE TEMPORAL tempPorRol
                                       into tempPorRol
                                       //SE HACE LA CONSULTA tempPorUsuarios
                                       from lastPorUsuarios in tempPorUsuarios.DefaultIfEmpty()
                                           // EL RESULTADO DE lastPorUsuarios SE RELACIONA CON MODULOS
                                       join mU in db.Modulos on lastPorUsuarios.ModuloID equals mU.ModuloID
                                       //SE HACE LA CONSULTA tempPorRol
                                       from lastPorRol in tempPorRol.DefaultIfEmpty()
                                       join mR in db.Modulos on lastPorRol.ModuloID equals mR.ModuloID
                                       //se hacen los where
                                       where lastPorRol.RoleID == Rol
                                       select new
                                       {
                                           PermisoID = p.PermisoID,
                                           Descripcion = p.Descripcion,
                                           UsuarioID = lastPorUsuarios.UsuarioID == null ? default(string) : lastPorUsuarios.UsuarioID,
                                           RolID = lastPorRol.RoleID == null ? default(string) : lastPorRol.RoleID,
                                           //chek para validar si tienen permisos o no
                                           CheekRol = lastPorRol.RoleID == null ? false : true,
                                           CheekUsuarios = lastPorUsuarios.UsuarioID == null ? false : true,
                                           //MODULOS POR USUARIO Y POR ROL
                                           ModuloIDPorUsuarios = lastPorUsuarios.ModuloID.Equals(null) ? default(int) : lastPorUsuarios.ModuloID,
                                           ModuloIDPorRol = lastPorRol.ModuloID.Equals(null) ? default(int) : lastPorRol.ModuloID,
                                           ModuloRolDes = mR.Descripcion,
                                           ModuloUsuDes = mU.Descripcion,
                                           ModuloID = mU.ModuloID
                                       }).ToList();

                var Varmodulos = db.Modulos.ToList();

                var VarModulosPermisos = (from modulos in Varmodulos
                                          join pRu in PermisosRolUser on modulos.ModuloID equals pRu.ModuloID into temp
                                          from tempModulos in temp.DefaultIfEmpty()
                                          select new
                                          {
                                              Descripcion = modulos.Descripcion,
                                              ID = modulos.ModuloID,
                                              Permisos = tempModulos,
                                          }).ToList();

                listPermisosRolUser.AddRange(VarModulosPermisos);
            }
            List<ViewPermisos> viewPermisos = new List<ViewPermisos>();
            var varPermisos = db.Permisos.ToList();
            foreach (var item in listPermisosRolUser)
            {
                foreach (var itemPermiso in varPermisos)
                {
                    viewPermisos.Add(new ViewPermisos
                    {
                        ModuloID = item.ID,
                        ModuloDescripcion = item.Descripcion,
                        CheekRol = false,
                        CheekUsuarios = false,
                        PermisoID = itemPermiso.PermisoID,
                        PermisoDescripcion = itemPermiso.Descripcion,
                        RolID = string.Empty,
                        UsuarioID = string.Empty
                    });
                }
            }
            var PermisosRol = (from p in db.Permisos
                                   join pR in db.PermisoPorRol on p.PermisoID equals pR.PermisoID
                                   into tempPorRol
                                   from lastPorRol in tempPorRol.DefaultIfEmpty()
                                   join mR in db.Modulos on lastPorRol.ModuloID equals mR.ModuloID
                                   into tempModulo
                                   from lastModulo in tempModulo.DefaultIfEmpty()
                                   where lastPorRol.RoleID == Rol
                                   select new
                                   {
                                       PermisoID = p.PermisoID,
                                       Descripcion = p.Descripcion,
                                       RolID = lastPorRol.RoleID == null ? default(string) : lastPorRol.RoleID,
                                       //chek para validar si tienen permisos o no
                                       CheekRol = lastPorRol.RoleID == null ? false : true,
                                       //MODULOS POR USUARIO Y POR ROL
                                       ModuloIDPorRol = lastPorRol.ModuloID.Equals(null) ? default(int) : lastPorRol.ModuloID,
                                       ModuloRolDes = lastModulo.Descripcion,
                                       ModuloID = lastPorRol.ModuloID
                                   }).ToList();


            var indicador = PermisosRol.Where(w=>w.CheekRol==true).Select(d => new { CheekRol = d.CheekRol,
                                                          PermisoID =d.PermisoID,
                                                          ModuloID = d.ModuloID}).Distinct().ToList();

            for (int i = 0; i < indicador.Count; i++)
            {
                foreach (var item in viewPermisos.Where(d => d.PermisoID == indicador[i].PermisoID && d.ModuloID== indicador[i].ModuloID).ToList())
                {
                    item.CheekRol = true;
                }
            }
                /***************************************************/

                ViewBag.Permisos = (from s in viewPermisos
                                select new
                                {
                                    PermisoID = s.PermisoID,
                                    PermisoDescripcion = s.PermisoDescripcion
                                }).Distinct();

            ViewBag.modulo = (from s in viewPermisos
                              select new
                              {
                                  ModuloID = s.ModuloID,
                                  ModuloDescripcion = s.ModuloDescripcion
                              }).Distinct().ToList();
            /*CREAMOS LOS DropDown*/
            ViewBag.DropRoles = new SelectList(db.Roles.ToList(), "Id", "Name");
            ViewBag.DropUsuario = new SelectList(db.Users.ToList(), "Id", "UserName");
            ViewBag.Tab = "1";


            return viewPermisos.ToList();

        }
        // GET: Permisos/Details/5
        public ActionResult Details()
        {
            /***************************************************/
            // SE OBTIENE EL IDUSER POR MEDIO DE LA CLASE QUE CREAMOS
            string idUser = userIdentity.GetIdUser();
            // SE OBTIENE LOS ROLES POR USUARIO POR MEDIO DE LA CLASE QUE CREAMOS
            List<IdentityRole> ListRolByUser = rolIdentity.GetRolByUser();

            List<dynamic> listPermisosRolUser = new List<dynamic>();



            // PermisosPorRoles
            foreach (var item in ListRolByUser)
            {
                var PermisosRolUser = (from p in db.Permisos
                                       join pU in db.PermisosPorUsuarios on p.PermisoID equals pU.PermisoID
                                       //SE ALMACENA EN UNA VARIABLE TEMPORAL tempPorUsuarios
                                       into tempPorUsuarios
                                       //SE HACE EL SEGUNDO JOIN
                                       join pR in db.PermisoPorRol on p.PermisoID equals pR.PermisoID
                                       //SE ALMACENA EN UNA VARIABLE TEMPORAL tempPorRol
                                       into tempPorRol
                                       //SE HACE LA CONSULTA tempPorUsuarios
                                       from lastPorUsuarios in tempPorUsuarios.DefaultIfEmpty()
                                           // EL RESULTADO DE lastPorUsuarios SE RELACIONA CON MODULOS
                                       join mU in db.Modulos on lastPorUsuarios.ModuloID equals mU.ModuloID
                                       //SE HACE LA CONSULTA tempPorRol
                                       from lastPorRol in tempPorRol.DefaultIfEmpty()
                                       join mR in db.Modulos on lastPorRol.ModuloID equals mR.ModuloID
                                       //se hacen los where
                                       where lastPorUsuarios.UsuarioID == idUser || lastPorRol.RoleID == item.Id
                                       select new
                                       {
                                           PermisoID = p.PermisoID,
                                           Descripcion = p.Descripcion,
                                           UsuarioID = lastPorUsuarios.UsuarioID == null ? default(string) : lastPorUsuarios.UsuarioID,
                                           RolID = lastPorRol.RoleID == null ? default(string) : lastPorRol.RoleID,
                                           //chek para validar si tienen permisos o no
                                           CheekRol = lastPorRol.RoleID == null ? false : true,
                                           CheekUsuarios = lastPorUsuarios.UsuarioID == null ? false : true,
                                           //MODULOS POR USUARIO Y POR ROL
                                           ModuloIDPorUsuarios = lastPorUsuarios.ModuloID.Equals(null) ? default(int) : lastPorUsuarios.ModuloID,
                                           ModuloIDPorRol = lastPorRol.ModuloID.Equals(null) ? default(int) : lastPorRol.ModuloID,
                                           ModuloRolDes = mR.Descripcion,
                                           ModuloUsuDes = mU.Descripcion,
                                           ModuloID = mU.ModuloID
                                       }).ToList();

                var Varmodulos = db.Modulos.ToList();

                var VarModulosPermisos = (from modulos in Varmodulos
                                          join pRu in PermisosRolUser on modulos.ModuloID equals pRu.ModuloID into temp
                                          from tempModulos in temp.DefaultIfEmpty()
                                          select new
                                          {
                                              Descripcion = modulos.Descripcion,
                                              ID = modulos.ModuloID,
                                              Permisos = tempModulos,
                                          }).ToList();

                listPermisosRolUser.AddRange(VarModulosPermisos);
            }
            List<ViewPermisos> viewPermisos = new List<ViewPermisos>();
            var varPermisos = db.Permisos.ToList();
            foreach (var item in listPermisosRolUser)
            {
                if (item.Permisos == null)
                {
                    foreach (var itemPermiso in varPermisos)
                    {
                        viewPermisos.Add(new ViewPermisos
                        {
                            ModuloID = item.ID,
                            ModuloDescripcion = item.Descripcion,
                            CheekRol = false,
                            CheekUsuarios = false,
                            PermisoID = itemPermiso.PermisoID,
                            PermisoDescripcion = itemPermiso.Descripcion,
                            RolID = string.Empty,
                            UsuarioID = string.Empty
                        });
                    }

                }
                else
                {
                    viewPermisos.Add(new ViewPermisos
                    {
                        ModuloID = item.ID,
                        ModuloDescripcion = item.Descripcion,
                        CheekRol = item.Permisos.CheekRol == null ? false : true,
                        CheekUsuarios = item.Permisos.CheekUsuarios == null ? false : true,
                        PermisoID = item.Permisos.PermisoID,
                        PermisoDescripcion = item.Permisos.Descripcion,
                        RolID = item.Permisos.RolID,
                        UsuarioID = item.Permisos.UsuarioID
                    });
                }
            }

            /***************************************************/

            ViewBag.Permisos = (from s in viewPermisos
                                select new
                                {
                                    PermisoID = s.PermisoID,
                                    PermisoDescripcion = s.PermisoDescripcion
                                }).Distinct();

            ViewBag.modulo = (from s in viewPermisos
                              select new
                              {
                                  ModuloID = s.ModuloID,
                                  ModuloDescripcion = s.ModuloDescripcion
                              }).Distinct().ToList();
            /*CREAMOS LOS DropDown*/
            ViewBag.DropRoles = new SelectList(db.Roles.ToList(), "Id", "Name");
            ViewBag.DropUsuario = new SelectList(db.Users.ToList(), "Id", "UserName");



            return PartialView(viewPermisos);
        }
        [HttpPost]
        public ActionResult Details(string DropRoles, string DropUsuario)
        {
            /***************************************************/
            /***************************************************/
            /***************************************************/
            // SE OBTIENE EL IDUSER POR MEDIO DE LA CLASE QUE CREAMOS
            string idUser = userIdentity.GetIdUser();
            // SE OBTIENE LOS ROLES POR USUARIO POR MEDIO DE LA CLASE QUE CREAMOS
            List<IdentityRole> ListRolByUser = rolIdentity.GetRolByUser();

            List<dynamic> listPermisosRolUser = new List<dynamic>();



            // PermisosPorRoles
            foreach (var item in ListRolByUser)
            {
                var PermisosRolUser = (from p in db.Permisos
                                       join pU in db.PermisosPorUsuarios on p.PermisoID equals pU.PermisoID
                                       //SE ALMACENA EN UNA VARIABLE TEMPORAL tempPorUsuarios
                                       into tempPorUsuarios
                                       //SE HACE EL SEGUNDO JOIN
                                       join pR in db.PermisoPorRol on p.PermisoID equals pR.PermisoID
                                       //SE ALMACENA EN UNA VARIABLE TEMPORAL tempPorRol
                                       into tempPorRol
                                       //SE HACE LA CONSULTA tempPorUsuarios
                                       from lastPorUsuarios in tempPorUsuarios.DefaultIfEmpty()
                                           // EL RESULTADO DE lastPorUsuarios SE RELACIONA CON MODULOS
                                       join mU in db.Modulos on lastPorUsuarios.ModuloID equals mU.ModuloID
                                       //SE HACE LA CONSULTA tempPorRol
                                       from lastPorRol in tempPorRol.DefaultIfEmpty()
                                       join mR in db.Modulos on lastPorRol.ModuloID equals mR.ModuloID
                                       //se hacen los where
                                       where lastPorUsuarios.UsuarioID == idUser || lastPorRol.RoleID == item.Id
                                       select new
                                       {
                                           PermisoID = p.PermisoID,
                                           Descripcion = p.Descripcion,
                                           UsuarioID = lastPorUsuarios.UsuarioID == null ? default(string) : lastPorUsuarios.UsuarioID,
                                           RolID = lastPorRol.RoleID == null ? default(string) : lastPorRol.RoleID,
                                           //chek para validar si tienen permisos o no
                                           CheekRol = lastPorRol.RoleID == null ? false : true,
                                           CheekUsuarios = lastPorUsuarios.UsuarioID == null ? false : true,
                                           //MODULOS POR USUARIO Y POR ROL
                                           ModuloIDPorUsuarios = lastPorUsuarios.ModuloID.Equals(null) ? default(int) : lastPorUsuarios.ModuloID,
                                           ModuloIDPorRol = lastPorRol.ModuloID.Equals(null) ? default(int) : lastPorRol.ModuloID,
                                           ModuloRolDes = mR.Descripcion,
                                           ModuloUsuDes = mU.Descripcion,
                                           ModuloID = mU.ModuloID
                                       }).ToList();

                var Varmodulos = db.Modulos.ToList();

                var VarModulosPermisos = (from modulos in Varmodulos
                                          join pRu in PermisosRolUser on modulos.ModuloID equals pRu.ModuloID into temp
                                          from tempModulos in temp.DefaultIfEmpty()
                                          select new
                                          {
                                              Descripcion = modulos.Descripcion,
                                              ID = modulos.ModuloID,
                                              Permisos = tempModulos,
                                          }).ToList();

                listPermisosRolUser.AddRange(VarModulosPermisos);
            }
            List<ViewPermisos> viewPermisos = new List<ViewPermisos>();
            var varPermisos = db.Permisos.ToList();
            foreach (var item in listPermisosRolUser)
            {
                if (item.Permisos == null)
                {
                    foreach (var itemPermiso in varPermisos)
                    {
                        viewPermisos.Add(new ViewPermisos
                        {
                            ModuloID = item.ID,
                            ModuloDescripcion = item.Descripcion,
                            CheekRol = false,
                            CheekUsuarios = false,
                            PermisoID = itemPermiso.PermisoID,
                            PermisoDescripcion = itemPermiso.Descripcion,
                            RolID = string.Empty,
                            UsuarioID = string.Empty
                        });
                    }

                }
                else
                {
                    viewPermisos.Add(new ViewPermisos
                    {
                        ModuloID = item.ID,
                        ModuloDescripcion = item.Descripcion,
                        CheekRol = item.Permisos.CheekRol == null ? false : true,
                        CheekUsuarios = item.Permisos.CheekUsuarios == null ? false : true,
                        PermisoID = item.Permisos.PermisoID,
                        PermisoDescripcion = item.Permisos.Descripcion,
                        RolID = item.Permisos.RolID,
                        UsuarioID = item.Permisos.UsuarioID
                    });
                }
            }
            /***************************************************/
            /***************************************************/
            /***************************************************/

            ViewBag.DropRoles = new SelectList(db.Roles.ToList(), "Id", "Name");
            ViewBag.DropUsuario = new SelectList(db.Users.ToList(), "Id", "UserName");

            //  return PartialView(viewPermisos.Where(w => w.RolID == DropRoles || w.UsuarioID == DropUsuario).ToList());
            return RedirectToAction("Index");
        }
        public JsonResult get()
        {
            try
            {
                // SE OBTIENE EL IDUSER POR MEDIO DE LA CLASE QUE CREAMOS
                string idUser = userIdentity.GetIdUser();
                // SE OBTIENE LOS ROLES POR USUARIO POR MEDIO DE LA CLASE QUE CREAMOS
                List<IdentityRole> ListRolByUser = rolIdentity.GetRolByUser();

                List<dynamic> listPermisosRolUser = new List<dynamic>();



                // PermisosPorRoles
                foreach (var item in ListRolByUser)
                {
                    var PermisosRolUser = (from p in db.Permisos
                                           join pU in db.PermisosPorUsuarios on p.PermisoID equals pU.PermisoID
                                           //SE ALMACENA EN UNA VARIABLE TEMPORAL tempPorUsuarios
                                           into tempPorUsuarios
                                           //SE HACE EL SEGUNDO JOIN
                                           join pR in db.PermisoPorRol on p.PermisoID equals pR.PermisoID
                                           //SE ALMACENA EN UNA VARIABLE TEMPORAL tempPorRol
                                           into tempPorRol
                                           //SE HACE LA CONSULTA tempPorUsuarios
                                           from lastPorUsuarios in tempPorUsuarios.DefaultIfEmpty()
                                               // EL RESULTADO DE lastPorUsuarios SE RELACIONA CON MODULOS
                                           join mU in db.Modulos on lastPorUsuarios.ModuloID equals mU.ModuloID
                                           //SE HACE LA CONSULTA tempPorRol
                                           from lastPorRol in tempPorRol.DefaultIfEmpty()
                                           join mR in db.Modulos on lastPorRol.ModuloID equals mR.ModuloID
                                           //se hacen los where
                                           where lastPorUsuarios.UsuarioID == idUser || lastPorRol.RoleID == item.Id
                                           select new
                                           {
                                               PermisoID = p.PermisoID,
                                               Descripcion = p.Descripcion,
                                               UsuarioID = lastPorUsuarios.UsuarioID == null ? default(string) : lastPorUsuarios.UsuarioID,
                                               RolID = lastPorRol.RoleID == null ? default(string) : lastPorRol.RoleID,
                                               //chek para validar si tienen permisos o no
                                               CheekRol = lastPorRol.RoleID == null ? false : true,
                                               CheekUsuarios = lastPorUsuarios.UsuarioID == null ? false : true,
                                               //MODULOS POR USUARIO Y POR ROL
                                               ModuloIDPorUsuarios = lastPorUsuarios.ModuloID.Equals(null) ? default(int) : lastPorUsuarios.ModuloID,
                                               ModuloIDPorRol = lastPorRol.ModuloID.Equals(null) ? default(int) : lastPorRol.ModuloID,
                                               ModuloRolDes = mR.Descripcion,
                                               ModuloUsuDes = mU.Descripcion,
                                               ModuloID = mU.ModuloID
                                           }).ToList();

                    var Varmodulos = db.Modulos.ToList();

                    var VarModulosPermisos = (from modulos in Varmodulos
                                              join pRu in PermisosRolUser on modulos.ModuloID equals pRu.ModuloID into temp
                                              from tempModulos in temp.DefaultIfEmpty()
                                              select new
                                              {
                                                  Descripcion = modulos.Descripcion,
                                                  ID = modulos.ModuloID,
                                                  Permisos = tempModulos,
                                              }).ToList();

                    listPermisosRolUser.AddRange(VarModulosPermisos);
                }
                List<ViewPermisos> viewPermisos = new List<ViewPermisos>();
                var varPermisos = db.Permisos.ToList();
                foreach (var item in listPermisosRolUser)
                {
                    if (item.Permisos==null)
                    {
                        foreach (var itemPermiso in varPermisos)
                        {
                            viewPermisos.Add(new ViewPermisos
                            {
                                ModuloID = item.ID,
                                ModuloDescripcion = item.Descripcion,
                                CheekRol = false,
                                CheekUsuarios = false,
                                PermisoID = itemPermiso.PermisoID,
                                PermisoDescripcion = itemPermiso.Descripcion,
                                RolID = string.Empty,
                                UsuarioID = idUser
                            });
                        }

                    }
                    else
                    {
                        viewPermisos.Add(new ViewPermisos
                        {
                            ModuloID = item.ID,
                            ModuloDescripcion = item.Descripcion,
                            CheekRol = item.Permisos.CheekRol == null ? false : true,
                            CheekUsuarios = item.Permisos.CheekUsuarios == null ? false : true,
                            PermisoID = item.Permisos.PermisoID,
                            PermisoDescripcion = item.Permisos.Descripcion,
                            RolID = item.Permisos.RolID,
                            UsuarioID = item.Permisos.UsuarioID
                        });
                    }
                }
                var jsonData = new
                {
                    data = viewPermisos
                };
                return Json(jsonData, JsonRequestBehavior.AllowGet);
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                var jsonData = new
                {
                    data = e
                };
                return Json(jsonData, JsonRequestBehavior.AllowGet);
            }
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


    }

}
