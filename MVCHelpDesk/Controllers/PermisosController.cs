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
            return PartialView();
        }
        public JsonResult get()
        {
            // SE OBTIENE EL IDUSER POR MEDIO DE LA CLASE QUE CREAMOS
            string idUser = userIdentity.GetIdUser();
            // SE OBTIENE LOS ROLES POR USUARIO POR MEDIO DE LA CLASE QUE CREAMOS
            List<IdentityRole> ListRolByUser = rolIdentity.GetRolByUser();

            // ES NECESARIO PONER EXACTAMENTE LOS CAMPOS A EXTRAER PORQUE SI NO DA ERROR
            var varPermisosPorUsuarios = (from p in db.Permisos
                         join pU in db.PermisosPorUsuarios on p.PermisoID equals pU.PermisoID
                         into temp
                         from last in temp.DefaultIfEmpty()
                         where last.UsuarioID == idUser
                         select new
                         {
                             PermisoID = p.PermisoID,
                             Descripcion = p.Descripcion,
                             UsuarioID = last.UsuarioID == null ? false : true,
                             ModuloID = last.ModuloID.Equals(null) ? false : true
                         }).ToList();


            List<object> listPermisosByRol = new List<object>();
            foreach (var item in ListRolByUser)
            {
                var varPermisosByRol = (from p in db.Permisos
                          join pU in db.PermisoPorRol on p.PermisoID equals pU.PermisoID
                          into temp
                          from last in temp.DefaultIfEmpty()
                          where last.RoleID == item.Id
                          select new
                          {
                              PermisoID = p.PermisoID,
                              Descripcion = p.Descripcion,
                              CheekRol = last.RoleID == null ? false : true,
                              CheekModulo = last.ModuloID.Equals(null) ? false : true,
                              RolID = last.RoleID == null ? default(string) : last.RoleID,
                              ModuloID = last.ModuloID.Equals(null) ? default(int) : last.ModuloID,
                          }).ToList();

                listPermisosByRol.AddRange(varPermisosByRol);
            }


            var jsonData = new
            {
                data = (from p in db.Permisos
                        join pU in db.PermisosPorUsuarios on p.PermisoID equals pU.PermisoID
                        into temp
                        from last in temp.DefaultIfEmpty()
                        select new
                        {
                            PermisoID = p.PermisoID,
                            Descripcion = p.Descripcion,
                            UsuarioID = last.UsuarioID == null ? false : true,
                            ModuloID = last.ModuloID.Equals(null) ? false : true
                        }).ToList()
            };//int i = age.HasValue ? age.Value : 0;
            //String.IsNullOrEmpty(ID.ToString());

            return Json(jsonData, JsonRequestBehavior.AllowGet);
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
