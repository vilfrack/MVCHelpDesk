using MVCHelpDesk.Helper;
using MVCHelpDesk.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace MVCHelpDesk.Controllers
{
    public class ModulosController : Controller
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
                data = db.Modulos.Select(s=> new { ModuloID = s.ModuloID, Descripcion =s.Descripcion}).ToList()
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
        public JsonResult Create(Modulos modulos)
        {
            try
            {
                bool bsuccess = false;
                if (ModelState.IsValid)
                {
                    bsuccess = true;
                    db.Modulos.Add(modulos);
                    db.SaveChanges();
                }
                return Json(new { success = bsuccess, Errors = getError.GetErrorsFromModelState(ModelState), JsonRequestBehavior.AllowGet });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, Errors = getError.GetErrorsFromModelState(ModelState), JsonRequestBehavior.AllowGet });
            }
        }
        // GET: Users/Edit/5
        public ActionResult Edit(string id)
        {
            int moduloID = Convert.ToInt32(id);
            Modulos modulos = db.Modulos.Where(sq => sq.ModuloID == moduloID).SingleOrDefault();
            return PartialView(modulos);
        }

        // POST: Users/Edit/5
        [HttpPost]
        public JsonResult Edit(Modulos modulos)
        {
            try
            {
                bool bsuccess = false;
                if (ModelState.IsValid)
                {
                    bsuccess = true;
                    var varModulos = db.Modulos.Find(modulos.ModuloID);
                    varModulos.Descripcion = modulos.Descripcion;
                    db.SaveChanges();
                }
                return Json(new { success = bsuccess, Errors = getError.GetErrorsFromModelState(ModelState), JsonRequestBehavior.AllowGet });
            }
            catch
            {
                return Json(new { success = false, Errors = getError.GetErrorsFromModelState(ModelState), JsonRequestBehavior.AllowGet });
            }
        }
        // POST: Users/Delete/5
        [HttpPost]
        public ActionResult Delete(string id)
        {
            try
            {
                bool bsuccess = false;
                string errorMensaje = "";
                if (id != "1")
                {
                    int moduloID = Convert.ToInt32(id);
                    bsuccess = true;
                    Modulos modulos = db.Modulos.Single(d => d.ModuloID == moduloID);
                    db.Modulos.Remove(modulos);
                    db.SaveChanges();
                }
                else
                {
                    bsuccess = false;
                    errorMensaje = "El departamento de sistema no puede eliminarse";
                }
                return Json(new { success = bsuccess, mensaje = errorMensaje });
            }
            catch (Exception ex)
            {
                return Json(new { success = false });
            }
        }

    }
}
