using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Challenge.Models
{
    [Table("Inscripciones")]
    public class Inscripcion
    {
        public int InscripcionID { get; set; }
        public int CursoID { get; set; }
        public int AlumnoID { get; set; }

        public virtual Curso Curso { get; set; }
        public virtual Alumno Alumno { get; set; }
    
        public EstadoCursada EstadoCursada { get; set; }
    }

    public enum EstadoCursada
    {
        [Display(Name = "Pendiente")]
        Pendiente = 0,
        [Display(Name = "En Curso")]
        EnCurso = 1,
        [Display(Name = "Aprobado")]
        Aprobado = 2,
        [Display(Name = "Desaprobado")]
        Desaprobado = 3
    }
}