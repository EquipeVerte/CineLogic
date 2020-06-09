using System.Web;
using System.Web.Optimization;

namespace CineLogic
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            BundleTable.EnableOptimizations = false;

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

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
                      "~/Content/Site.css"));

            bundles.Add(new StyleBundle("~/Content/fullcalendarcss").Include(
                    "~/Scripts/Fullcalendar/core/main.css",
                    "~/Scripts/Fullcalendar/daygrid/main.css",
                    "~/Scripts/Fullcalendar/timegrid/main.css",
                    "~/Scripts/Fullcalendar/interaction/main.css",
                    "~/Scripts/Fullcalendar/bootstrap/main.css"
                ));

            bundles.Add(new ScriptBundle("~/Content/fullcalendarjs").Include(
                    "~/Scripts/Fullcalendar/core/main.js",
                    "~/Scripts/Fullcalendar/daygrid/main.js",
                    "~/Scripts/Fullcalendar/timegrid/main.js",
                    "~/Scripts/Fullcalendar/interaction/main.js",
                    "~/Scripts/Fullcalendar/bootstrap/main.js",
                     "~/Scripts/Fullcalendar/core/locales-all.js"
                ));

            bundles.Add(new StyleBundle("~/Content/bootstrapdatepickercss").Include(
                    "~/Scripts/Datepicker/css/bootstrap-datepicker.css"
                ));

            bundles.Add(new ScriptBundle("~/Content/bootstrapdatepickerjs").Include(
                    "~/Scripts/Datepicker/js/bootstrap-datepicker.js",
                    "~/Scripts/Datepicker/locales/bootstrap-datepicker.fr.min.js"
                ));

            bundles.Add(new ScriptBundle("~/Content/fontawesomejs").Include(
                    "~/Scripts/FontAwesome/all.js"
                ));

            bundles.Add(new ScriptBundle("~/Content/Programmation").Include(
                    "~/Scripts/Programmation/ChangeManagement.js",
                    "~/Scripts/Programmation/AjaxLoader.js",
                    "~/Scripts/Programmation/DatePicker.js",
                    "~/Scripts/Programmation/Calendar.js",
                    "~/Scripts/Programmation/SalleSelection.js",
                    "~/Scripts/Programmation/SeanceCreation.js"
                ));
        }
    }
}
