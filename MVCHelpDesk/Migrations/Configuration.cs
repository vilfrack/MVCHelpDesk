namespace MVCHelpDesk.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MVCHelpDesk.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "MVCHelpDesk.Models.ApplicationDbContext";
        }

        protected override void Seed(MVCHelpDesk.Models.ApplicationDbContext context)
        {
            //var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            // Create Admin Role
            var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            string roleName = "Administrador";
            IdentityResult roleResult;

            // Check to see if Role Exists, if not create it
            if (!RoleManager.RoleExists(roleName))
            {
                roleResult = RoleManager.Create(new IdentityRole(roleName));
            }
            //SE CREA AL USUARIO
            if (!(context.Users.Any(u => u.UserName == "admin@admin.com")))
            {
                var userStore = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(userStore);
                var userToInsert = new ApplicationUser
                {
                    UserName = "admin@admin.com",
                    Email = "admin@admin.com"
                };
                userManager.Create(userToInsert, "123456");
                //asociamos el rol al usuario
                userManager.AddToRole(userToInsert.Id, "Administrador");

                context.Perfiles.AddOrUpdate(
                new Models.Perfiles { UsuarioID = userToInsert.Id, Nombre = "Administrador", }
                );
            }
            //OBTENEMOS EL USUARIO DE ADMINISTRADOR
            var User = context.Users.Where(w => w.UserName == "admin@admin.com").SingleOrDefault();
            //AGREMOS LOS STATUS
            context.Status.AddOrUpdate(
                new Models.Status { StatusID = 1, nombre = "Asignado" },
                new Models.Status { StatusID = 2, nombre = "Desarrollo"},
                new Models.Status { StatusID = 3, nombre = "Realizados"},
                new Models.Status { StatusID = 4, nombre = "Rechazados"}
            );
            //AGREGAMOS LOS PERMISOS
            context.Permisos.AddOrUpdate(
                new Models.Permisos { PermisoID = 1, Descripcion = "Ver" },
                new Models.Permisos { PermisoID = 2, Descripcion = "Crear" },
                new Models.Permisos { PermisoID = 3, Descripcion = "Modificar" },
                new Models.Permisos { PermisoID = 4, Descripcion = "Eliminar" }
            );
        }
    }
}
