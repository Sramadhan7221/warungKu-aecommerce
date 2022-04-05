using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WarungKuApp.Models;
using WarungKuApp.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Authorization;
using WarungKuApp.Interfaces;
using System.Security.Claims;
using WarungKuApp.Helpers;
using WarungKuApp.Datas.Entities;
using System.Globalization;

namespace WarungKuApp.Controllers;

[Authorize(Roles = AppConstant.CUSTOMER)]
public class TransaksiController : Controller
{
     private readonly ILogger<TransaksiController> _logger;
     private readonly IKeranjangService _keranjangService;
     private readonly ITransaksiService _orderService;
     private readonly IDetailOrderService _detailOrderService;

     public TransaksiController(ILogger<TransaksiController> logger, IKeranjangService keranjangService,
     ITransaksiService orderService, IDetailOrderService detailOrderService)
     {
          _logger = logger;
          _keranjangService = keranjangService;
          _orderService = orderService;
          _detailOrderService = detailOrderService;
     }

     public override void OnActionExecuted(ActionExecutedContext context)
     {
          if (HttpContext.User == null || HttpContext.User.Identity == null)
          {
               ViewBag.IsLogged = false;
          }
          else
          {
               ViewBag.IsLogged = HttpContext.User.Identity.IsAuthenticated;
          }

          base.OnActionExecuted(context);
     }

     public async Task<IActionResult> Index()
     {

          var result = await _orderService.Riwayat(HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value.ToInt());

          ViewBag.result = result;
          return View();
     }

     [HttpPost]
     [ValidateAntiForgeryToken]
     public async Task<IActionResult> Checkout(CheckoutViewModel? request)
     {
          if (request == null)
          {
               return BadRequest();
          }

          if (request.Alamat == 0)
          {
               return BadRequest();
          }

          if (request.Action == "Simpan")
          {
               for (int i = 0; i < request.Id.Length; i++)
               {
                    await _keranjangService.Update(new Datas.Entities.Keranjang
                    {
                         IdKeranjang = request.Id[i],
                         JmlBarang = request.Qty[i],
                         IdCustomer = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value.ToInt(),
                         SubTotal = decimal.Parse(request.harga[i])
                    });
               }
               return RedirectToAction(nameof(Index),"Keranjang");
          }

          int idCustomer = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value.ToInt();
          var result = await _keranjangService.Get(idCustomer);

          if (result == null || !result.Any())
          {
               return BadRequest();
          }

          foreach (var item in result)
          {
               int keranjangId = request.Id.FirstOrDefault(x => item.IdKeranjang == x);

               if (keranjangId < 1)
               {
                    continue;
               }
               int jumlahBarangBaru = request.Qty[Array.IndexOf(request.Id, keranjangId)];

               item.JmlBarang = jumlahBarangBaru;
               item.Subtotal = item.harga * jumlahBarangBaru;
          }

          var newOrder = new Transaksi();

          newOrder.IdCustomer = idCustomer;
          newOrder.JmlBayar = result.Sum(x => x.Subtotal);
          newOrder.Notes = request.Note;
          newOrder.StatusId = 1;
          newOrder.IdAlamat = request.Alamat;
          newOrder.TglTransaksi = DateTime.Now;
          newOrder.DetailOrders = new List<DetailOrder>();

          foreach (var item in result)
          {
               newOrder.DetailOrders.Add(new DetailOrder
               {
                    IdOrder = newOrder.NoTransaksi,
                    Harga = item.harga,
                    JmlBarang = item.JmlBarang,
                    SubTotal = item.Subtotal,
                    IdProduk = item.IdProduk
               });
          }

          await _orderService.Checkout(newOrder);

          await _keranjangService.Clear(idCustomer);

          return RedirectToAction(nameof(CheckoutBerhasil));
     }


     public async Task<IActionResult> CheckoutBerhasil()
     {
          var result = await _detailOrderService.Get(HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value.ToInt());
          return View(result);
     }
}