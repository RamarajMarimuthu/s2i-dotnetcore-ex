using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using app.Models;
using System.IO;
using SkiaSharp;
using Microsoft.AspNetCore.Hosting;

namespace app.Controllers
{
    public class HomeController : Controller
    {
    private readonly IHostingEnvironment _hostingEnvironment;

    public HomeController(IHostingEnvironment hostingEnvironment)
    {
      _hostingEnvironment = hostingEnvironment;
    }

    public IActionResult Index(string button)
    {
      if (button == null)
        return View();
      try
      {
        string basePath = _hostingEnvironment.WebRootPath;
        FileStream fileStreamInput = new FileStream(basePath + @"/fonts/CHILLER.TTF", FileMode.Open, FileAccess.Read);

        SKBitmap bitmap = new SKBitmap(500, 500, SKImageInfo.PlatformColorType, SKAlphaType.Premul);
        var surface = SKSurface.Create(bitmap.Info);
        SKCanvas canvas = surface.Canvas;
        canvas.Clear(SKColors.White);
        var paint = new SKPaint();
        paint.IsAntialias = true;
        paint.Color = SKColors.Red;
        paint.StrokeWidth = 3;
        paint.TextSize = 30;
        string fontName = SKTypeface.Default.FamilyName;
        canvas.DrawCircle(50, 50, 25, paint);

        paint.Typeface = SKTypeface.FromStream(fileStreamInput);
        canvas.DrawText("SkiaSharp", 200, 200, paint);

        var image = SKImage.FromBitmap(bitmap);
        var data = surface.Snapshot().Encode(SKEncodedImageFormat.Jpeg, 80);
        MemoryStream outputStream = new MemoryStream();
        data.SaveTo(outputStream);
        outputStream.Position = 0;
        ViewBag.Error = "Drawing Completed successfully.";
        return File(outputStream, "application/png", "Test.png");
      }
      catch (Exception e)
      {
        ViewBag.Error = e.Message + e.StackTrace.ToString();
        return View();

      }
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
