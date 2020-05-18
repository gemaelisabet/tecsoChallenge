using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Challenge.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using Challenge.Services;

namespace Challenge.Controllers
{
    public class InscripcionController : Controller
    {
        private ChallengeDBContext db = new ChallengeDBContext();
        TecsoLogger _logger;

        public InscripcionController()
        {
            _logger = new TecsoLogger(true, true, true, true, true, true);
        }

        // GET: Inscripcion
        public ActionResult Index()
        {
            var inscripcions = db.Inscripciones.Include(i => i.Alumno).Include(i => i.Curso);
            return View(inscripcions.ToList());
        }

        // GET: Inscripcion/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                _logger.LogError("Inscripcion/Details requiere el id.");
                return RedirectToAction("Index");
            }
            Inscripcion inscripcion = db.Inscripciones.Find(id);
            if (inscripcion == null)
            {
                _logger.LogError("Inscripcion/Details InscripcionID " + id.ToString() + " inexistente.");
                return RedirectToAction("Index");
            }
            return View(inscripcion);
        }

        // GET: Inscripcion/Create
        public ActionResult Create()
        {
            ViewBag.CursoID = new SelectList(db.Cursos, "CursoID", "Nombre");

            List<SelectListItem> alumnos = db.Alumnos.Select(x => new SelectListItem {
                    Text = x.Apellido + ", " + x.Nombre,
                    Value = x.AlumnoID.ToString()
                }).ToList();

            ViewBag.AlumnoID = new SelectList(alumnos, "Value", "Text");
            return View();
        }

        // POST: Inscripcion/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "InscripcionID,CursoID,AlumnoID,EstadoCursada")] Inscripcion inscripcion)
        {
            if (ModelState.IsValid)
            {
                db.Inscripciones.Add(inscripcion);
                db.SaveChanges();
                _logger.LogMessage("Se ha creado una inscripción nueva. InscripcionID: " + inscripcion.InscripcionID.ToString());
                return RedirectToAction("Index");             
            }

            ViewBag.AlumnoID = new SelectList(db.Alumnos, "AlumnoID", "Apellido", inscripcion.AlumnoID);
            ViewBag.CursoID = new SelectList(db.Cursos, "CursoID", "Nombre", inscripcion.CursoID);
            return View(inscripcion);
        }

        // GET: Inscripcion/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                _logger.LogError("Inscripcion/Edit requiere el id.");
                return RedirectToAction("Index");
            }
            Inscripcion inscripcion = db.Inscripciones.Find(id);
            if (inscripcion == null)
            {
                _logger.LogError("Inscripcion/Edit InscripcionID " + id.ToString() + " inexistente.");
                return RedirectToAction("Index");
            }
            ViewBag.AlumnoID = new SelectList(db.Alumnos, "AlumnoID", "Apellido", inscripcion.AlumnoID);
            ViewBag.CursoID = new SelectList(db.Cursos, "CursoID", "Nombre", inscripcion.CursoID);
            return View(inscripcion);
        }

        // POST: Inscripcion/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "InscripcionID,CursoID,AlumnoID,EstadoCursada")] Inscripcion inscripcion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(inscripcion).State = EntityState.Modified;
                db.SaveChanges();
                _logger.LogMessage("Se ha editado una inscripción. InscripcionID: " + inscripcion.InscripcionID.ToString());
                return RedirectToAction("Index");
            }
            ViewBag.AlumnoID = new SelectList(db.Alumnos, "AlumnoID", "Apellido", inscripcion.AlumnoID);
            ViewBag.CursoID = new SelectList(db.Cursos, "CursoID", "Nombre", inscripcion.CursoID);
            return View(inscripcion);
        }

        // GET: Inscripcion/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                _logger.LogError("Inscripcion/Delete requiere el id.");
                return RedirectToAction("Index");
            }
            Inscripcion inscripcion = db.Inscripciones.Find(id);
            if (inscripcion == null)
            {
                _logger.LogError("Inscripcion/Delete InscripcionID " + id.ToString() + " inexistente.");
                return RedirectToAction("Index");
            }
            return View(inscripcion);
        }

        // POST: Inscripcion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Inscripcion inscripcion = db.Inscripciones.Find(id);
            db.Inscripciones.Remove(inscripcion);
            db.SaveChanges();
            _logger.LogMessage("Se ha eliminado una inscripción. InscripcionID: " + id.ToString());

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
