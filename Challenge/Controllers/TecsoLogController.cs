using Challenge.Models;
using Challenge.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Challenge.Controllers
{
    public class TecsoLogController : Controller
    {
        private ChallengeDBContext db = new ChallengeDBContext();
        TecsoLogger _logger;

        // GET: TecsoLog
        public ActionResult Index()
        {
            return View(db.TecsoLogs.ToList());
        }

        // GET: TecsoLog/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TecsoLog tecsoLog = db.TecsoLogs.Find(id);
            if (tecsoLog == null)
            {
                return HttpNotFound();
            }
            return View(tecsoLog);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TecsoLog tecsoLog = db.TecsoLogs.Find(id);
            if (tecsoLog == null)
            {
                return HttpNotFound();
            }
            return View(tecsoLog);
        }

        // POST: TecsoLog/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TecsoLog tecsoLog = db.TecsoLogs.Find(id);
            db.TecsoLogs.Remove(tecsoLog);
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