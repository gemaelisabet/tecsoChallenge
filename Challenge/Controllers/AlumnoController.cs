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
    public class AlumnoController : Controller
    {
        private ChallengeDBContext db = new ChallengeDBContext();
        TecsoLogger _logger;

        public AlumnoController()
        {           
            _logger = new TecsoLogger(true, true, true, true, true, true);
        }

        // GET: Alumno
        public ActionResult Index()
        {
           return View(db.Alumnos.ToList());
        }

        // GET: Alumno/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                HttpStatusCodeResult error = new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                _logger.LogError("Alumno/Details requiere el id.");
                return RedirectToAction("Index");
            }
            Alumno alumno = db.Alumnos.Find(id);
            if (alumno == null)
            {
                _logger.LogError("Alumno/Details AlumnoID "+ id.ToString() +" inexistente.");
                //return HttpNotFound();
                return RedirectToAction("Index");
            }
            return View(alumno);
        }

        // GET: Alumno/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Alumno/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Apellido,Nombre")] Alumno alumno)
        {
            if (ModelState.IsValid)
            {
                db.Alumnos.Add(alumno);
                db.SaveChanges();

                _logger.LogMessage("Se ha creado un alumno nuevo");
                return RedirectToAction("Index");
            }
            else
            {
                _logger.LogError("Ha ocurrido un error en la creación del alumno");
                return View(alumno);
            }
                    
        }

        // GET: Alumno/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                _logger.LogError("Alumno/Details requiere el id.");
                return RedirectToAction("Index");
            }
            Alumno alumno = db.Alumnos.Find(id);
            if (alumno == null)
            {
                //return HttpNotFound();
                _logger.LogError("Alumno/Details AlumnoID " + id.ToString() + " inexistente.");               
                return RedirectToAction("Index");
            }
            return View(alumno);
        }

        // POST: Alumno/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Apellido,Nombre")] Alumno alumno)
        {
            if (ModelState.IsValid)
            {
                db.Entry(alumno).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(alumno);
        }

        // GET: Alumno/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Alumno alumno = db.Alumnos.Find(id);
            if (alumno == null)
            {
                return HttpNotFound();
            }
            return View(alumno);
        }

        // POST: Alumno/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Alumno alumno = db.Alumnos.Find(id);
            db.Alumnos.Remove(alumno);
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
