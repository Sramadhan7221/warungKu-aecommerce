using WarungKuApp.Interfaces;
using WarungKuApp.Datas;
using WarungKuApp.Datas.Entities;
using WarungKuApp.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace WarungKuApp.Services;
public class TransaksiService : BaseDbService, ITransaksiService
{
     public TransaksiService(warungkuContext dbContext) : base(dbContext)
     {
     }

     public async Task<Transaksi> Checkout(Transaksi newTransaksi)
     {
          await DbContext.AddAsync(newTransaksi);
          await DbContext.SaveChangesAsync();

          return newTransaksi;
     }

     public async Task<List<TransaksiViewModel>> Riwayat(int idCustomer)
     {
          var data = await DbContext.Transaksis.ToListAsync();
          var result = new List<TransaksiViewModel>();
          foreach (var item in data)
          {
               var Alamat = await DbContext.Alamats.FirstOrDefaultAsync(x => x.IdAlamat == item.IdAlamat);
               var Status = await DbContext.StatusOrders.FirstOrDefaultAsync(x => x.IdSatus == item.StatusId);
               result.Add(new TransaksiViewModel
               {
                    TglTransaksi = item.TglTransaksi,
                    Alamat = Alamat.Detail,
                    JmlBayar = item.JmlBayar,
                    Status = Status.Nama,
                    DetailOrders = await (from a in DbContext.DetailOrders
                    join b in DbContext.Produks on a.IdProduk equals b.IdProduk
                    where a.IdOrder == item.NoTransaksi
                    select new DetailOrderViewModel{
                         NamaProduk = b.Nama,
                         JmlBarang = a.JmlBarang,
                         SubTotal = a.SubTotal
                    }).ToArrayAsync()
               });
          }
          return result;
     }
}