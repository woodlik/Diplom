using System.Web;
using System.Web.Optimization;

namespace GoSport.Client
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.

            RegisterScripts(bundles);
            RegisterStyles(bundles);
        }

        public static void RegisterScripts(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
          "~/Scripts/kendo/jquery.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
               "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Scripts/bootstrap.js",
                "~/Scripts/respond.js"));

            // For Kendo
            bundles.Add(new ScriptBundle("~/bundles/kendo").Include(
                "~/Scripts/kendo/kendo.all.min.js",
                "~/Scripts/kendo/kendo.aspnetmvc.min.js"));

            //Jquery vizualization
            bundles.Add(new StyleBundle("~/bundles/Slider").Include(
                "~/Scripts/ImageSlider/jquery.bxslider.js",
                "~/Scripts/ImageSlider/jquery.bxslider.min.js"));

            //app
            bundles.Add(new ScriptBundle("~/bundles/app").Include(
               "~/Scripts/application/application.js"));
        }

        public static void RegisterStyles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/bootstrap.css",
                "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/Slider").Include(
               "~/Content/ImageSlider/jquery.bxslider.css"));

            bundles.Add(new StyleBundle("~/Content/kendo-css").Include(
                "~/content/kendo-css/kendo.common.min.css",
                "~/content/kendo-css/kendo.metro.min.css",
                "~/content/kendo-css/kendo.default.*"));
        }
    }
}
