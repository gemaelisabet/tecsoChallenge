using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Challenge.Models
{
    [Table("Alumnos")]
    public class Alumno
    { 

        public int AlumnoID { get; set; }
        public string Apellido { get; set; }
        public string Nombre { get; set; }
       
        public virtual ICollection<Inscripcion> Inscripciones { get; set; }    
    }

    //public class AlumnoDBContext : DbContext
    //{
    //    public DbSet<Alumno> Alumnos { get; set; }
    //}

}