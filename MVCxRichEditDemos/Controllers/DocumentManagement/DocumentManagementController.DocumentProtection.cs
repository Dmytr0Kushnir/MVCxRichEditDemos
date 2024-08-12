using System.Web.Mvc;
using DevExpress.Web.Mvc;
using DevExpress.Web.Office;
using DevExpress.XtraRichEdit;
using DevExpress.XtraRichEdit.API.Native;
using DevExpress.XtraRichEdit.Import.Doc;

namespace DevExpress.Web.Demos {
    public partial class DocumentManagementController : DemoController {
        public ActionResult DocumentProtection() {
            return DemoView("DocumentProtection", DocumentProtectionDemoOptions.Current);
        }

      [HttpPost, ValidateInput(false)]
        public ActionResult DocumentProtection(DocumentProtectionDemoOptions options) {
            DocumentProtectionDemoOptions.Current = options;
            string documentId = RichEditExtension.GetDocumentId("RichEdit");
            if(!string.IsNullOrEmpty(documentId))
                DocumentManager.FindDocument(documentId).Close();
            return DemoView("DocumentProtection", DocumentProtectionDemoOptions.Current);
        }  

        public ActionResult DocumentProtectionPartial() {
            return PartialView("DocumentProtectionPartial", DocumentProtectionDemoOptions.Current);
        }

        //public ActionResult EditDocument()
        //{
        //    // Одержуємо поточного користувача та його роль
        //    //var currentUser = User.Identity.Name;
        //    //var userRole = GetUserRole(currentUser);

        //    // Завантажуємо документ у редактор
        //    var document = new RichEditDocumentServer();
        //    document.LoadDocument("path_to_document.docx");

        //    // Налаштовуємо захист документа
        //    if (userRole == "lawyer")
        //    {
        //        document.Document.Sections[0].ProtectedForForms = true;
               
        //        document.Document.Protect("lawyerPassword", DocumentProtectionType.);
        //    }
        //    else if (userRole == "projectmanager")
        //    {
        //        document.Document.Protect("managerPassword");
        //    }

        //    // Повертаємо документ у View
        //    return View(document);
        //}
    }
}
