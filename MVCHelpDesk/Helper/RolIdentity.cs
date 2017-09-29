using Microsoft.AspNet.Identity.EntityFramework;
using MVCHelpDesk.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCHelpDesk.Helper
{
    public class RolIdentity : UserIdentity
    {
        private static ApplicationDbContext db = new ApplicationDbContext();

        public List<IdentityRole> GetRolByUser() {

            string idUser = GetIdUser();

            var RolByUser = (db.Users
                    .Where(u => u.Id == idUser)
                    .SelectMany(u => u.Roles)
                    .Join(db.Roles, ur => ur.RoleId, r => r.Id, (ur, r) => r)
                    ).ToList();

            return RolByUser;
        }
    }
}