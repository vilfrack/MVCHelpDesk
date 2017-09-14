using MVCHelpDesk.Helper;
using MVCHelpDesk.Models;
using MVCHelpDesk.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCHelpDesk.Controllers
{
    public class RolesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private GetErrors getError = new GetErrors();
        private string rutaImagen = "";
        // GET: Users
        public ActionResult Index()
        {
            return View();
        }

        // GET: Users/Details/5
        public ActionResult Details()
        {
            return PartialView();
        }
        public JsonResult get()
        {
            // ES NECESARIO PONER EXACTAMENTE LOS CAMPOS A EXTRAER PORQUE SI NO DA ERROR
            var jsonData = new
            {
                data = db.Roles.ToList()
            };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        // GET: Users/Create
        public ActionResult Create()
        {
            return PartialView();
        }

        // POST: Users/Create
        [HttpPost]
        public JsonResult Create(ViewRol roles)
        {
            try
            {
                // m.IDTema = m.IDTema == default(Guid) ? Guid.NewGuid() : m.IDTema;
                bool bsuccess = false;
                if (ModelState.IsValid)
                {
                    //db.Roles.Add(roles);
                    //db.SaveChanges();
                    //bsuccess = true;
                }
                return Json(new { success = bsuccess, Errors = getError.GetErrorsFromModelState(ModelState), JsonRequestBehavior.AllowGet });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, Errors = getError.GetErrorsFromModelState(ModelState), JsonRequestBehavior.AllowGet });
            }
        }
        // GET: Users/Edit/5
        public ActionResult Edit(int id)
        {

            ViewRol rol = new ViewRol();
            //var query = db.Roles.Where(sq => sq.IDRol == id).SingleOrDefault();
            //var subqueryPerfiles = db.Perfiles.Where(qf => qf.IDUsers == id).SingleOrDefault();
            return PartialView();
        }

        // POST: Users/Edit/5
        [HttpPost]
        public JsonResult Edit(ViewRol roles)
        {
            try
            {
                bool bsuccess = false;
                //var getRuta = db.Roles.Where(c => c.IDRol == roles.IDRol);
                //if (ModelState.IsValid)
                //{
                //    var rol = db.Roles.Find(roles.IDRol);
                //    rol.rol = roles.rol;
                //    db.SaveChanges();
                //    bsuccess = true;

                //}
                return Json(new { success = bsuccess, Errors = getError.GetErrorsFromModelState(ModelState), JsonRequestBehavior.AllowGet });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, Errors = getError.GetErrorsFromModelState(ModelState), JsonRequestBehavior.AllowGet });
            }
        }
        // POST: Users/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                bool bsuccess = false;
                string errorMensaje = "";
                //if (id != 1)
                //{
                //    Roles rol = db.Roles.Where(r => r.IDRol == id).Single();
                //    db.Roles.Remove(rol);
                //    db.SaveChanges();
                //}
                //else
                //{
                //    bsuccess = false;
                //    errorMensaje = "El rol de Administrador no puede borrarse";
                //}
                return Json(new { success = bsuccess, mensaje = errorMensaje });
            }
            catch (Exception ex)
            {
                return Json(new { success = false });
            }
        }

    }
}
