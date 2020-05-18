using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Challenge.Models;
using Challenge.Services;

namespace Challenge.Controllers
{
    public class CursoController : Controller
    {
        private ChallengeDBContext db = new ChallengeDBContext();
        TecsoLogger _logger;

        public CursoController()
        {
            _logger = new TecsoLogger(true, true, true, true, true, true);
        }

        // GET: Curso
        public ActionResult Index()
        {
            return View(db.Cursos.ToList());
        }

        // GET: Curso/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                _logger.LogError("Curso/Details requiere el id.");
                return RedirectToAction("Index");
            }
            Curso curso = db.Cursos.Find(id);
            if (curso == null)
            {
                _logger.LogError("Curso/Details CursoID " + id.ToString() + " inexistente.");
                return RedirectToAction("Index");
            }
            return View(curso);
        }

        // GET: Curso/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Curso/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CursoID,Nombre")] Curso curso)
        {
            if (ModelState.IsValid)
            {
                db.Cursos.Add(curso);
                db.SaveChanges();
                _logger.LogMessage("Se ha creado un curso nuevo. CursoID: " + curso.CursoID.ToString());
                return RedirectToAction("Index");
            }

            return View(curso);
        }

        // GET: Curso/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                _logger.LogError("Curso/Edit requiere el id.");
                return RedirectToAction("Index");
            }
            Curso curso = db.Cursos.Find(id);
            if (curso == null)
            {
                _logger.LogError("Curso/Edit CursoID " + id.ToString() + " inexistente.");
                return RedirectToAction("Index");
            }
            return View(curso);
        }

        // POST: Curso/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CursoID,Nombre")] Curso curso)
        {
            if (ModelState.IsValid)
            {
                db.Entry(curso).State = EntityState.Modified;
                db.SaveChanges();
                _logger.LogMessage("Se ha editado un curso. CursoID: " + curso.CursoID.ToString());
                return RedirectToAction("Index");
            }
            return View(curso);
        }

        // GET: Curso/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                _logger.LogError("Curso/Delete requiere el id.");
                return RedirectToAction("Index");
            }
            Curso curso = db.Cursos.Find(id);
            if (curso == null)
            {
                _logger.LogError("Curso/Delete CursoID " + id.ToString() + " inexistente.");
                return RedirectToAction("Index");
            }
            return View(curso);
        }

        // POST: Curso/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Curso curso = db.Cursos.Find(id);
            db.Cursos.Remove(curso);
            db.SaveChanges();
            _logger.LogMessage("Se ha eliminado un curso. CursoID: " + id.ToString());
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
