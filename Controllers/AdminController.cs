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

public class AdminController : Controller
{
     private readonly IAdminService _adminService;
     private readonly ILogger<AdminController> _logger;

     private readonly IWebHostEnvironment _iWebHost;

     public AdminController(ILogger<AdminController> logger, IAdminService kategoriService, IWebHostEnvironment iwebHost)
     {
          _logger = logger;
          _adminService = kategoriService;
          _iWebHost = iwebHost;
     }

     public async Task<IActionResult> Index()
     {
          var dbResult = await _adminService.GetAll();

          var viewModels = new List<AdminViewModel>();

          for (int i = 0; i < dbResult.Count; i++)
          {
               viewModels.Add(new AdminViewModel
               {
                    IdAdmin = dbResult[i].IdAdmin,
                    Nama = dbResult[i].Nama,
                    NoHp = dbResult[i].NoHp,
                    Username = dbResult[i].Username,
                    Password = dbResult[i].Password
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

          var data = await _adminService.Get(id);
          if (data == null)
          {
               return new NotFoundResult();
          }
          return View(data);
     }

     // GET
     public ActionResult Create()
     {
          return View(new AdminViewModel());
     }

     // POST
     [HttpPost]
     public async Task<IActionResult> Create(AdminViewModel request)
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

               await _adminService.Add(request.ConvertToDbModel());
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

          var data = await _adminService.Get(id.Value);
          if (data == null)
          {
               return NotFound();
          }

          return View(new AdminViewModel()
          {
               Nama = data.Nama,
               NoHp = data.NoHp,
               Username = data.Username
          });
     }

     // POST
     [HttpPost]
     public async Task<IActionResult> Edit(int? id, AdminViewModel request)
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
               request.IdAdmin = id.Value;
               var updated = await _adminService.Update(request.ConvertToDbModel());
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
               var deleted = await _adminService.Delete(id);
               if (deleted)
               {
                    return RedirectToAction(nameof(Index));
               }
          }
          catch (Exception)
          {
               throw;
          }
          return View(new AdminViewModel());
     }

     [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
     public IActionResult Error()
     {
          return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
     }
}
