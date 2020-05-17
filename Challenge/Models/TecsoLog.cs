using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace Challenge.Models
{
    [Table("TecsoLogs")]
    public class TecsoLog
    {
        public int TecsoLogID { get; set; }

        public string Text { get; set; }

        public DateTime DateTime { get; set; }

        public int TipoLog { get; set; }

    }
}