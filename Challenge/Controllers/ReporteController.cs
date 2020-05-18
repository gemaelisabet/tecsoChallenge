using Challenge.Models;
using Challenge.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Challenge.Controllers
{
    public class ReporteController : Controller
    {
     
        private ChallengeDBContext db = new ChallengeDBContext();
        TecsoLogger _logger;

        public ReporteController()
        {
            _logger = new TecsoLogger(true, true, true, true, true, true);
        }

        public ActionResult ReporteCursosPorAlumno()
        {
            FiltroReporteCursosPorAlumno filtro = new FiltroReporteCursosPorAlumno();
            List<SelectListItem> alumnos = db.Alumnos.Select(x => new SelectListItem
            {
                Text = x.Apellido + ", " + x.Nombre,
                Value = x.AlumnoID.ToString()
            }).ToList();

            ViewBag.AlumnoID = new SelectList(alumnos, "Value", "Text");

            return View("FiltroCursosPorAlumno", filtro);

        }

        [HttpPost]
        public ActionResult ReporteCursosPorAlumno(FiltroReporteCursosPorAlumno filtro)
        {
            try
            {
                var lista = db.Inscripciones.Where(x => x.AlumnoID == filtro.AlumnoID);

                lista = lista.OrderBy(x => x.Curso.Nombre);

                ViewBag.Documentos = lista.ToList();

                string nombre = db.Alumnos.Find(filtro.AlumnoID).Nombre.ToString();
                Response.AddHeader("content-disposition", "attachment; filename=ReporteCursos_" + nombre + ".xls");

                Response.ContentType = "application/ms-excel";
                Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1252");
                Response.Charset = "utf-8";

                return View();
            }
            catch (Exception e)
            {
                _logger.LogError("Error en Reporte Cursos por Alumno. " + e.Message.ToString());
               
                List<SelectListItem> alumnos = db.Alumnos.Select(x => new SelectListItem
                {
                    Text = x.Apellido + ", " + x.Nombre,
                    Value = x.AlumnoID.ToString()
                }).ToList();

                ViewBag.AlumnoID = new SelectList(alumnos, "Value", "Text");

                return View("FiltroCursosPorAlumno", filtro);
            }
            
        }

        public ActionResult ReporteAlumnosPorCurso()
        {
            FiltroReporteAlumnosPorCurso filtro = new FiltroReporteAlumnosPorCurso();
            List<SelectListItem> cursos = db.Cursos.Select(x => new SelectListItem
            {
                Text = x.Nombre,
                Value = x.CursoID.ToString()
            }).ToList();

            ViewBag.CursoID = new SelectList(cursos, "Value", "Text");

            return View("FiltroAlumnosPorCurso", filtro);

        }

        [HttpPost]
        public ActionResult ReporteAlumnosPorCurso(FiltroReporteAlumnosPorCurso filtro)
        {
            try
            {
                var lista = db.Inscripciones.Where(x => x.CursoID == filtro.CursoId);

                lista = lista.OrderBy(x => x.Alumno.Apellido).ThenBy(x => x.Alumno.Nombre);

                ViewBag.Documentos = lista.ToList();

                string nombre = db.Cursos.Find(filtro.CursoId).Nombre.ToString();
                Response.AddHeader("content-disposition", "attachment; filename=ReporteAlumnos_" + nombre + ".xls");

                Response.ContentType = "application/ms-excel";
                Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1252");
                Response.Charset = "utf-8";

                return View();
            }
            catch (Exception e){

                _logger.LogError("Error en Reporte Aliumnos por Curso. " + e.Message.ToString());
             
                List<SelectListItem> cursos = db.Cursos.Select(x => new SelectListItem
                {
                    Text = x.Nombre,
                    Value = x.CursoID.ToString()
                }).ToList();

                ViewBag.CursoID = new SelectList(cursos, "Value", "Text");

                return View("FiltroAlumnosPorCurso", filtro);
            }
            
        }
    }
}