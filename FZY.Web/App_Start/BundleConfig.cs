using System.Web.Optimization;

namespace FZY.Web
{
    public static class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.IgnoreList.Clear();

            //VENDOR RESOURCES

            //~/Bundles/vendor/css
            bundles.Add(
                new StyleBundle("~/Bundles/vendor/css")
                    .Include("~/Content/reset.css", new CssRewriteUrlTransform())
                    .Include("~/Content/style.css", new CssRewriteUrlTransform())
                );


            //~/Bundles/vendor/bottom (Included in the bottom for fast page load)
            bundles.Add(
                new ScriptBundle("~/Bundles/vendor/js/top")
                    .Include(
                        "~/Scripts/jquery-2.2.0.min.js"
                    )
                );


  

        }
    }
}