using MVCHelpDesk.Models;
using MVCHelpDesk.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCHelpDesk.Helper;
using MVCHelpDesk.Attribute;
using System.Data.Entity.Validation;

namespace MVCHelpDesk.Controllers
{
    public class KanbanController : Controller
    {
        #region INSTANCIAS
        private Helper.UserIdentity usuario = new UserIdentity();
        private ApplicationDbContext db = new ApplicationDbContext();
        private ModuloAttribute moduloAttribute = new ModuloAttribute();
        private GetErrors getError = new GetErrors();
        private Helpers help = new Helpers();
        #endregion

        public ActionResult Index()
        {
            string UsuarioID = usuario.GetIdUser();
            bool permisoAsignar = false;
            if (!moduloAttribute.PermisoByRol(Permisos.AllModulos.Requerimiento, Permisos.AllPermisos.Asignar) && !moduloAttribute.PermisoByUser(Permisos.AllModulos.Requerimiento, Permisos.AllPermisos.Asignar))
            {
                permisoAsignar = true;
            }
            ViewBag.permisoAsignar = permisoAsignar;
            return View(db.Tasks.Where(w => w.AsignadoID == UsuarioID).ToList());
        }
        public ActionResult AddTask()
        {
            return View();
        }
        [HttpPost]
        public ActionResult editStatus(string id, string status)
        {
            try
            {
                int cod = Convert.ToInt32(id);
                Tasks task = db.Tasks.Find(cod);
                Comentarios coment = new Comentarios();
                string UserID = usuario.GetIdUser();
                var perfilUsuario = db.Perfiles.Where(w => w.UsuarioID == UserID)
                                                .Select(s => new { Nombre = s.Nombre, Apellido = s.Apellido })
                                                .SingleOrDefault();

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

                    coment.Comentario = perfilUsuario.Apellido + " " + perfilUsuario.Nombre + " Ha modificado el status del requerimiento a " + status;
                    coment.TaskID = cod;
                    coment.UsuarioID = usuario.GetIdUser();
                    coment.Fecha = DateTime.Now;
                    db.Comentarios.Add(coment);
                    db.SaveChanges();


                }
                return Json(new { success = true });
            }
            catch (DbEntityValidationException e)
            {
                return Json(new { success = false });
            }
            catch (Exception ex)
            {
                return Json(new { success = false });
            }

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
                                TaskID = tas.TaskID,
                                FechaFinalizacion = tas.FechaFinalizacion,
                                //
                                UsuarioID = tas.UsuarioID,
                                Asignado = tas.AsignadoID
                            }).SingleOrDefault();
            //se obtiene los datos del usuario asignado
            var usuarioAsignado = db.Perfiles.Where(w => w.UsuarioID == subquery.Asignado).Select(s => new { Nombre = s.Nombre, Apellido = s.Apellido }).SingleOrDefault();
            //se obtiene los datos del usuario solicitante
            var usuarioSolicitante = db.Perfiles.Where(w => w.UsuarioID == subquery.UsuarioID).Select(s => new { Nombre = s.Nombre, Apellido = s.Apellido }).SingleOrDefault();
            //se obtiene los datos de los archivos
            var subqueryFile = db.Files.Where(w => w.TasksID == id).ToList();
            //SE CREA UNA RUTA PARA LA IMAGEN PREDETERMINADA
            string FotoPredefinida = "~/Images/empleados/perfil.jpg";
            //FOTO DEL SOLICITANTE
            string FotoPerfil = db.Perfiles.Where(w => w.UsuarioID == subquery.UsuarioID).Select(s => s.rutaImg).SingleOrDefault() == null ? FotoPredefinida : db.Perfiles.Where(w => w.UsuarioID == subquery.UsuarioID).Select(s => s.rutaImg).SingleOrDefault();
            //FOTO DEL ASIGNADO
            string FotoAsignado = db.Perfiles.Where(w => w.UsuarioID == subquery.Asignado).Select(s => s.rutaImg).SingleOrDefault() == null ? FotoPredefinida : db.Perfiles.Where(w => w.UsuarioID == subquery.Asignado).Select(s => s.rutaImg).SingleOrDefault();
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
            TaskFiles.status = subquery.Status;
            TaskFiles.UsuarioID = subquery.UsuarioID;
            TaskFiles.FechaFinalizacion = Convert.ToString(subquery.FechaFinalizacion);
            TaskFiles.nombre = usuarioSolicitante.Apellido + " " + usuarioSolicitante.Nombre;
            TaskFiles.Foto = FotoPerfil;
            TaskFiles.FotoAsignado = FotoAsignado;
            TaskFiles.NombreCompletoAsignado = usuarioAsignado.Apellido + " " + usuarioAsignado.Nombre;

            TaskFiles.ComentarioPerfiles = new List<ViewComentarioPerfiles>();
            foreach (var item in db.Comentarios.Where(w => w.TaskID == TaskFiles.TaskID).ToList())
            {
                TaskFiles.ComentarioPerfiles.Add(new ViewComentarioPerfiles
                {
                    Nombre = db.Perfiles.Where(w => w.UsuarioID == item.UsuarioID).Select(s => s.Nombre).SingleOrDefault(),
                    Apellido = db.Perfiles.Where(w => w.UsuarioID == item.UsuarioID).Select(s => s.Apellido).SingleOrDefault(),
                    rutaImg = db.Perfiles.Where(w => w.UsuarioID == item.UsuarioID).Select(s => s.rutaImg).SingleOrDefault(),
                    Comentario = item.Comentario,
                    Fecha = item.Fecha
                });
            }



            return PartialView(TaskFiles);
        }
        [HttpPost]
        public JsonResult Save(ViewComentario viewComentario, HttpPostedFileBase[] File)
        {
            bool bsuccess = false;
            if (ModelState.IsValid)
            {
                Comentarios coment = new Comentarios();
                coment.Comentario = viewComentario.Comentario;
                coment.TaskID = Convert.ToInt32(viewComentario.TaskID);
                coment.UsuarioID = usuario.GetIdUser();
                coment.Fecha = DateTime.Now;
                db.Comentarios.Add(coment);
                db.SaveChanges();

                var req = db.Tasks.Find(Convert.ToInt32(viewComentario.TaskID));
                req.FechaFinalizacion = Convert.ToDateTime(viewComentario.FechaEntrega).Date;
                db.SaveChanges();

                bsuccess = true;
            }

            if (File != null)
            {
                int TaskID = Convert.ToInt32(viewComentario.TaskID);
                bool boolArchivos = help.CantidadArchivos(TaskID);
                if (boolArchivos == true)
                {
                    return Json(new { success = false,cantidad = boolArchivos, Errors = getError.GetErrorsFromModelState(ModelState), JsonRequestBehavior.AllowGet });
                }
                SaveUploadedFile(File, Convert.ToInt32(viewComentario.TaskID));
                bsuccess = true;
            }
            return Json(new { success = bsuccess, Errors = getError.GetErrorsFromModelState(ModelState), JsonRequestBehavior.AllowGet });
        }
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
                            TasksID = id,
                            ruta_virtual = ruta_virtual + "/" + Archivo.FileName
                        };
                        db.Files.Add(modelFiles);
                        db.SaveChanges();
                        Archivo.SaveAs(path);
                    }
                }
            }
        }
        [HttpPost]
        public JsonResult getComentario(int id)
        {
            char[] MyChar = { '~' };
            var comentarioPerfil = (from perfil in db.Perfiles
                                    join coment in db.Comentarios on perfil.UsuarioID equals coment.UsuarioID
                                    where coment.TaskID == id
                                    select new
                                    {
                                        IDComentario= coment.IDComentario,
                                        Nombre = perfil.Nombre == null ? "" : perfil.Nombre,
                                        Apellido = perfil.Apellido == null ? "" : perfil.Apellido,
                                        rutaImg = perfil.rutaImg.Remove(0, 1),
                                        Comentario = coment.Comentario,
                                        Fecha = coment.Fecha
                                    }).ToList();

            return Json(comentarioPerfil.OrderByDescending(o=>o.IDComentario), JsonRequestBehavior.AllowGet);
        }
    }
}