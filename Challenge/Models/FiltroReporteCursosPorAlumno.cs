using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Challenge.Models
{
    public class FiltroReporteAlumnosPorCurso
    {
        public int CursoId { get; set; }

        public virtual Curso Curso { get; set; }
    }
}