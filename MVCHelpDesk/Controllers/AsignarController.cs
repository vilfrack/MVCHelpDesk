using MVCHelpDesk.Models;
using MVCHelpDesk.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCHelpDesk.Helper;
namespace MVCHelpDesk.Controllers
{
    public class AsignarController : Controller
    {
        public UserIdentity userIdentity = new UserIdentity();
        public Helpers help = new Helpers();
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            return View();
        }
        /*
              <th>Titulo</th>
            <th>Descripcion</th>
            <th>Solicitante</th>
            <th>Fecha de solicitud</th>
             */
        public JsonResult get()
        {
            var requerimientos = (from t in db.Tasks
                                  join p in db.Perfiles on t.UsuarioID equals p.UsuarioID
                                  select new {
                                      TaskID = t.TaskID,
                                      Descripcion = t.Descripcion,
                                      Solicitante = p.Apellido +" "+p.Nombre,
                                      Fecha = t.FechaCreacion
                                  }).ToList();
            var jsonData = new
            {
                data = (from t in db.Tasks
                        join p in db.Perfiles on t.UsuarioID equals p.UsuarioID
                        select new
                        {
                            TaskID = t.TaskID,
                            Descripcion = t.Descripcion,
                            Titulo = t.Titulo,
                            Solicitante = p.Apellido + " " + p.Nombre,
                            Fecha = t.FechaCreacion
                        }).ToList()
            };
          return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Details()
        {
            return PartialView();
        }

        // GET: Asignar/Create
        public ActionResult asignar(int id)
        {
            var a = (from t in db.Tasks
                     join p in db.Perfiles on t.UsuarioID equals p.UsuarioID
                     where t.TaskID == id
                     select new
                     {
                         TaskID = t.TaskID,
                         Descripcion = t.Descripcion,
                         Solicitante = p.Apellido + " " + p.Nombre,
                         Fecha = t.FechaCreacion,
                         Asignado = t.AsignadoID
                     }).SingleOrDefault();

            string UserID = userIdentity.GetIdUser();

            var IDDepartamento = help.GetDepartByIDUser(UserID);

            var perfiles = (from perfil in db.Perfiles
                            join dep in db.Departamento on perfil.IDDepartamento equals dep.IDDepartamento
                            where perfil.IDDepartamento == IDDepartamento
                            select new
                            {
                                Nombre = perfil.Nombre,
                                Apellido = perfil.Apellido,
                            }).ToList();

            if (a.Asignado == null)
            {
                return PartialView();
            }



            var query = db.Roles.Find(id);
            var viewRol = new ViewRol
            {
                Name = query.Name,
                Id = query.Id
            };
            return PartialView(viewRol);
        }

        // POST: Asignar/Create
        [HttpPost]
        public ActionResult asignar(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Asignar/Edit/5
        public ActionResult Edit(int id)
        {

            var a = (from t in db.Tasks
                                  join p in db.Perfiles on t.UsuarioID equals p.UsuarioID
                                  where t.TaskID == id
                                  select new
                                  {
                                      TaskID = t.TaskID,
                                      Descripcion = t.Descripcion,
                                      Solicitante = p.Apellido + " " + p.Nombre,
                                      Fecha = t.FechaCreacion,
                                      Asignado = t.AsignadoID
                                  }).SingleOrDefault();
            if (a.Asignado==null)
            {
                return PartialView();
            }



            var query = db.Roles.Find(id);
            var viewRol = new ViewRol
            {
                Name = query.Name,
                Id = query.Id
            };
            return PartialView(viewRol);
        }

        // POST: Asignar/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Asignar/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Asignar/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
