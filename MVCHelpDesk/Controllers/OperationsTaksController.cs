using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCHelpDesk.Models;
using System.IO;
using MVCHelpDesk.ViewModel;

namespace MVCHelpDesk.Controllers
{
    [Authorize]
    public class OperationsTaksController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: OperationsTaks
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Details()
        {
            return PartialView();
        }
        public JsonResult get()
        {

            // ES NECESARIO PONER EXACTAMENTE LOS CAMPOS A EXTRAER PORQUE SI NO DA ERROR
            var tabla = db.Tasks.ToList();
            var status = db.Status.ToList();
            var jsonData = new
            {
                data = (from query in tabla
                        join sta in status on query.StatusID equals sta.StatusID
                        select new
                        {
                            Titulo = query.Titulo,
                            FechaCreacion = query.FechaCreacion,
                            Status = sta.nombre,
                            TaskID = query.TaskID
                        }).ToList()
            };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Create()
        {
            return PartialView();
        }
        [HttpPost]
        public JsonResult Create(Tasks tasks, HttpPostedFileBase[] file)
        {

            try
            {
                bool bsuccess = false;
                int id = 0;
                if (ModelState.IsValid)
                {

                    //  tasks.ruta_foto = CrearDirectorio(IDRuta);
                    tasks.FechaCreacion = DateTime.Now.Date;
                    tasks.FechaFinalizacion = DateTime.Now.Date;
                    tasks.StatusID = 1;
                    db.Tasks.Add(tasks);
                    db.SaveChanges();
                    var getLast = db.Tasks.OrderByDescending(u => u.TaskID).FirstOrDefault();
                    id = getLast.TaskID;
                    bsuccess = true;
                    SaveUploadedFile(file, id);


                }
                return Json(new { success = bsuccess, Errors = GetErrorsFromModelState(), JsonRequestBehavior.AllowGet });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, Errors = GetErrorsFromModelState(), JsonRequestBehavior.AllowGet });
            }
        }
        //SaveUploadedFile DEBE ESTAR EN LA CLASE HELPER
        private void SaveUploadedFile(HttpPostedFileBase[] file, int id)
        {
            foreach (HttpPostedFileBase Archivo in file)
            {
                if (Archivo != null)
                {
                    if (Archivo != null && Archivo.ContentLength > 0)
                    {
                        var originalDirectory = new DirectoryInfo(string.Format("~/Images/" + id));
                        string ruta = Path.Combine(Server.MapPath("~/Images/" + id));
                        string ruta_virtual = "~/Images/" + id;
                        string pathString = ruta;
                        var fileName1 = Path.GetFileName(Archivo.FileName);
                        bool isExists = System.IO.Directory.Exists(pathString);

                        if (!isExists)
                            System.IO.Directory.CreateDirectory(pathString);

                        var path = string.Format("{0}\\{1}", pathString, Archivo.FileName);


                        Files modelFiles = new Files
                        {
                            nombre = Archivo.FileName,
                            ruta = pathString + "/" + Archivo.FileName,
                            IDFiles = id,
                            ruta_virtual = ruta_virtual + "/" + Archivo.FileName
                        };
                        // Archivo.SaveAs(path);
                        db.Files.Add(modelFiles);
                        db.SaveChanges();
                    }
                }
            }
        }
        //GetErrorsFromModelState DEBE ESTAR EN LA CLASE HELPER
        public Dictionary<string, object> GetErrorsFromModelState()
        {

            var errors = new Dictionary<string, object>();
            foreach (var key in ModelState.Keys)
            {
                // Only send the errors to the client.
                if (ModelState[key].Errors.Count > 0)
                {
                    errors[key] = ModelState[key].Errors;
                }
                else
                {
                    errors[key] = "true";
                }
            }

            return errors;
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            ViewTaskFile TaskFiles = new ViewTaskFile();
            var vtask = db.Tasks.ToList();
            var vFile = db.Files.ToList();

            var subquery = db.Tasks.Where(sq => sq.TaskID == id).SingleOrDefault();
            var subqueryFile = db.Files.Where(qf => qf.IDFiles == id).ToList();
            TaskFiles.TaskID = id;
            TaskFiles.Titulo = subquery.Titulo;
            TaskFiles.Descripcion = subquery.Descripcion;
            TaskFiles.ruta_virtual = new List<string>();
            TaskFiles.IDFiles = new List<int>();
            foreach (var item in subqueryFile)
            {
                TaskFiles.ruta_virtual.Add(item.ruta_virtual);
                TaskFiles.IDFiles.Add(item.IDFiles);
            }
            return PartialView(TaskFiles);
        }

        [HttpPost]
        public JsonResult Edit(Tasks tasks, HttpPostedFileBase[] FileEdit)
        {
            try
            {
                bool bsuccess = false;
                if (ModelState.IsValid)
                {
                    var result = db.Tasks.Find(tasks.TaskID);
                    result.Titulo = tasks.Titulo;
                    result.Descripcion = tasks.Descripcion;
                    db.SaveChanges();
                    bsuccess = true;
                    if (FileEdit.Count() > 0)
                    {
                        SaveUploadedFile(FileEdit, tasks.TaskID);
                    }
                }
                return Json(new { success = bsuccess, Errors = GetErrorsFromModelState(), JsonRequestBehavior.AllowGet });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, Errors = GetErrorsFromModelState(), JsonRequestBehavior.AllowGet });
            }
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                bool bsuccess = true;
                Files deleteFiles = db.Files.Where(d => d.IDFiles == id).SingleOrDefault();
                if (deleteFiles != null)
                {
                    Directory.Delete(deleteFiles.ruta, true);
                    db.Files.Remove(deleteFiles);
                }
                Tasks registro = db.Tasks.Where(r => r.TaskID == id).Single();

                db.Tasks.Remove(registro);
                db.SaveChanges();
                return Json(new { success = bsuccess });
            }
            catch (Exception ex)
            {
                return Json(new { success = false });
            }
        }
        [HttpPost]
        public ActionResult DeleteImage(int id)
        {
            try
            {
                bool bsuccess = false;
                Files deleteFiles = db.Files.Where(d => d.IDFiles == id).SingleOrDefault();
                if (deleteFiles != null)
                {
                    if (System.IO.File.Exists(Server.MapPath(deleteFiles.ruta_virtual)))
                    {
                        System.IO.File.Delete(Server.MapPath(deleteFiles.ruta_virtual));//investigar  Server.MapPath
                        Files registro = db.Files.Where(r => r.IDFiles == id).Single();

                        db.Files.Remove(registro);
                        db.SaveChanges();
                        bsuccess = true;
                    }
                }

                return Json(new { success = bsuccess });
            }
            catch (Exception ex)
            {
                return Json(new { success = false });
            }
        }
    }
}
