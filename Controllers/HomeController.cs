using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WarungKuApp.Models;
using WarungKuApp.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Authorization;
using WarungKuApp.Interfaces;
using WarungKuApp.Helpers;

namespace WarungKuApp.Controllers;

public class HomeController : BaseController
{
     private readonly ILogger<HomeController> _logger;
     private readonly IProdukService _produkService;

     public HomeController(ILogger<HomeController> logger, IProdukService produkService)
     {
          _logger = logger;
          _produkService = produkService;
     }

     public async Task<IActionResult> Index(int? page, int? pageCount)
     {
          var viewModels = new List<ProdukViewModel>();
          var tuplePagination = Common.ToLimitOffset(page, pageCount);
          var dbResult = await _produkService.Get(tuplePagination.Item1, tuplePagination.Item2, string.Empty);

          if (dbResult == null || !dbResult.Any())
          {
               return RedirectToAction(nameof(Index), new
               {
                    page = page > 1 ? page - 1 : 1,
                    pageCount = pageCount
               });
          }

          for (int i = 0; i < dbResult.Count; i++)
          {
               viewModels.Add(new ProdukViewModel
               {
                    IdProduk = dbResult[i].IdProduk,
                    Nama = dbResult[i].Nama,
                    Gambar = dbResult[i].Gambar,
                    Harga = dbResult[i].Harga,
                    Kategories = dbResult[i].ProdKategoris.Select(x => new KategoriViewModel
                    {
                         IdKategoriProduk = x.IdKategoriProduk,
                         Nama = x.IdKategoriProdukNavigation.Nama,
                         Icon = x.IdKategoriProdukNavigation.Icon
                    }).ToList()
               });
          }
          ViewBag.halamanSekarang = page ?? 1;
          ViewBag.halamanTotal = dbResult.Count() == 0 ? 1 : dbResult.Count();

          return View(viewModels);
     }

     public async Task<IActionResult> Details(int? id)
     {
          if (id == null)
          {
               return NotFound();
          }

          var produk = await _produkService.Get(id.Value);

          if (produk == null)
          {
               return NotFound();
          }

          return View(new KatalogViewModel()
          {
               IdProduk = produk.IdProduk,
               Nama = produk.Nama,
               Deskripsi = produk.Deskripsi,
               Harga = produk.Harga,
               Gambar = produk.Gambar,
               Stok = 100,
               Terjual = 10
          });
     }
     public IActionResult Privacy()
     {
          return View();
     }

     public IActionResult Denied()
     {
          return View();
     }

     [AllowAnonymous]
     [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
     public IActionResult Error()
     {
          return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
     }
}