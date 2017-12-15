using MVCHelpDesk.Models;
using MVCHelpDesk.ViewModel;
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
        public List<MaestroTaskStatus> ListCantidadTask { get; set; }

        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: DashBoard
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(string FechaInicio, string FechaFin)
        {
            return View();
        }

        public ActionResult CantidadTask() {
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
        public JsonResult getCantidadTask(string fechaInicio, string fechaFinal)
        {
            DateTime fInicio = Convert.ToDateTime(fechaInicio).Date;
            DateTime fFinal = Convert.ToDateTime(fechaFinal).Date;
            var jsonData = new
            {
                data = db.MaestroTaskStatus.Where(w=> w.Fecha >= fInicio && w.Fecha <= fFinal).ToList()
            };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
    }
}