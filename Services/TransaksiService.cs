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
         var pengiriman = await DbContext.Pengirimen.FirstOrDefaultAsync(x => x.NoTransaksi == item.NoTransaksi);
         result.Add(new TransaksiViewModel
         {
            Id = item.NoTransaksi,
            TglTransaksi = item.TglTransaksi,
            Alamat = Alamat.Detail,
            JmlBayar = item.JmlBayar,
            Status = Status.Nama,
            StatusId = item.StatusId,
            Ongkir = (pengiriman == null) ? 0m : pengiriman.Ongkir,
            Kurir = (pengiriman == null) ? string.Empty : pengiriman.Kurir,
            DetailOrders = await (from a in DbContext.DetailOrders
                                  join b in DbContext.Produks on a.IdProduk equals b.IdProduk
                                  where a.IdOrder == item.NoTransaksi
                                  select new DetailOrderViewModel
                                  {
                                     NamaProduk = b.Nama,
                                     JmlBarang = a.JmlBarang,
                                     SubTotal = a.SubTotal,
                                     Harga = b.Harga,
                                     Image = b.Gambar
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
      var user = await DbContext.Customers.FirstOrDefaultAsync(x => x.IdCustomer == transaksi.IdCustomer);
      var pengiriman = await DbContext.Pengirimen.FirstOrDefaultAsync(x => x.NoTransaksi == transaksi.NoTransaksi);
      var pembayaran = await DbContext.Pembayarans.FirstOrDefaultAsync(x => x.NoTransaksi == transaksi.NoTransaksi);
      var detailOrder = await (from a in DbContext.DetailOrders
                               join b in DbContext.Produks on a.IdProduk equals b.IdProduk
                               where a.IdOrder == noTransaksi
                               select new DetailOrderViewModel
                               {
                                  IdProduk = b.IdProduk,
                                  NamaProduk = b.Nama,
                                  JmlBarang = a.JmlBarang,
                                  SubTotal = a.SubTotal,
                                  Harga = b.Harga,
                                  Image = b.Gambar

                               }).ToArrayAsync();
      var status = await DbContext.StatusOrders.FirstOrDefaultAsync(x => x.IdSatus == transaksi.StatusId);
      return new DetailTransaksiViewModel()
      {
         Id = noTransaksi,
         TglTransaksi = transaksi.TglTransaksi,
         StatusId = transaksi.StatusId,
         Status = status.Nama,
         Alamat = alamat.Detail,
         JmlBayar = transaksi.JmlBayar,
         DetailOrders = detailOrder,
         IdAlamat = transaksi.IdAlamat,
         NamaCustomer = user.Nama,
         Nohp = user.NoHp,
         NoResi = (pengiriman == null) ? string.Empty : pengiriman.NoResi,
         Ongkir = (pengiriman == null) ? 0m : pengiriman.Ongkir,
         Kurir = (pengiriman == null) ? string.Empty : pengiriman.Kurir,
         Metode = (pembayaran == null) ? string.Empty : pembayaran.Metode,
         TotalBayar = (pembayaran == null) ? 0m : pembayaran.TotalBayar
      };
   }

   public async Task<bool> updatesStatus(UpdateStatusTransaksiViewModel? req)
   {
      var transaksi = await DbContext.Transaksis.FirstOrDefaultAsync(x => x.NoTransaksi == req.NoTransaksi);
      if (transaksi == null)
      {
         return false;
      }
      transaksi.StatusId = req.StatusId;
      DbContext.Update(transaksi);
      await DbContext.SaveChangesAsync();

      return true;
   }
   public async Task<bool> KirimPesanan(DetailTransaksiViewModel? req)
   {
      var transaksi = await DbContext.Transaksis.FirstOrDefaultAsync(x => x.NoTransaksi == req.Id);
      var pengiriman = await DbContext.Pengirimen.FirstOrDefaultAsync(x => x.NoTransaksi == transaksi.NoTransaksi);
      if (transaksi == null)
      {
         return false;
      }
      transaksi.StatusId = req.StatusId;
      pengiriman.Notes = req.Notes;
      pengiriman.NoResi = req.NoResi;
      DbContext.Update(transaksi);
      DbContext.Update(pengiriman);
      await DbContext.SaveChangesAsync();

      return true;
   }

   public async Task<List<TransaksiViewModel>> GetAll()
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
                    }).ToListAsync();
   }
}