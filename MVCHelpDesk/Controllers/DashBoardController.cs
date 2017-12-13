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
        // GET: DashBoard
        public ActionResult Index()
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
    }
}