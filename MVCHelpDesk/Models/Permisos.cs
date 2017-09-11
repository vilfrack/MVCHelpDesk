﻿using System.ComponentModel.DataAnnotations;

namespace MVCHelpDesk.Models
{
    public class Permisos
    {
        [Key]
        public int PermisoID { get; set; }
        public string Modulo { get; set; }
        public string Descripcion { get; set; }
    }
}