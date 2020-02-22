using System.Web;
using System.Web.Optimization;

namespace SSL_SMS
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.bundle.js",
                      "~/Scripts/bootbox.js",
                      "~/Scripts/toastr.js",
                      "~/Scripts/DataTables/jquery.dataTables.js",
                      "~/Scripts/DataTables/dataTables.bootstrap4.js",
                      "~/Scripts/custom.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/font-awesome.css",
                      "~/Content/animate.css",
                      "~/Content/custom.css",
                      "~/Content/toastr.css",
                      "~/Content/DataTables/css/dataTables.bootstrap4.css",
                      "~/Content/site.css"));
        }
    }
}
