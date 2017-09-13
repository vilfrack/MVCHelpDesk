using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace MVCHelpDesk
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<Models.ApplicationDbContext,
                                MVCHelpDesk.Migrations.Configuration>());
            //se pasan dos colecciones, uno donde esta el model
            //dos donde esta el archivo de migracion
            //CON ESTE CAMBIO CADA VEZ QUE ADICIONE O QUITE CAMPO EL SISTEMA VA A FUNCIONAR
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
