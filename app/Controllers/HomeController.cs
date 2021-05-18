using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using app.Models;
using System.IO;
using Aspose.Words;

namespace app.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

    public IActionResult Index(string button)
    {
      if (button == null)
        return View();
      MemoryStream outputStream;
      try
      {
        // load the file to be converted
        Document document = new Document();

        DocumentBuilder builder = new DocumentBuilder(document);

        // Specify font formatting
        Font font = builder.Font;
        font.Size = 16;
        font.Bold = true;
        font.Color = System.Drawing.Color.Blue;
        font.Name = "Arial";
        font.Underline = Underline.Dash;

        // Specify paragraph formatting
        ParagraphFormat paragraphFormat = builder.ParagraphFormat;
        paragraphFormat.FirstLineIndent = 8;
        paragraphFormat.Alignment = ParagraphAlignment.Justify;
        paragraphFormat.KeepTogether = true;

        builder.Writeln("Aspose paragraph.");

        //Saves the PDF file
        outputStream = new MemoryStream();
        // save in different formats
        document.Save(outputStream, Aspose.Words.SaveFormat.Pdf);

        //outputStream = new MemoryStream();
        //document.Save(outputStream, FormatType.Docx);
        //document.Dispose();

        outputStream.Position = 0;

      }
      catch (Exception e)
      {
        ViewBag.Error = "SkiaNativeAssets.NoDependencies   " + e.Message + e.StackTrace.ToString();
        return View();

      }
      return File(outputStream, "application/pdf", "Test.pdf");
      //return File(outputStream, "application/msword", "Test.docx");
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
