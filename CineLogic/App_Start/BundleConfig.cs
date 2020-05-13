using System.Web;
using System.Web.Optimization;

namespace CineLogic
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
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/fullcalendarcss").Include(
                    "~/Content/Fullcalendar/core/main.css",
                    "~/Content/Fullcalendar/daygrid/main.css",
                    "~/Content/Fullcalendar/timegrid/main.css",
                    "~/Content/Fullcalendar/interaction/main.css",
                    "~/Content/Fullcalendar/bootstrap/main.css"
                ));

            bundles.Add(new ScriptBundle("~/Content/fullcalendarjs").Include(
                    "~/Content/Fullcalendar/core/main.js",
                    "~/Content/Fullcalendar/daygrid/main.js",
                    "~/Content/Fullcalendar/timegrid/main.js",
                    "~/Content/Fullcalendar/interaction/main.js",
                    "~/Content/Fullcalendar/bootstrap/main.js",
                     "~/Content/Fullcalendar/core/locales-all.js"
                ));

            bundles.Add(new StyleBundle("~/Content/bootstrapdatepickercss").Include(
                    "~/Content/Datepicker/css/bootstrap-datepicker.css"
                ));

            bundles.Add(new ScriptBundle("~/Content/bootstrapdatepickerjs").Include(
                    "~/Content/Datepicker/js/bootstrap-datepicker.js",
                    "~/Content/Datepicker/locales/bootstrap-datepicker.fr.min.js"
                ));

            bundles.Add(new ScriptBundle("~/Content/fontawesomejs").Include(
                    "~/Content/FontAwesome/all.js"
                ));

            bundles.Add(new ScriptBundle("~/Content/Programmation").Include(
                    "~/Content/Programmation/ChangeManagement.js",
                    "~/Content/Programmation/AjaxLoader.js",
                    "~/Content/Programmation/DatePicker.js",
                    "~/Content/Programmation/Calendar.js",
                    "~/Content/Programmation/SalleSelection.js",
                    "~/Content/Programmation/SeanceCreation.js"
                ));
        }
    }
}
