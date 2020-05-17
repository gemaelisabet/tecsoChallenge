using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Challenge.Models
{
    public class FiltroReporteCursosPorAlumno
    {
        public int AlumnoID { get; set; }

        public virtual Alumno Alumno { get; set; }
    }
}