using System.Web;
using System.Web.Optimization;

namespace GuessResult
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/js").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery.validate*",
                        "~/Scripts/bootstrap.js",
                        "~/Scripts/moment-with-locales.js",
                        "~/Scripts/sweetalert2.js",
                        "~/Scripts/angular.min.js",
                        "~/Scripts/bootstrap-datetimepicker.js",
                        "~/Scripts/ui-bootstrap-tpls-2.5.0.min.js",
                        "~/Scripts/locale-all.js"
                        ));


            bundles.Add(new ScriptBundle("~/bundles/js/UserIndex").Include(
          "~/Scripts/Views/User/Index.js"));



            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/bootstrap-datetimepicker.css",
                      "~/Content/sweetalert2.css",
                      "~/Content/site.css"));
        }
    }
}
