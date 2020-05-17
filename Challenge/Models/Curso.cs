using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Challenge.Models
{
    [Table("Cursos")]
    public class Curso
    {
        public int CursoID { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<Inscripcion> Inscripciones { get; set; }

  
    }

}