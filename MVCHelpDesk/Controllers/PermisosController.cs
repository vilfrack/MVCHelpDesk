using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVCHelpDesk.Models;
using MVCHelpDesk.Attribute;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Security.Claims;
using Microsoft.AspNet.Identity.Owin;
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
            return View();
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

            return PartialView(viewPermisos.Where(w => w.RolID == DropRoles || w.UsuarioID == DropUsuario).ToList());
        }
        /*
         */
        //[HttpPost]
        //public ActionResult Details(List<ViewGetPermisos> list)
        //{

        //    return View();
        //}

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
        // GET: Permisos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Permisos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PermisoID,Modulo,Descripcion")] Models.Permisos permisos)
        {
            if (ModelState.IsValid)
            {
                db.Permisos.Add(permisos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(permisos);
        }

        // GET: Permisos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Models.Permisos permisos = db.Permisos.Find(id);
            if (permisos == null)
            {
                return HttpNotFound();
            }
            return View(permisos);
        }

        // POST: Permisos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PermisoID,Modulo,Descripcion")] Models.Permisos permisos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(permisos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(permisos);
        }

        // GET: Permisos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Models.Permisos permisos = db.Permisos.Find(id);
            if (permisos == null)
            {
                return HttpNotFound();
            }
            return View(permisos);
        }

        // POST: Permisos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Models.Permisos permisos = db.Permisos.Find(id);
            db.Permisos.Remove(permisos);
            db.SaveChanges();
            return RedirectToAction("Index");
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
