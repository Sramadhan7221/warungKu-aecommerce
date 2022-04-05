using WarungKuApp.Interfaces;
using WarungKuApp.Datas;
using WarungKuApp.Datas.Entities;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using WarungKuApp.ViewModels;
using System.Linq;

namespace WarungKuApp.Services;
public class DetailOrderService : BaseDbService, IDetailOrderService
{
     private readonly IProdukService _produkService;
     private readonly ITransaksiService _orderService;
     public DetailOrderService(warungkuContext dbContext, IProdukService produkService, ITransaksiService orderService
     ) : base(dbContext)
     {
          _produkService = produkService;
          _orderService = orderService;
     }
     async Task<List<DetailOrderViewModel>> IDetailOrderService.Get(int idCustomer)
     {
          var result = await (from a in DbContext.DetailOrders
                              join b in DbContext.Produks on a.IdProduk equals b.IdProduk
                              join c in DbContext.Transaksis on a.IdOrder equals c.NoTransaksi
                              where c.IdCustomer == idCustomer
                              select new DetailOrderViewModel
                              {
                                   IdOrder = a.IdOrder,
                                   IdCustomer = c.IdCustomer,
                                   IdProduk = a.IdProduk,
                                   Image = b.Gambar,
                                   JmlBarang = a.JmlBarang,
                                   SubTotal = a.SubTotal,
                                   NamaProduk = b.Nama,
                                   Harga = b.Harga,
                                   tglTransaksi = c.TglTransaksi
                              }).ToListAsync();

          return result;
     }
}