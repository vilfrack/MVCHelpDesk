﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCHelpDesk.Models
{
    public class Perfiles
    {
        [Key]
        public int IDPerfil { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string rutaImg { get; set; }
        public int IDDepartamento { get; set; }
        public int UsuarioID { get; set; }

        [ForeignKey("UsuarioID")]
        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual Departamento Departamento { get; set; }
    }
}