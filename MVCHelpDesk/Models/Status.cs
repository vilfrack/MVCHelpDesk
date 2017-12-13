using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCHelpDesk.Models
{
    public class Status
    {
        [Key]
        public int StatusID { get; set; }

        public string nombre { get; set; }

        [ForeignKey("StatusID")]
        public virtual ICollection<MaestroTaskStatus> MaestroTaskStatus { get; set; }
    }
}