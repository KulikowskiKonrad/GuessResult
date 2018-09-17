﻿using System.Web;
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
                        "~/Scripts/jquery.dataTables.js",
                        "~/Scripts/moment-with-locales.js",
                        "~/Scripts/sweetalert2.js",
                        "~/Scripts/angular.min.js",
                        "~/Scripts/bootstrap-datetimepicker.js",
                        "~/Scripts/angular-fusioncharts.js",
                        "~/Scripts/fusioncharts.js",
                        "~/Scripts/fusiondesign.js",
                        "~/Scripts/fusioncharts.charts.js",
                        "~/Scripts/angular-datatables.js",
                        "~/Scripts/dataTables.bootstrap.js",
                        "~/Scripts/fusioncharts.theme.fusion.js",
                        "~/Scripts/fusioncharts.theme.candy.js",
                        "~/Scripts/Common.js"

                        ));

            bundles.Add(new ScriptBundle("~/bundles/js/UserIndex").Include(
          "~/Scripts/Views/User/Index.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/UsersStatistics").Include(
              "~/Scripts/Views/User/UsersStatistics.js"
              ));


            bundles.Add(new ScriptBundle("~/bundles/js/Statystyki").Include(
               "~/Scripts/Views/User/Statystyki.js"
               ));

            bundles.Add(new ScriptBundle("~/bundles/js/EventList").Include(
             "~/Scripts/Views/Event/EventList.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      //"~/Content/angular-datatables.css",
                      //"~/Content/dataTables.bootstrap.css",
                      "~/Content/bootstrap-datetimepicker.css",
                      "~/Content/sweetalert2.css",
                      "~/Content/awesome-bootstrap-checkbox.css",
                      "~/Content/font-awesome.css",
                      "~/Content/site.css"));
        }
    }
}
