using System.Web.Mvc;

namespace DevExpress.Web.Demos {
    public partial class EditingFeaturesController : DemoController {
        public ActionResult Hyperlinks() {
            return DemoView("Hyperlinks", HyperlinksAndBookmarksDemoOptions.Current);
        }
        [HttpPost]
        public ActionResult Hyperlinks(HyperlinksAndBookmarksDemoOptions options) {
            HyperlinksAndBookmarksDemoOptions.Current = options;
            return DemoView("Hyperlinks", options);
        }
        public ActionResult HyperlinksPartial() {
            return PartialView("HyperlinksPartial", HyperlinksAndBookmarksDemoOptions.Current);
        }
    }
}
