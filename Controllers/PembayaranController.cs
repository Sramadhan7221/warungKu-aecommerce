using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WarungKuApp.Models;
using WarungKuApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using WarungKuApp.Interfaces;
using WarungKuApp.Helpers;

namespace WarungKuApp.Controllers;
[Authorize]
public class PembayaranController : BaseController
{
     private readonly IPembayaranService _pembayaranService;
     private readonly ITransaksiService _transaksiService;
     private readonly IPengirimanService _pengirimanService;
     private readonly ILogger<PembayaranController> _logger;

     private readonly IWebHostEnvironment _iWebHost;

     public PembayaranController(ILogger<PembayaranController> logger, IPembayaranService PembayaranService, IWebHostEnvironment iwebHost, ITransaksiService transaksiService, IPengirimanService pengirimanService)
     {
          _logger = logger;
          _pembayaranService = PembayaranService;
          _transaksiService = transaksiService;
          _pengirimanService = pengirimanService;
          _iWebHost = iwebHost;
     }

     [Authorize(Roles = AppConstant.CUSTOMER)]
     public async Task<IActionResult> Index(PembayaranViewModel? request)
     {
          if (request == null)
          {
               return BadRequest();
          }
          var transaksi = await _transaksiService.Get(request.NoTransaksi);
          var pengiriman = await _pengirimanService.Get(request.NoTransaksi);
          return View(new PembayaranViewModel
          {
               NoTransaksi = request.NoTransaksi,
               tagihan = transaksi.JmlBayar + pengiriman.Ongkir
          });
     }

     [HttpPost]
     public async Task<IActionResult> Create(PembayaranViewModel request)
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
                    string filePathName = _iWebHost.WebRootPath + "/bukti/" + fileName;
                    using (var streamWriter = System.IO.File.Create(filePathName))
                    {
                         //await streamWriter.WriteAsync(Common.StreamToBytes(request.GambarFile.OpenReadStream()));
                         //using extension to convert stream to bytes
                         await streamWriter.WriteAsync(request.GambarFile.OpenReadStream().ToBytes());
                    }
               }
               request.TglBayar = DateTime.Now;
               request.BuktiBayar = $"bukti/{fileName}";
               var pembayaran = request.ConvertToDbModel();


               await _pembayaranService.Add(pembayaran);
               return RedirectToAction("MyOrder", "Transaksi");

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

     [HttpPost]
     public async Task<IActionResult> Details(int noTransaksi)
     {
          try
          {
               var data = await _pembayaranService.GetByOrderId(noTransaksi);
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
