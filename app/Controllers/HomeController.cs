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
      try
      {
        SKBitmap bitmap = new SKBitmap(100, 100, SKImageInfo.PlatformColorType, SKAlphaType.Premul);
        var surface = SKSurface.Create(bitmap.Info);
        SKCanvas canvas = surface.Canvas;
        canvas.Clear(SKColors.White);
        var paint = new SKPaint();
        paint.IsAntialias = true;
        paint.Color = SKColors.Red;
        paint.StrokeWidth = 3;
        string fontName = SKTypeface.Default.FamilyName;
        canvas.DrawCircle(50, 50, 25, paint);
        var image = SKImage.FromBitmap(bitmap);
        var data = surface.Snapshot().Encode(SKEncodedImageFormat.Jpeg, 80);
        MemoryStream outputStream = new MemoryStream();
        data.SaveTo(outputStream);
        outputStream.Position = 0;
        ViewBag.Error = "Drawing Completed successfully.";
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
