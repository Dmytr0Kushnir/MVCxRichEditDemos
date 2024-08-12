using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web.Mvc;
using DevExpress.Web.Mvc;
using DevExpress.XtraPrinting;
using DevExpress.XtraRichEdit;
using DevExpress.XtraRichEdit.API.Native;
using Document = DevExpress.XtraRichEdit.API.Native.Document;


namespace DevExpress.Web.Demos {
    public partial class BarsAndUIController : DemoController {
        public ActionResult SimpleView() {
            return DemoView("SimpleView");
        }
        public static void Document_CalculateDocumentVariable(object sender, CalculateDocumentVariableEventArgs e)
        {
            switch (e.VariableName)
            {
                case "TABLE":
                    var doc1 = new RichEditDocumentServer();
                    Table table = doc1.Document.Tables.Create(doc1.Document.Range.Start, 20, 4);

                    doc1.Document.InsertSingleLineText(table.Rows[0].Cells[0].Range.Start, "ID");
                    doc1.Document.InsertSingleLineText(table.Rows[0].Cells[1].Range.Start, "Photo");
                    doc1.Document.InsertSingleLineText(table.Rows[0].Cells[2].Range.Start, "Customer Info");
                    doc1.Document.InsertSingleLineText(table.Rows[0].Cells[3].Range.Start, "Rentals");

                    for (int i = 1; i < 20; i++)
                    {
                        doc1.Document.InsertSingleLineText(table.Rows[i].Cells[0].Range.Start, $"ID {i}");

                        string customerInfo = $"Customer Info {i}\n" +
                                              $"Address: 123 Main St, Apt {i}\n" +
                                              $"Phone: (555) 123-456{i}\n" +
                                              $"Email: customer{i}@example.com";
                        doc1.Document.InsertText(table.Rows[i].Cells[2].Range.Start, customerInfo);

                        string rentalsInfo = $"Rental {i}\n" +
                                             $"Date: 01/01/202{i}\n" +
                                             $"Amount: ${100 * i}\n" +
                                             $"Status: Active";
                        doc1.Document.InsertText(table.Rows[i].Cells[3].Range.Start, rentalsInfo);
                    }

                    for (int i = 1; i < 20; i++)
                    {
                        string imagePath = System.Web.Hosting.HostingEnvironment.MapPath($"~/Content/logo.png");
                        if (System.IO.File.Exists(imagePath))
                        {
                            using (System.IO.FileStream imageStream = new System.IO.FileStream(imagePath, System.IO.FileMode.Open))
                            {
                                var documentImageSource = DevExpress.XtraRichEdit.API.Native.DocumentImageSource.FromStream(imageStream);
                                doc1.Document.Images.Insert(table.Rows[i].Cells[1].Range.Start, documentImageSource);
                            }
                        }
                    }

                    e.Value = doc1.Document;
                    e.Handled = true;
                    doc1.Document.UpdateAllFields();
                    break;
            }
        }

        public ActionResult SimpleViewPartial()
        {
            using (var wordProcessor = new RichEditDocumentServer())
            { 
                wordProcessor.LoadDocument("C:\\Users\\Public\\Documents\\DevExpress Demos 24.1\\Components\\ASP.NET\\CS\\MVCxRichEditDemos\\App_Data\\Documents\\AlbertEinstein.docx");

                // Append another document's content
                wordProcessor.Document.AppendDocumentContent("C:\\Users\\Public\\Documents\\DevExpress Demos 24.1\\Components\\ASP.NET\\CS\\MVCxRichEditDemos\\App_Data\\Documents\\Tables.docx");

                wordProcessor.Document.AppendDocumentContent("C:\\Users\\Public\\Documents\\DevExpress Demos 24.1\\Components\\ASP.NET\\CS\\MVCxRichEditDemos\\App_Data\\Documents\\AutoCorrect.docx");

                wordProcessor.SaveDocument("C:\\Users\\Public\\Documents\\DevExpress Demos 24.1\\Components\\ASP.NET\\CS\\MVCxRichEditDemos\\App_Data\\Documents\\result5.docx", DocumentFormat.OpenXml);
            }

            return PartialView("SimpleViewPartial");
        }


        [HttpPost]
        public ActionResult ExportFile()
        {
            if (IsExportAction())
                return RichEditExtension.ExportToPDF("RichEdit", "FINAL CM031124 Office Buford GA Report");
            return PartialView("SimpleViewPartial");
        }

        bool IsExportAction() {
            return Request.Params["Export"] != null;
        }
    }
}
