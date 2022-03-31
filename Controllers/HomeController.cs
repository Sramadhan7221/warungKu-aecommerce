using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WarungKuApp.Models;
using WarungKuApp.ViewModels;
using WarungKuApp.Datas;
using Microsoft.EntityFrameworkCore;

namespace WarungKuApp.Controllers;

public class HomeController : Controller
{
     private readonly ILogger<HomeController> _logger;
     private readonly warungkuContext _dbContext;

     public HomeController(ILogger<HomeController> logger, WarungKuApp.Datas.warungkuContext dbContext)
     {
          _logger = logger;
          _dbContext = dbContext;
     }

     public async Task<IActionResult> Index()
     {
          var dbResult = await _dbContext.KategoriProduks.Select(x => new ViewModels.KategoriViewModel
          {
               Nama = x.Nama,
               Deskripsi = x.Deskripsi
          }).ToListAsync();

          return View(dbResult);
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
