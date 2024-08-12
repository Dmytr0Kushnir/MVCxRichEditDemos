using System.Web.Mvc;
using DevExpress.Web.Mvc;
using DevExpress.XtraRichEdit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

using DevExpress.XtraPrinting;
using DevExpress.XtraRichEdit.API.Native;
using Document = DevExpress.XtraRichEdit.API.Native.Document;

namespace DevExpress.Web.Demos {
    public partial class DocumentManagementController : DemoController {
        public ActionResult ExportToPDF() {
            return DemoView("ExportToPDF");
        }
        [HttpPost]
        public ActionResult ExportFile() {
            if (IsExportAction())
                return RichEditExtension.ExportToPDF("RichEdit", "AlbertEinstein");
            return ExportToPDFPartial();
        }
        public ActionResult ExportToPDFPartial() {
            return PartialView("ExportToPDFPartial");
        }
        bool IsExportAction() {
            return Request.Params["Export"] != null;
        }
    }
}
