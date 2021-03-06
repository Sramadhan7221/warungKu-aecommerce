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
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WarungKuApp.Controllers;

[Authorize]
public class TransaksiController : BaseController
{
   private readonly ILogger<TransaksiController> _logger;
   private readonly IKeranjangService _keranjangService;
   private readonly ITransaksiService _orderService;
   private readonly IDetailOrderService _detailOrderService;
   private readonly IStatusService _statusService;
   private readonly IPengirimanService _pengirimanService;

   public TransaksiController(ILogger<TransaksiController> logger, IKeranjangService keranjangService,
   ITransaksiService orderService, IDetailOrderService detailOrderService, IStatusService statusService, IPengirimanService pengirimanService)
   {
      _logger = logger;
      _keranjangService = keranjangService;
      _orderService = orderService;
      _detailOrderService = detailOrderService;
      _statusService = statusService;
      _pengirimanService = pengirimanService;
   }

   [Authorize(Roles = AppConstant.ADMIN)]

   public async Task<IActionResult> index(int? page, int? pageCount)
   {
      var tuplePagination = Common.ToLimitOffset(page, pageCount);
      var result = await _orderService.GetV3(tuplePagination.Item1, tuplePagination.Item2);

      await SetStatusListAsSelectListItem();
      ViewBag.FilterDate = null;

      return View(result);
   }

   [HttpPost]
   public async Task<IActionResult> Index([FromQuery] int? page, [FromQuery] int? pageCount, int? status, DateTime? date)
   {
      var tuplePagination = Common.ToLimitOffset(page, pageCount);

      var result = await _orderService.GetV3(tuplePagination.Item1, tuplePagination.Item2, status, date);

      await SetStatusListAsSelectListItem(status);
      if (date != null)
      {
         ViewBag.FilterDate = date.Value.ToString("MM/dd/yyyy");
      }

      return View(result);
   }

   private async Task SetStatusListAsSelectListItem(int? status = null)
   {
      var statusList = await _statusService.Get();

      if (statusList == null || !statusList.Any())
      {
         ViewBag.StatusList = new List<SelectListItem>();
      }
      else
      {
         ViewBag.StatusList = statusList.Select(x => new SelectListItem
         {
            Value = x.IdSatus.ToString(),
            Text = x.Nama,
            Selected = status != null && status.Value == x.IdSatus
         }).ToList();
      }
   }

   [Authorize(Roles = AppConstant.CUSTOMER)]
   public async Task<IActionResult> MyOrder()
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
         return RedirectToAction(nameof(Index), "Keranjang");
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

   public async Task<IActionResult> Detail(int noTrans)
   {
      var result = await _orderService.Get(noTrans);
      return View(result);
   }

   [HttpPost]
   public async Task<IActionResult> SetEkspedisi(PengirimanViewModel? request)
   {
      if (request == null)
      {
         return Json(new
         {
            success = false,
            message = "bad request"
         });
      }
      if (!ModelState.IsValid)
      {
         return Json(new
         {
            success = false,
            message = "bad request"
         });
      }
      try
      {
         var transaksi = await _orderService.Get(request.NoTransaksi);
         request.IdAlamat = transaksi.IdAlamat;
         if (await _pengirimanService.isExist(request.NoTransaksi))
         {
            await _pengirimanService.Update(request.convertToDbModel());
         }
         else
         {

            await _pengirimanService.Add(request.convertToDbModel());
         }

         return Json(new
         {
            success = true,
            message = "Berhasil"
         });
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

   public async Task<IActionResult> TerimaPesanan(UpdateStatusTransaksiViewModel? request)
   {
      if (!ModelState.IsValid)
      {
         return Json(new
         {
            success = false,
            message = "Bad Request"
         });
      }
      if (request == null)
      {
         return Json(new
         {
            success = false,
            message = "Bad Request"
         });
      }
      try
      {
         var update = await _orderService.updatesStatus(request);
         if (!update)
         {
            return Json(new
            {
               success = false,
               message = "Bad Request"
            });
         }
         return Json(new
         {
            success = true,
            message = "Berhasil"
         });
      }
      catch (System.Exception)
      {

         throw;
      }
   }

   [Authorize(Roles = AppConstant.ADMIN)]
   [HttpPost]
   public async Task<IActionResult> ManagePesanan(DetailTransaksiViewModel? request)
   {
      if (!ModelState.IsValid)
      {
         return Json(new
         {
            success = false,
            message = "Bad Request"
         });
      }
      if (request == null)
      {
         return Json(new
         {
            success = false,
            message = "Bad Request"
         });
      }
      try
      {
         var update = await _orderService.KirimPesanan(request);
         if (!update)
         {
            return Json(new
            {
               success = false,
               message = "Bad Request"
            });
         }
         return RedirectToAction(nameof(Index));
      }
      catch (System.Exception)
      {

         throw;
      }
   }
   public async Task<IActionResult> Datatable(object datatable)
   {
      var result = await _orderService.GetV3(int.Parse(HttpContext.Request.Query["length"]), int.Parse(HttpContext.Request.Query["start"]));

      var totalData = await _orderService.GetAll();
      int totalDataCount = totalData.Count();

      var viewModels = new List<TransaksiViewModel>();

      for (int i = 0; i < result.Count; i++)
      {
         viewModels.Add(new TransaksiViewModel
         {
            Id = result[i].Id,
            Status = result[i].Status,
            TglTransaksi = result[i].TglTransaksi,
            JmlBayar = result[i].JmlBayar
         });
      }

      return Json(new
      {
         recordsTotal = totalDataCount,
         recordsFiltered = totalDataCount,
         data = viewModels
      });
   }
   public async Task<IActionResult> ManagePengiriman(int id)
   {
      var result = await _orderService.Get(id);
      return View(result);
   }
}