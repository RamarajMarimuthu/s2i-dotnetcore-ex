using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using app.Models;
using System.IO;
using Syncfusion.DocIO.DLS;
using Syncfusion.DocIO;
using Syncfusion.Pdf;
using Syncfusion.DocIORenderer;

namespace app.Controllers
{
    public class HomeController : Controller
    {
    public IActionResult Index(string button)
    {
      if (button == null)
        return View();
      MemoryStream outputStream;
      try
      {
        // Loads document from stream.
        WordDocument document = new WordDocument();
        document.EnsureMinimal();
        document.LastParagraph.AppendText("Document from Openshift");

        ////Instantiation of DocIORenderer for Word to PDF conversion
        DocIORenderer render = new DocIORenderer();
        //Converts Word document into PDF document
        PdfDocument pdfDocument = render.ConvertToPDF(document);
        //Releases all resources used by the Word document and DocIO Renderer objects
        render.Dispose();
        document.Dispose();
        //Saves the PDF file
        outputStream = new MemoryStream();
        pdfDocument.Save(outputStream);
        //Closes the instance of PDF document object
        pdfDocument.Close();

        //outputStream = new MemoryStream();
        //document.Save(outputStream, FormatType.Docx);
        //document.Dispose();

        outputStream.Position = 0;

      }
      catch (Exception e)
      {
        ViewBag.Error = e.Message + e.StackTrace.ToString();
        return View();

      }
      return File(outputStream, "application/pdf", "Test.pdf");
      //return File(outputStream, "application/msword", "Test.docx");
    }

    public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
