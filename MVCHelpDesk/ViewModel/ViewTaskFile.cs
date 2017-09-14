using System.Collections.Generic;
namespace MVCHelpDesk.ViewModel
{
    public class ViewTaskFile
    {
        public int TaskID { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public List<string> ruta { get; set; }
        public List<string> ruta_virtual { get; set; }
        public List<int> IDFiles { get; set; }
        public string nombre { get; set; }
        public string status { get; set; }
    }
}