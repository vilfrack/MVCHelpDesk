using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVCHelpDesk.Models
{
    public class MaestroTaskStatus
    {
        [Key]
        public int IDMaestroTask { get; set; }

        public int? TaskID { get; set; }

        public int? StatusID { get; set; }

        public DateTime? Fecha { get; set; }
    }
}