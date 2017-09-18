using MVCHelpDesk.Models;
using MVCHelpDesk.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCHelpDesk.Helper;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using System.IO;

namespace MVCHelpDesk.Controllers
{
    public class UsuarioController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private GetErrors errors = new GetErrors();
        // GET: Usuario
        public ActionResult Index()
        {
            ApplicationDbContext sd = new ApplicationDbContext();
            return View();
        }

        // GET: Usuario/Details/5
        public ActionResult Details()
        {
            return PartialView();
        }
        public JsonResult get()
        {

                // ES NECESARIO PONER EXACTAMENTE LOS CAMPOS A EXTRAER PORQUE SI NO DA ERROR
                var jsonData = new
            {
                data = (from u in db.Users
                        join p in db.Perfiles on u.Id equals p.UsuarioID
                        select new
                        {
                            IDUser = u.Id,
                            login = u.UserName,
                            nombre = p.Nombre,
                            apellido = p.Apellido
                        }).ToList()
        };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        // GET: Usuario/Create
        public ActionResult Create()
        {
            return PartialView();
        }

        // POST: Usuario/Create
        [HttpPost]
        public JsonResult Create(ViewUserPerfil UserPerfil, HttpPostedFileBase file)
        {
            try
            {
                bool bsuccess = false;
                if (ModelState.IsValid)
                {
                    if (!(db.Users.Any(u => u.UserName == UserPerfil.Email))) {

                        var userStore = new UserStore<ApplicationUser>(db);
                        var userManager = new UserManager<ApplicationUser>(userStore);
                        var userToInsert = new ApplicationUser
                        {
                            Email = UserPerfil.Email,
                            PasswordHash = UserPerfil.Password,
                            UserName = UserPerfil.Email
                        };
                        userManager.Create(userToInsert, UserPerfil.Password);
                        string ruta = string.Empty;
                        if (file != null)
                        {
                           ruta= SaveUploadedFile(file, userToInsert.Id);
                        }
                        var perfiles = new Perfiles
                        {
                            Nombre = UserPerfil.Nombre,
                            Apellido = UserPerfil.Apellido,
                            UsuarioID = userToInsert.Id,
                            rutaImg = ruta
                        };
                        db.Perfiles.Add(perfiles);
                        db.SaveChanges();
                        bsuccess = true;
                    }
                }

                return Json(new { success = bsuccess, Errors = errors.GetErrorsFromModelState(ModelState), JsonRequestBehavior.AllowGet });
            }
            catch
            {
                return Json(new { success = false, Errors = errors.GetErrorsFromModelState(ModelState), JsonRequestBehavior.AllowGet });
            }
        }
        private string SaveUploadedFile(HttpPostedFileBase file, string id)
        {
            var originalDirectory = new DirectoryInfo(string.Format("~/Images/empleados/" + id));
            string ruta = Path.Combine(Server.MapPath("~/Images/empleados/" + id));
            string ruta_virtual = "~/Images/empleados/" + id;
            string pathString = ruta;
            bool isExists = System.IO.Directory.Exists(pathString);

            if (!isExists)
                System.IO.Directory.CreateDirectory(pathString);

            var path = string.Format("{0}\\{1}", pathString, file.FileName);

            file.SaveAs(path);
            return ruta_virtual+"/"+ file.FileName;
        }
        // GET: Usuario/Edit/5
        public ActionResult Edit(string id)
        {
            var query = (from u in db.Users
                        join p in db.Perfiles on u.Id equals p.UsuarioID
                        where u.Id == id
                        select new {
                            Id = u.Id,
                            Password = u.PasswordHash,
                            Email = u.Email,
                            Nombre = p.Nombre,
                            Apellido = p.Apellido,
                            IDPerfil = p.IDPerfil,
                            Ruta = p.rutaImg
                        }).SingleOrDefault();

            var userPerfil = new ViewUserPerfil
            {
                IDPerfil = query.IDPerfil,
                IDUser = query.Id,
                Email = query.Email,
                Nombre = query.Nombre,
                Apellido = query.Apellido,
                Password = query.Password,
                rutaImg = query.Ruta
            };

            return PartialView(userPerfil);
        }

        // POST: Usuario/Edit/5
        [HttpPost]
        public ActionResult Edit(ViewUserPerfil userPerfil, HttpPostedFileBase FileEdit)
        {
            try
            {
                bool bsuccess = false;
                if (ModelState.IsValid)
                {
                    var result = (from u in db.Users
                                 join p in db.Perfiles on u.Id equals p.UsuarioID
                                 where u.Id == userPerfil.IDUser
                                 select new
                                 {
                                     Id = u.Id,
                                     Password = u.PasswordHash,
                                     Email = u.Email,
                                     Nombre = p.Nombre,
                                     Apellido = p.Apellido,
                                     IDPerfil = p.IDPerfil,
                                     Ruta = p.rutaImg
                                 }).SingleOrDefault();
                    string ruta = string.Empty;
                    if (FileEdit != null)
                    {
                        ruta = SaveUploadedFile(FileEdit, userPerfil.IDUser);
                    }
                    else
                    {
                        ruta = result.Ruta;
                    }
                    var userStore = new UserStore<ApplicationUser>(db);
                    var userManager = new UserManager<ApplicationUser>(userStore);
                    //SE BUSCA AL USUARIO
                    var usu = userManager.FindById(userPerfil.IDUser);
                    usu.Email = userPerfil.Email;
                    usu.Id = userPerfil.IDUser;
                    usu.UserName = userPerfil.Email;
                    //se actualiza los resultados
                    userManager.Update(usu);
                    //cambiamos el password
                    userManager.ChangePassword(userPerfil.IDUser,result.Password, userPerfil.Password);
                    //actualizamos los datos de perfiles
                    var perfiles = db.Perfiles.Find(userPerfil.IDPerfil);
                    perfiles.Nombre = userPerfil.Nombre;
                    perfiles.Apellido = userPerfil.Apellido;
                    perfiles.rutaImg = ruta;

                    db.SaveChanges();
                    bsuccess = true;

                }
                return Json(new { success = bsuccess, Errors = errors.GetErrorsFromModelState(ModelState), JsonRequestBehavior.AllowGet });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, Errors = errors.GetErrorsFromModelState(ModelState), JsonRequestBehavior.AllowGet });
            }
        }
        // POST: Usuario/Delete/5
        [HttpPost]
        public ActionResult Delete(string id)
        {
            try
            {
                bool bsuccess = true;
                Perfiles perfil = db.Perfiles.Where(d => d.UsuarioID == id).SingleOrDefault();
                if (perfil != null)
                {

                    string ruta = Path.Combine(Server.MapPath("~/Images/empleados/" + id));
                    bool isExists = System.IO.Directory.Exists(ruta);

                    if (isExists)
                        Directory.Delete(ruta, true);

                    db.Perfiles.Remove(perfil);
                }
                var userStore = new UserStore<ApplicationUser>(db);
                var userManager = new UserManager<ApplicationUser>(userStore);
                //se busca el usuario
                var usu = userManager.FindById(id);
                //se elimina
                userManager.Delete(usu);


                db.SaveChanges();
                return Json(new { success = bsuccess });
            }
            catch (Exception ex)
            {
                return Json(new { success = false });
            }
        }
    }
}
