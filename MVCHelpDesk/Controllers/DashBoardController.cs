using MVCHelpDesk.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCHelpDesk.Controllers
{
    [Authorize]
    public class DashBoardController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: DashBoard
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CantidadTask() {


            //return Json(new { success = true, Errors = getError.GetErrorsFromModelState(ModelState), JsonRequestBehavior.AllowGet });
            return PartialView();
        }
        public ActionResult TaskMensuales()
        {
            return PartialView();
        }
        public ActionResult DesarrolladorTask()
        {
            return PartialView();
        }
        public ActionResult DesarrolladorTiempo()
        {
            return PartialView();
        }
        public ActionResult TaskTiempo()
        {
            return PartialView();
        }
        /*PRUEBA*/
        public JsonResult getCantidadTask()
        {
            var jsonData = new
            {
                data = db.MaestroTaskStatus.ToList()
            };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
    }
}