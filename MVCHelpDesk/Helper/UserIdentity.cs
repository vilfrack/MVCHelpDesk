using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MVCHelpDesk.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCHelpDesk.Helper
{
    public class UserIdentity
    {
        private static ApplicationDbContext db = new ApplicationDbContext();
        private static UserStore<ApplicationUser> userStore = new UserStore<ApplicationUser>(db);
        private static UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(userStore);

        public string GetIdUser()
        {
            string idUser = string.Empty;
            if (HttpContext.Current.User != null)
            {
                idUser = HttpContext.Current.User.Identity.GetUserId();
            }
            return idUser;
        }
       
    }

}