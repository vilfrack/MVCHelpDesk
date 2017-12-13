using System.Web;
using System.Web.Optimization;

namespace MVCHelpDesk
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            /*SE DEBE ORDENAR MEJOR*/
            bundles.Add(new ScriptBundle("~/bundles/script").Include(
              "~/Scripts/jquery-3.2.1.min.js",

              "~/Scripts/jquery-ui-1.12.1.custom/jquery-ui.js",
              "~/Scripts/fileinput.js",
              "~/Scripts/bootstrap.js",
              //"~/Scripts/plugins/morris/morris.js",
              //"~/Scripts/plugins/morris/morris-data.js",
              "~/Scripts/plugins/DataTables-1.10.15/media/js/jquery.dataTables.js",//para el grid
              "~/Scripts/plugins/DataTables-1.10.15/media/js/dataTables.bootstrap.js",//para el grid
              "~/Scripts/Apps/task/task.js").IncludeDirectory("~/Scripts", ".js"));

            bundles.Add(new ScriptBundle("~/bundles/scriptOperations").Include(
               "~/Scripts/Apps/OperationsTaks/OperationsTaks.js").IncludeDirectory("~/Scripts", ".js"));

            bundles.Add(new ScriptBundle("~/bundles/scriptUsuario").Include(
                "~/Scripts/Apps/Usuario/Usuario.js").IncludeDirectory("~/Scripts", ".js"));

            bundles.Add(new ScriptBundle("~/bundles/scriptDepartamento").Include(
                "~/Scripts/Apps/Departamento/departamento.js").IncludeDirectory("~/Scripts", ".js"));

            bundles.Add(new ScriptBundle("~/bundles/scriptRoles").Include(
                "~/Scripts/Apps/Roles/roles.js").IncludeDirectory("~/Scripts", ".js"));

            bundles.Add(new ScriptBundle("~/bundles/scriptPermisos").Include(
                "~/Scripts/Apps/Permisos/Permisos.js").IncludeDirectory("~/Scripts", ".js"));

            bundles.Add(new ScriptBundle("~/bundles/scriptModulos").Include(
               "~/Scripts/Apps/Modulos/Modulos.js").IncludeDirectory("~/Scripts", ".js"));

            bundles.Add(new ScriptBundle("~/bundles/scriptAsignar").Include(
              "~/Scripts/Apps/Asignar/Asignar.js").IncludeDirectory("~/Scripts", ".js"));

            bundles.Add(new ScriptBundle("~/bundles/DataPikerJs").Include(
                "~/Scripts/bootstrap-datepicker.js").IncludeDirectory("~/Scripts", ".js"));

            bundles.Add(new StyleBundle("~/bundles/css").Include(
                "~/Content/bootstrap-3.3.7-dist/css/bootstrap.css",

                "~/Content/fileinput.css",
                "~/Content/main.css",
                "~/Content/sb-admin.css",
                 "~/Content/morris.css"
                ).IncludeDirectory("~/Content", ".css"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryUI").Include(
                "~/Content/jquery-ui.css").IncludeDirectory("~/Content", ".css"));

            bundles.Add(new ScriptBundle("~/bundles/DataPikerCss").Include(
            "~/Content/bootstrap-datepicker.css").IncludeDirectory("~/Content", ".css"));


            bundles.Add(new ScriptBundle("~/bundles/KanbanCss").Include(
                "~/Content/kanban.css").IncludeDirectory("~/Content", ".css"));

            bundles.Add(new StyleBundle("~/bundles/cssLogin").Include(
                "~/Content/bootstrap-3.3.7-dist/css/bootstrap.css",
                "~/Content/main.css"
                ).IncludeDirectory("~/Content", ".css"));


        }
    }
}
