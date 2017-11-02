using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using MVCHelpDesk.Models;
using MVCHelpDesk.Attribute;

namespace MVCHelpDesk.Controllers
{
    [Authorize, ModuloAttribute(modulo = Permisos.AllModulos.Permiso, permisos = Permisos.AllPermisos.Ver)]
    public class PruebaController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [ModuloAttribute(permisos = Permisos.AllPermisos.Ver)]
        public ActionResult Index()
        {
            return View(db.Modulos.ToList());
        }

        // GET: Prueba/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Modulos modulos = db.Modulos.Find(id);
            if (modulos == null)
            {
                return HttpNotFound();
            }
            return View(modulos);
        }

        [ModuloAttribute(permisos = Permisos.AllPermisos.Crear)]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Prueba/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ModuloID,Descripcion")] Modulos modulos)
        {
            if (ModelState.IsValid)
            {
                db.Modulos.Add(modulos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(modulos);
        }

        [ModuloAttribute(permisos = Permisos.AllPermisos.Editar)]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Modulos modulos = db.Modulos.Find(id);
            if (modulos == null)
            {
                return HttpNotFound();
            }
            return View(modulos);
        }

        // POST: Prueba/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ModuloID,Descripcion")] Modulos modulos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(modulos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(modulos);
        }

        [ModuloAttribute(permisos = Permisos.AllPermisos.Eliminar)]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Modulos modulos = db.Modulos.Find(id);
            if (modulos == null)
            {
                return HttpNotFound();
            }
            return View(modulos);
        }

        // POST: Prueba/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Modulos modulos = db.Modulos.Find(id);
            db.Modulos.Remove(modulos);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
