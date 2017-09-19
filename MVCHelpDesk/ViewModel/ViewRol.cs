using System.ComponentModel.DataAnnotations;
namespace MVCHelpDesk.ViewModel
{
    public class ViewRol
    {
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}