using System.ComponentModel.DataAnnotations;

namespace MVCHelpDesk.Models
{
    public class Status
    {
        [Key]
        public int StatusID { get; set; }

        public string nombre { get; set; }

    }
}