using System.Web;
using System.Web.Optimization;

namespace MVCHelpDesk
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/script").Include(
              "~/Scripts/jquery-3.2.1.min.js",

              "~/Scripts/jquery-ui-1.12.1.custom/jquery-ui.js",
              "~/Scripts/fileinput.js",
              "~/Scripts/bootstrap.js",
              "~/Scripts/plugins/morris/morris.js",
              "~/Scripts/plugins/morris/morris-data.js",
              "~/Scripts/plugins/DataTables-1.10.15/media/js/jquery.dataTables.js",//para el grid
              "~/Scripts/plugins/DataTables-1.10.15/media/js/dataTables.bootstrap.js",//para el grid
              "~/Scripts/Apps/task/task.js").IncludeDirectory("~/Scripts", ".js"));

            bundles.Add(new ScriptBundle("~/bundles/scriptOperations").Include(
               "~/Scripts/Apps/OperationsTaks/OperationsTaks.js").IncludeDirectory("~/Scripts", ".js"));

            bundles.Add(new ScriptBundle("~/bundles/scriptUsers").Include(
                "~/Scripts/Apps/Users/users.js").IncludeDirectory("~/Scripts", ".js"));

            bundles.Add(new ScriptBundle("~/bundles/scriptDepartamento").Include(
                "~/Scripts/Apps/Departamento/departamento.js").IncludeDirectory("~/Scripts", ".js"));

            bundles.Add(new ScriptBundle("~/bundles/scriptRoles").Include(
                "~/Scripts/Apps/Roles/roles.js").IncludeDirectory("~/Scripts", ".js"));

            bundles.Add(new StyleBundle("~/bundles/css").Include(
                "~/Content/css/bootstrap-3.3.7-dist/css/bootstrap.css",

                "~/Content/css/fileinput.css",
                "~/Content/css/main.css",
                "~/Content/css/sb-admin.css",
                 "~/Content/css/morris.css"
                ).IncludeDirectory("~/Content", ".css"));

        }
    }
}
