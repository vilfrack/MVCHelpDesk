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
namespace MVCHelpDesk.Controllers
{
    public class KanbanController : Controller
    {
        private Helper.UserIdentity usuario = new UserIdentity();
        private ApplicationDbContext db = new ApplicationDbContext();
        private ModuloAttribute moduloAttribute = new ModuloAttribute();
        private GetErrors getError = new GetErrors();
        public ActionResult Index()
        {
            string UsuarioID = usuario.GetIdUser();
            bool permisoAsignar = false;
            if (!moduloAttribute.PermisoByRol(Permisos.AllModulos.Requerimiento, Permisos.AllPermisos.Asignar) && !moduloAttribute.PermisoByUser(Permisos.AllModulos.Requerimiento, Permisos.AllPermisos.Asignar))
            {
                permisoAsignar = true;
            }
            ViewBag.permisoAsignar = permisoAsignar;
            return View(db.Tasks.Where(w=>w.AsignadoID== UsuarioID).ToList());
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
                                TaskID = tas.TaskID,
                                FechaFinalizacion = tas.FechaFinalizacion.ToShortDateString(),
                                //
                                UsuarioID = tas.UsuarioID,
                                Asignado = tas.AsignadoID
                            }).SingleOrDefault();
            var usuarioAsignado = db.Perfiles.Where(w => w.UsuarioID == subquery.Asignado).Select(s => new { Nombre = s.Nombre, Apellido = s.Apellido }).SingleOrDefault();
            var usuarioSolicitante = db.Perfiles.Where(w => w.UsuarioID == subquery.UsuarioID).Select(s => new { Nombre = s.Nombre, Apellido = s.Apellido }).SingleOrDefault();
            var subqueryFile = db.Files.Where(qf => qf.IDFiles == id).ToList();
            string FotoPredefinida = "~/Images/empleados/perfil.jpg";
            string FotoPerfil = db.Perfiles.Where(w => w.UsuarioID == subquery.UsuarioID).Select(s => s.rutaImg).SingleOrDefault() == null ? FotoPredefinida : db.Perfiles.Where(w => w.UsuarioID == subquery.UsuarioID).Select(s => s.rutaImg).SingleOrDefault();
            string FotoAsignado = db.Perfiles.Where(w => w.UsuarioID == subquery.Asignado).Select(s => s.rutaImg).SingleOrDefault() == null ? FotoPredefinida : db.Perfiles.Where(w => w.UsuarioID == subquery.Asignado).Select(s => s.rutaImg).SingleOrDefault();
            TaskFiles.TaskID = id;
            TaskFiles.Titulo = subquery.Titulo;
            TaskFiles.Descripcion = subquery.Descripcion;
            TaskFiles.ruta_virtual = new List<string>();
            TaskFiles.IDFiles = new List<int>();
            TaskFiles.status = subquery.Status;
            TaskFiles.UsuarioID = subquery.UsuarioID;
            TaskFiles.FechaFinalizacion = subquery.FechaFinalizacion;
            TaskFiles.nombre = usuarioSolicitante.Apellido + " " + usuarioSolicitante.Nombre;
            TaskFiles.Foto = FotoPerfil;
            TaskFiles.FotoAsignado = FotoAsignado;
            TaskFiles.NombreCompletoAsignado = usuarioAsignado.Apellido + " " + usuarioAsignado.Nombre;
            foreach (var item in subqueryFile)
            {
                TaskFiles.ruta_virtual.Add(item.ruta_virtual);
                TaskFiles.IDFiles.Add(item.IDFiles);
            }
            TaskFiles.ComentarioPerfiles = new List<ViewComentarioPerfiles>();
            foreach (var item in db.Comentarios.Where(w=>w.TaskID==TaskFiles.TaskID).ToList())
            {
                TaskFiles.ComentarioPerfiles.Add(new ViewComentarioPerfiles
                {
                    Nombre = db.Perfiles.Where(w=>w.UsuarioID==item.UsuarioID).Select(s=>s.Nombre).SingleOrDefault(),
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
            //if (File !=null)
            //{

            //    bsuccess = true;
            //}
            return Json(new { success = bsuccess, Errors = getError.GetErrorsFromModelState(ModelState), JsonRequestBehavior.AllowGet });
        }
        [HttpPost]
        public JsonResult getComentario(int id) {
            char[] MyChar = { '~'};
            var comentarioPerfil = (from perfil in db.Perfiles
                                    join coment in db.Comentarios on perfil.UsuarioID equals coment.UsuarioID
                                    where coment.TaskID == id
                                    select new
                                    {
                                        Nombre = perfil.Nombre ==null ? "": perfil.Nombre,
                                        Apellido = perfil.Apellido==null?"": perfil.Apellido,
                                        rutaImg = perfil.rutaImg.Remove(0, 1),
                                        Comentario = coment.Comentario,
                                        Fecha = coment.Fecha
                                    }).ToList();

            return Json(comentarioPerfil, JsonRequestBehavior.AllowGet);
        }
    }
}