using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WarungKuApp.Models;
using WarungKuApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using WarungKuApp.Interfaces;
using WarungKuApp.Helpers;
using System.Security.Claims;

namespace WarungKuApp.Controllers;
[Authorize]
public class UlasanController : BaseController
{
   private readonly ITransaksiService _transaksiService;
   private readonly IUlasanService _ulasanService;
   private readonly IProdukService _produkService;
   private readonly ILogger<UlasanController> _logger;
   private readonly IWebHostEnvironment _iWebHost;

   public UlasanController(ILogger<UlasanController> logger, IWebHostEnvironment iwebHost, ITransaksiService transaksiService, IUlasanService ulasanService, IProdukService produkService)
   {
      _logger = logger;
      _transaksiService = transaksiService;
      _ulasanService = ulasanService;
      _produkService = produkService;
      _iWebHost = iwebHost;
   }

   [Authorize(Roles = AppConstant.CUSTOMER)]
   public async Task<IActionResult> Index(int? idProduk,int? idTransaksi)
   {
      if (idProduk == null)
      {
         return View();
      }
      var produk = await _produkService.Get(idProduk.Value);
      return View(new UlasanViewModel
      {
         NamaProduk = produk.Nama,
         IdProduk = produk.IdProduk,
         GambarProduk = produk.Gambar
      });
   }

   [HttpPost]
   public async Task<IActionResult> BeriUlasan(UlasanViewModel request)
   {
      if (!ModelState.IsValid)
      {
         return BadRequest();
      }
      if (request == null)
      {
         return BadRequest();
      }

      try
      {
         string fileName = string.Empty;
         if (request.GambarFile != null)
         {
            fileName = $"{Guid.NewGuid()}-{request.GambarFile?.FileName}";
            string filePathName = _iWebHost.WebRootPath + "/review/" + fileName;
            using (var streamWriter = System.IO.File.Create(filePathName))
            {
               //await streamWriter.WriteAsync(Common.StreamToBytes(request.GambarFile.OpenReadStream()));
               //using extension to convert stream to bytes
               await streamWriter.WriteAsync(request.GambarFile.OpenReadStream().ToBytes());
            }
            request.Gambar = $"review/{fileName}";
         }
         request.IdCustomer = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value.ToInt();
         var ulasan = request.ConvertToDb();
         bool Update = await _ulasanService.isExist(request.IdCustomer, request.IdProduk);
         await _ulasanService.Add(ulasan);
         return RedirectToAction(nameof(UlasanSaya));
      }
      catch (InvalidOperationException ex)
      {
         ViewBag.ErrorMessage = ex.Message;
      }
      catch (Exception)
      {
         throw;
      }
      return View();
   }

   public async Task<IActionResult> UlasanSaya()
   {
      try
      {
         var IdCustomer = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value.ToInt();
         var data = await _ulasanService.UserUlasan(IdCustomer);
         if (data == null)
         {
            return new NotFoundResult();
         }
         return View(data);
      }
      catch (InvalidOperationException ex)
      {
         return Json(new
         {
            success = false,
            message = ex.Message
         });
      }
      catch
      {
         throw;
      }
   }
   [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
   public IActionResult Error()
   {
      return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
   }
}
