using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        // Loads document from stream.
        WordDocument document = new WordDocument();
        document.EnsureMinimal();
        document.LastParagraph.AppendText("Document from Openshift");

        outputStream = new MemoryStream();
        document.Save(outputStream, FormatType.Docx);
        document.Dispose();

        outputStream.Position = 0;

      }
      catch (Exception e)
      {
        ViewBag.Error = e.Message + e.StackTrace.ToString();
        return View();

      }
      return File(outputStream, "application/msword", "Test.docx");
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
