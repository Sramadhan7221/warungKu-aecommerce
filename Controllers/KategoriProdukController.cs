using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WarungKuApp.Models;
using WarungKuApp.ViewModels;
using WarungKuApp.Datas.Entities;
using Microsoft.EntityFrameworkCore;
using WarungKuApp.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using WarungKuApp.Helpers;

namespace WarungKuApp.Controllers;

public class KategoriProdukController : Controller
{
     private readonly IKategoriService _kategoriService;
     private readonly ILogger<KategoriProdukController> _logger;

     private readonly IWebHostEnvironment _iWebHost;

     public KategoriProdukController(ILogger<KategoriProdukController> logger, IKategoriService kategoriService,IWebHostEnvironment iwebHost)
     {
          _logger = logger;
          _kategoriService = kategoriService;
          _iWebHost = iwebHost;
     }

     public async Task<IActionResult> Index()
     {
          var dbResult = await _kategoriService.GetAll();

          var viewModels = new List<KategoriViewModel>();

          for (int i = 0; i < dbResult.Count; i++)
          {
               viewModels.Add(new KategoriViewModel
               {
                    IdKategoriProduk = dbResult[i].IdKategoriProduk,
                    Nama = dbResult[i].Nama,
                    Deskripsi = dbResult[i].Deskripsi,
                    Icon = dbResult[i].Icon
               });
          }

          return View(viewModels);
     }

     public async Task<IActionResult> Details(int id)
     {
          // if (id == null)
          // {
          //      return NotFound();
          // }

          var data = await _kategoriService.Get(id);
          if (data == null)
          {
               return new NotFoundResult();
          }
          return View(data);
     }

     // GET
     public ActionResult Create()
     {
          return View(new KategoriViewModel());
     }

     // POST
     [HttpPost]
     public async Task<IActionResult> Create(KategoriViewModel request)
     {
          if (!ModelState.IsValid)
          {
               return View(request);
          }
          if (request == null)
          {
               return View(request);
          }

          try
          {
               string fileName = string.Empty;
               if (request.IconFile != null)
               {
                    fileName = $"{request.IconFile?.FileName}";
                    string filePathName = _iWebHost.WebRootPath + "/icons/" + fileName;
                    using (var streamWriter = System.IO.File.Create(filePathName))
                    {
                         //await streamWriter.WriteAsync(Common.StreamToBytes(request.GambarFile.OpenReadStream()));
                         //using extension to convert stream to bytes
                         await streamWriter.WriteAsync(request.IconFile.OpenReadStream().ToBytes());
                    }
               }
               request.Icon = $"icons/{fileName}";

               await _kategoriService.Add(request.ConvertToDbModel());
               return RedirectToAction(nameof(Index));

          }
          catch (InvalidOperationException ex)
          {
               ViewBag.ErrorMessage = ex.Message;
          }
          catch (Exception)
          {
               throw;
          }
          return View(request);
     }

     // GET
     public async Task<IActionResult> Edit(int? id)
     {
          if (id == null)
          {
               return new NotFoundResult();
          }

          var data = await _kategoriService.Get(id.Value);
          if (data == null)
          {
               return NotFound();
          }

          return View(new KategoriViewModel()
          {
               Nama = data.Nama,
               Deskripsi = data.Deskripsi,
               Icon = data.Icon
          });
     }

     // POST
     [HttpPost]
     public async Task<IActionResult> Edit(int? id, KategoriViewModel request)
     {
          if (id == null)
          {
               return BadRequest();
          }
          if (!ModelState.IsValid)
          {
               return View(request);
          }
          try
          {
               request.IdKategoriProduk = id.Value;
               var updated = await _kategoriService.Update(request.ConvertToDbModel());
               return RedirectToAction(nameof(Index));
          }
          catch (InvalidOperationException ex)
          {
               ViewBag.ErrorMessage = ex.Message;
          }
          catch (Exception)
          {
               throw;
          }
          return View(request);

     }

     public async Task<IActionResult> Delete(int id)
     {
          try
          {
               var deleted = await _kategoriService.Delete(id);
               if (deleted)
               {
                    return RedirectToAction(nameof(Index));
               }
          }
          catch (Exception)
          {
               throw;
          }
          return View(new KategoriViewModel());
     }

     [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
     public IActionResult Error()
     {
          return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
     }
}
