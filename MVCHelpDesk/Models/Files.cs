using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCHelpDesk.Models
{
    public class Files
    {
        [Key]
        public int IDFiles { get; set; }

        public string ruta { get; set; }

        public string nombre { get; set; }

        public string ruta_virtual { get; set; }

        public int TasksID { get; set; }

        [ForeignKey("TasksID")]
        public virtual Tasks Tasks { get; set; }
    }
}