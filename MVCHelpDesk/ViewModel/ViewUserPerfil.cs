using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MVCHelpDesk.ViewModel
{
    public class ViewUserPerfil
    {
        public string IDUser { get; set; }
        public int IDPerfil { get; set; }
        //[Column("Name")]//sirve para cambiar los nombre de los campos
        //Display permite visualizar el campo en la vista, si se quita el firts name saldra pegado
        //[Display(Name = "Login")]
        //[Required(ErrorMessage = "El campo {0} No puede quedar vacio")]//el {0} copia el nombre del campo
        //El en el between {1} es el primer parametro y {2} es el segundo
        //al agregarle una cantidad maxima de caracter se le cambia el maximo chart en la base de datos
        //[StringLength(15, ErrorMessage = "El campo {0} debe contener entre {2} y {1} caracteres", MinimumLength = 5)]
        //public string UserName { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "El campo {0} No puede quedar vacio")]
        [StringLength(200, ErrorMessage = "El campo {0} debe contener entre {2} y {1} caracteres", MinimumLength = 6)]
        public string Password { get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El campo {0} No puede quedar vacio")]
        [StringLength(15, ErrorMessage = "El campo {0} debe contener entre {2} y {1} caracteres", MinimumLength = 3)]
        public string Nombre { get; set; }

        [Display(Name = "Apellido")]
        [Required(ErrorMessage = "El campo {0} No puede quedar vacio")]
        [StringLength(15, ErrorMessage = "El campo {0} debe contener entre {2} y {1} caracteres", MinimumLength = 3)]
        public string Apellido { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "El campo {0} No puede quedar vacio")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Direccion del {0} es invalida")]
        public string Email { get; set; }

        public List<string> rol { get; set; }

        public string rutaImg { get; set; }
    }
}