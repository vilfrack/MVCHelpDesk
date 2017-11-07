using MVCHelpDesk.Models;
using MVCHelpDesk.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCHelpDesk.Controllers
{
    public class KanbanController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            return View(db.Tasks.ToList());
        }
        public ActionResult AddTask()
        {
            return View();
        }
        [HttpPost]
        public ActionResult editStatus(string id, string status)
        {
            int cod = Convert.ToInt32(id);
            Tasks task = db.Tasks.SingleOrDefault(c => c.TaskID == cod);
            if (task != null)
            {
                var sta = db.Status.ToList();
                foreach (var item in sta)
                {
                    if (item.nombre == status)
                    {
                        task.StatusID = item.StatusID;
                    }
                }
                db.SaveChanges();
            }
            return Json(new { success = true });
        }
        [HttpGet]
        public ActionResult detail(string sid)
        {
            int id = Convert.ToInt32(sid);
            ViewTaskFile TaskFiles = new ViewTaskFile();
            var vtask = db.Tasks.ToList();
            var vFile = db.Files.ToList();
            var vStatus = db.Status.ToList();

            // var subquery = db.Tasks.Where(sq => sq.TaskID == id).SingleOrDefault();
            var subquery = (from tas in vtask
                            join sta in vStatus on tas.StatusID equals sta.StatusID
                            where tas.TaskID == id
                            select new
                            {
                                Titulo = tas.Titulo,
                                FechaCreacion = tas.FechaCreacion,
                                Status = sta.nombre,
                                Descripcion = tas.Descripcion,
                                TaskID = tas.TaskID
                            }).SingleOrDefault();

            var subqueryFile = db.Files.Where(qf => qf.IDFiles == id).ToList();
            TaskFiles.TaskID = id;
            TaskFiles.Titulo = subquery.Titulo;
            TaskFiles.Descripcion = subquery.Descripcion;
            TaskFiles.ruta_virtual = new List<string>();
            TaskFiles.IDFiles = new List<int>();
            TaskFiles.status = subquery.Status;
            foreach (var item in subqueryFile)
            {
                TaskFiles.ruta_virtual.Add(item.ruta_virtual);
                TaskFiles.IDFiles.Add(item.IDFiles);
            }
            return PartialView(TaskFiles);
        }
        //[HttpPost]
        //public ActionResult AddTask()
        //{
        //    return View();
        //}
    }
}