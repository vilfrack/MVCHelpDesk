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
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            // Create Admin Role
            string roleName = "Administrador";
            IdentityResult roleResult;

            // Check to see if Role Exists, if not create it
            if (!RoleManager.RoleExists(roleName))
            {
                roleResult = RoleManager.Create(new IdentityRole(roleName));
            }
        }
    }
}
