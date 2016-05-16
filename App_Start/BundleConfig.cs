using System.Web;
using System.Web.Optimization;

namespace TV2Presets2
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/angular-busy.min.css"));

            bundles.Add(new StyleBundle("~/Content/kendo/css").Include(
                "~/Content/kendo/kendo.common.min.css",
                "~/Content/kendo/kendo.metroblack.min.css",
                "~/Content/kendo/kendo.dataviz.min.css",
                "~/Content/kendo/kendo.dataviz.metroblack.min.css"));

            bundles.Add(new Bundle("~/bundles/files/scripts")
                .Include("~/Scripts/jquery-{version}.js")
                .Include("~/Scripts/bootstrap.js")
                .Include("~/Scripts/respond.js")
                .Include("~/Scripts/angular.min.js")
                .Include("~/Scripts/angular-route.min.js")
                .Include("~/Scripts/angular-animate.min.js")
                .Include("~/Scripts/kendo/kendo.all.min.js")
                .Include("~/Scripts/angular-kendo.min.js")
                .Include("~/Scripts/angular-ui/ui-bootstrap-tpls.min.js")
                .Include("~/Scripts/jquery.mask.min.js")
                .Include("~/Scripts/angular-busy.min.js"));
        }
    }
}
