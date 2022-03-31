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
[Authorize]
public class ProdukController : Controller
{
     private readonly IProdukService _produkService;
     private readonly IKategoriService _kategoriService;
     private readonly IProdKategoriService _prodKategoriServices;
     private readonly IWebHostEnvironment _iWebHost;
     private readonly ILogger<ProdukController> _logger;

     public ProdukController(ILogger<ProdukController> logger, IProdukService produkService, IKategoriService kategoriService, IWebHostEnvironment iwebHost, IProdKategoriService prodKategoriService)
     {
          _logger = logger;
          _produkService = produkService;
          _kategoriService = kategoriService;
          _iWebHost = iwebHost;
          _prodKategoriServices = prodKategoriService;
     }

     public async Task<IActionResult> Index()
     {
          var dbResult = await _produkService.GetAll();

          var viewModel = new List<ProdukViewModel>();

          for (int i = 0; i < dbResult.Count; i++)
          {
               viewModel.Add(new ProdukViewModel
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

     //Transfer data list of kategori ke view dimasukan dalam selectlistitem
     private async Task SetKategoriDataSource()
     {
          var kategoriViewModels = await _kategoriService.GetAll();

          ViewBag.KategoriDataSource = kategoriViewModels.Select(x => new SelectListItem
          {
               Value = x.IdKategoriProduk.ToString(),
               Text = x.Nama,
               Selected = false
          }).ToList();
     }

     private async Task SetKategoriDataSource(int[] kategoris)
     {

          if (kategoris == null)
          {
               await SetKategoriDataSource();
               return;
          }
          var kategoriViewModels = await _kategoriService.GetAll();
          ViewBag.KategoriDataSource = kategoriViewModels
               .Select(x => new SelectListItem
               {
                    Value = x.IdKategoriProduk.ToString(),
                    Text = x.Nama,
                    Selected = kategoris.FirstOrDefault(y => y == x.IdKategoriProduk) == 0 ? false : true
               }).ToList();
     }
     public async Task<IActionResult> Create()
     {
          await SetKategoriDataSource();
          return View(new ProdukReqViewModel());
     }

     [HttpPost]
     public async Task<IActionResult> Create(ProdukReqViewModel request)
     {
          if (!ModelState.IsValid)
          {
               await SetKategoriDataSource();
               return View(request);
          }
          if (request == null)
          {
               await SetKategoriDataSource();
               return View(request);
          }
          try
          {
               string fileName = string.Empty;

               if (request.GambarFile != null)
               {
                    fileName = $"{Guid.NewGuid()}-{request.GambarFile?.FileName}";

                    string filePathName = _iWebHost.WebRootPath + "/images/" + fileName;

                    using (var streamWriter = System.IO.File.Create(filePathName))
                    {
                         //await streamWriter.WriteAsync(Common.StreamToBytes(request.GambarFile.OpenReadStream()));
                         //using extension to convert stream to bytes
                         await streamWriter.WriteAsync(request.GambarFile.OpenReadStream().ToBytes());
                    }
                    request.Gambar = $"images/{fileName}";
               }
               var produk = request.ConvertToDbModel();

               for (int i = 0; i < request.IdKategoriProduk.Length; i++)
               {
                    // insert to ProdKategori table
                    produk.ProdKategoris.Add(new Datas.Entities.ProdKategori
                    {
                         IdKategoriProduk = request.IdKategoriProduk[i],
                         IdProduk = produk.IdProduk
                    });

               }

               await _produkService.Add(produk);
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

          await SetKategoriDataSource();
          return View(request);
     }

     public async Task<IActionResult> Details(int id)
     {
          var data = await _produkService.Get(id);
          if (data == null)
          {
               throw new NullReferenceException();
          }
          return View(new ProdukReqViewModel()
          {
               IdProduk = data.IdProduk,
               Deskripsi = data.Deskripsi,
               Harga = data.Harga,
               Stok = data.Stok,
               Gambar = data.Gambar
          });
     }

     // GET
     public async Task<IActionResult> Edit(int? id)
     {
          if (id == null)
          {
               return new NotFoundResult();
          }
          var data = await _produkService.Get(id.Value);
          if (data == null)
          {
               return NotFound();
          }
          var kategoriIds = await _prodKategoriServices.GetKategoriIds(data.IdProduk);
          await SetKategoriDataSource(kategoriIds);

          return View(new ProdukReqViewModel()
          {
               IdProduk = data.IdProduk,
               Nama = data.Nama,
               Deskripsi = data.Deskripsi,
               Harga = data.Harga,
               Stok = data.Stok,
               Gambar = data.Gambar,
               IdKategoriProduk = kategoriIds
          });
     }

     // POST
     [HttpPost]
     public async Task<IActionResult> Edit(int id, ProdukReqViewModel request)
     {
          if (!ModelState.IsValid)
          {

               await SetKategoriDataSource(request.IdKategoriProduk);
               return View(request);
          }

          if (request == null)
          {
               await SetKategoriDataSource();
               return View(request);
          }

          try
          {
               string fileName = string.Empty;

               if (request.GambarFile != null)
               {
                    fileName = $"{Guid.NewGuid()}-{request.GambarFile?.FileName}";

                    string filePathName = _iWebHost.WebRootPath + "\\images\\" + fileName;

                    using (var streamWriter = System.IO.File.Create(filePathName))
                    {
                         //await streamWriter.WriteAsync(Common.StreamToBytes(request.GambarFile.OpenReadStream()));
                         //using extension to convert stream to bytes
                         await streamWriter.WriteAsync(request.GambarFile.OpenReadStream().ToBytes());
                    }
               }

               var product = request.ConvertToDbModel();
               if (request.GambarFile != null)
               {
                    product.Gambar = $"images/{fileName}";
               }

               //Update ProdukKategori
               var productKategories = await _prodKategoriServices.GetKategoriIds(id);

               for (int i = 0; i < request.IdKategoriProduk.Length; i++)
               {
                    if (productKategories != null && productKategories.Any())
                    {
                         if (!productKategories.Any(x => x == request.IdKategoriProduk[i]))
                         {
                              product.ProdKategoris.Add(new Datas.Entities.ProdKategori
                              {
                                   IdKategoriProduk = request.IdKategoriProduk[i],
                                   IdProduk = id
                              });
                         }
                    }
                    else
                    {
                         product.ProdKategoris.Add(new Datas.Entities.ProdKategori
                         {
                              IdKategoriProduk = request.IdKategoriProduk[i],
                              IdProduk = id
                         });
                    }
               }

               //Remove kategori from product
               if (productKategories != null && (product.ProdKategoris != null && product.ProdKategoris.Any()))
               {
                    foreach (var item in productKategories)
                    {
                         if (!product.ProdKategoris.Any(x => x.IdKategoriProduk == item))
                         {
                              await _prodKategoriServices.Remove(request.IdProduk, item);
                         }
                    }
               }

               await _produkService.Update(product);

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

          await SetKategoriDataSource(request.IdKategoriProduk);
          return View(request);
     }

     public async Task<IActionResult> Delete(int id)
     {
          try
          {
               var deleted = await _produkService.Delete(id);
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
