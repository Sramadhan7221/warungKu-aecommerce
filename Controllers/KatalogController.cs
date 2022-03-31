using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WarungKuApp.Models;
using WarungKuApp.ViewModels;
using Microsoft.EntityFrameworkCore;
using WarungKuApp.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using WarungKuApp.Helpers;
using Microsoft.AspNetCore.Authorization;

namespace WarungKuApp.Controllers;

public class KatalogController : Controller
{
     private readonly IProdukService _produkService;
     private readonly IKategoriService _kategoriService;
     private readonly IProdKategoriService _prodKategoriServices;
     private readonly ILogger<ProdukController> _logger;

     public KatalogController(ILogger<ProdukController> logger, IProdukService produkService, IKategoriService kategoriService, IProdKategoriService prodKategoriService)
     {
          _logger = logger;
          _produkService = produkService;
          _kategoriService = kategoriService;
          _prodKategoriServices = prodKategoriService;
     }

     public async Task<IActionResult> Index()
     {
          var dbResult = await _produkService.GetAll();

          var viewModel = new List<KatalogViewModel>();

          for (int i = 0; i < dbResult.Count; i++)
          {
               viewModel.Add(new KatalogViewModel
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

          return View(viewModel);
     }

}