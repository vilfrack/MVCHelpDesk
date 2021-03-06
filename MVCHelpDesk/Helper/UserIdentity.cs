﻿using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MVCHelpDesk.Models;
using MVCHelpDesk.Permisos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MVCHelpDesk.Attribute;
namespace MVCHelpDesk.Helper
{
    public class UserIdentity
    {
        private static ApplicationDbContext db = new ApplicationDbContext();
        private static UserStore<ApplicationUser> userStore = new UserStore<ApplicationUser>(db);
        private static UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(userStore);
        //CREAMOS LA INSTANCINA PARA VALIDAR EL PERMISO DE ASIGANAR
        private ModuloAttribute modeloAtribute = new ModuloAttribute();

        public string GetIdUser()
        {
            string idUser = string.Empty;
            if (HttpContext.Current.User != null)
            {
                idUser = HttpContext.Current.User.Identity.GetUserId();
            }
            return idUser;
        }
        public bool ValidarPermiso(AllPermisos permiso,AllModulos modulo)
        {
            var usuario = GetIdUser();
            if (!modeloAtribute.PermisoByRol(modulo, permiso) && !modeloAtribute.PermisoByUser(modulo, permiso))
            {
                return false;
            }

            return true;
        }
    }

}