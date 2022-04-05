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
          var data = await DbContext.Transaksis.Where(x => x.IdCustomer == idCustomer).ToListAsync();
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
                                          select new DetailOrderViewModel
                                          {
                                               NamaProduk = b.Nama,
                                               JmlBarang = a.JmlBarang,
                                               SubTotal = a.SubTotal
                                          }).ToArrayAsync()
               });
          }
          return result;
     }

     public async Task<List<TransaksiViewModel>> GetV3(int limit, int offset, int? status = null, DateTime? date = null)
     {

          if (status != null && date != null)
          {
               return await (from a in DbContext.Transaksis
                             join b in DbContext.StatusOrders on a.StatusId equals b.IdSatus
                             join alamat in DbContext.Alamats on a.IdAlamat equals alamat.IdAlamat
                             where a.StatusId == status.Value &&
                             date.Value.Date == a.TglTransaksi.Date
                             select new TransaksiViewModel
                             {
                                  Id = a.NoTransaksi,
                                  Status = b.Nama,
                                  TglTransaksi = a.TglTransaksi,
                                  JmlBayar = a.JmlBayar
                             }).Skip(offset)
                                   .Take(limit).ToListAsync();
          }
          else if (status != null && date == null)
          {
               return await (from a in DbContext.Transaksis
                             join b in DbContext.StatusOrders on a.StatusId equals b.IdSatus
                             join alamat in DbContext.Alamats on a.IdAlamat equals alamat.IdAlamat
                             where a.StatusId == status.Value
                             select new TransaksiViewModel
                             {
                                  Id = a.NoTransaksi,
                                  Status = b.Nama,
                                  TglTransaksi = a.TglTransaksi,
                                  JmlBayar = a.JmlBayar
                             }).Skip(offset)
                                   .Take(limit).ToListAsync();
          }
          else if (status == null && date != null)
          {
               return await (from a in DbContext.Transaksis
                             join b in DbContext.StatusOrders on a.StatusId equals b.IdSatus
                             join alamat in DbContext.Alamats on a.IdAlamat equals alamat.IdAlamat
                             where a.TglTransaksi.Date == date.Value.Date
                             select new TransaksiViewModel
                             {
                                  Id = a.NoTransaksi,
                                  Status = b.Nama,
                                  TglTransaksi = a.TglTransaksi,
                                  JmlBayar = a.JmlBayar
                             }).Skip(offset)
                                   .Take(limit).ToListAsync();
          }
          else
          {
               return await (from a in DbContext.Transaksis
                             join b in DbContext.StatusOrders on a.StatusId equals b.IdSatus
                             join alamat in DbContext.Alamats on a.IdAlamat equals alamat.IdAlamat
                             select new TransaksiViewModel
                             {
                                  Id = a.NoTransaksi,
                                  Status = b.Nama,
                                  TglTransaksi = a.TglTransaksi,
                                  JmlBayar = a.JmlBayar
                             }).Skip(offset)
                                   .Take(limit).ToListAsync();
          }

     }

     public async Task<List<TransaksiViewModel>> GetV2(int limit, int offset, int? status, DateTime? date)
     {
          var selectCondition = (from a in DbContext.Transaksis
                                 join b in DbContext.StatusOrders on a.StatusId equals b.IdSatus
                                 join alamat in DbContext.Alamats on a.IdAlamat equals alamat.IdAlamat
                                 select new TransaksiViewModel
                                 {
                                      Id = a.NoTransaksi,
                                      Status = b.Nama,
                                      TglTransaksi = a.TglTransaksi,
                                      JmlBayar = a.JmlBayar
                                 }).AsQueryable();

          if (status != null)
          {
               selectCondition = selectCondition.Where(x => x.StatusId == status.Value);
          }

          if (date != null)
          {
               selectCondition = selectCondition.Where(x => x.TglTransaksi.Date == date.Value.Date);
          }

          return await selectCondition
          .Skip(offset)
          .Take(limit)
          .ToListAsync();
     }

     public async Task<List<TransaksiViewModel>> GetV1(int limit, int offset, int? status, DateTime? date)
     {
          return await (from a in DbContext.Transaksis
                        join b in DbContext.StatusOrders on a.StatusId equals b.IdSatus
                        join alamat in DbContext.Alamats on a.IdAlamat equals alamat.IdAlamat
                        where status == null ? 1 == 1 : status.Value == a.StatusId
                        && date == null ? 1 == 1 : date.Value.Date == a.TglTransaksi.Date
                        select new TransaksiViewModel
                        {
                             Id = a.NoTransaksi,
                             Status = b.Nama,
                             TglTransaksi = a.TglTransaksi,
                             JmlBayar = a.JmlBayar
                        }).Skip(offset)
                                  .Take(limit).ToListAsync();
     }

     public async Task<TransaksiViewModel> Get(int noTransaksi)
     {
          var transaksi = await DbContext.Transaksis.FirstOrDefaultAsync(x => x.NoTransaksi == noTransaksi);
          var alamat = await DbContext.Alamats.FirstOrDefaultAsync(x => x.IdAlamat == transaksi.IdAlamat);
          var detailOrder = await (from a in DbContext.DetailOrders
                                   join b in DbContext.Produks on a.IdProduk equals b.IdProduk
                                   where a.IdOrder == noTransaksi
                                   select new DetailOrderViewModel
                                   {
                                        NamaProduk = b.Nama,
                                        JmlBarang = a.JmlBarang,
                                        SubTotal = a.SubTotal
                                   }).ToArrayAsync();
          return new TransaksiViewModel
          {
               Id = transaksi.NoTransaksi,
               TglTransaksi = transaksi.TglTransaksi,
               StatusId = transaksi.StatusId,
               Status = (from status in DbContext.StatusOrders where status.IdSatus == transaksi.StatusId select status.Nama).ToString(),
               Alamat = alamat.Detail,
               JmlBayar = transaksi.JmlBayar,
               DetailOrders = detailOrder
          };
     }
}