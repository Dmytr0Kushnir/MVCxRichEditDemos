using DevExpress.XtraRichEdit;
using System.Web.Mvc;
using DevExpress.XtraPrinting;
using DevExpress.XtraRichEdit.API.Native;
using Document = DevExpress.XtraRichEdit.API.Native.Document;

namespace DevExpress.Web.Demos
{
    public partial class DocumentManagementController : DemoController
    {
        public ActionResult LoadAndSave()
        {
            return DemoView("LoadAndSave");
        }
        public ActionResult LoadAndSavePartial()
        {
            return PartialView("LoadAndSavePartial");
        }

        public static void Document_CalculateDocumentVariable(object sender, CalculateDocumentVariableEventArgs e)
        {
            switch (e.VariableName)
            {
                case "TABLE":
                    var doc1 = new RichEditDocumentServer();
                    Table table = doc1.Document.Tables.Create(doc1.Document.Range.Start, 2, 4);

                    doc1.Document.InsertSingleLineText(table.Rows[0].Cells[0].Range.Start, "ID");
                    doc1.Document.InsertSingleLineText(table.Rows[0].Cells[1].Range.Start, "Photo");
                    doc1.Document.InsertSingleLineText(table.Rows[0].Cells[2].Range.Start, "Customer Info");
                    doc1.Document.InsertSingleLineText(table.Rows[0].Cells[3].Range.Start, "Rentals");

                    for (int i = 1; i < 2; i++)
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

                    for (int i = 1; i < 2; i++)
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
    }
}
