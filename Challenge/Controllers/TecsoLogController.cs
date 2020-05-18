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
                _logger.LogError("TecsoLog/Details requiere el id.");
                return RedirectToAction("Index");
            }
            TecsoLog tecsoLog = db.TecsoLogs.Find(id);
            if (tecsoLog == null)
            {
                _logger.LogError("TecsoLog/Details InscripcionID " + id.ToString() + " inexistente.");
                return RedirectToAction("Index");
            }
            return View(tecsoLog);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                _logger.LogError("TecsoLog/Delete requiere el id.");
                return RedirectToAction("Index");
            }
            TecsoLog tecsoLog = db.TecsoLogs.Find(id);
            if (tecsoLog == null)
            {
                _logger.LogError("TecsoLog/Delete InscripcionID " + id.ToString() + " inexistente.");
                return RedirectToAction("Index");
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
            _logger.LogMessage("Se ha eliminado un Log. TecsoLogID: " + id.ToString());

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