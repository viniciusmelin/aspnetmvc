using System.Web;
using System.Web.Optimization;

namespace App.Game
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery-ui-1.12.1.min.js",
                        "~/Scripts/DataTables/jquery.dataTables.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                       
                      "~/Content/bootstrap.css",
                       "~/Content/DataTables/css/*.cs",
                      "~/Content/animate.min.css",
                      "~/Content/demo.css",
                      "~/Content/light-bootstrap-dashboard.css",
                      "~/Content/pe-icon-7-stroke.css"));

            bundles.Add(new StyleBundle("~/Content/csslogin").Include("~/Content/login/login.css", new CssRewriteUrlTransform()));
           bundles.Add(new StyleBundle("~/Content/csslogin").Include(
                "~/Content/login/form-elements.css",
                "~/Content/login/style.css"));
            bundles.Add(new ScriptBundle("~/Content/jslogin").Include(
                "~/Scripts/login/jquery.backstretch.min.js", "~/Scripts/login/placeholder.js", "~/Scripts/login/scripts.js"));


          bundles.Add(new ScriptBundle("~/bundles/tema").Include(
                "~/Scripts/light-bootstrap-dashboard.js",
                "~/Scripts/demo.js",
                 "~/Scripts/bootstrap-select.js",
                "~/Scripts/notify.min.js"
                ));
        }
    }
}
