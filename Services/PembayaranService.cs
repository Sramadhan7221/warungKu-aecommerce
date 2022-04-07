using WarungKuApp.Interfaces;
using WarungKuApp.Datas;
using WarungKuApp.Datas.Entities;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using WarungKuApp.ViewModels;
using System.Linq;

namespace WarungKuApp.Services;
public class PembayaranService : BaseDbService, IPembayaranService
{
     private readonly ITransaksiService _transaksiService;
     public PembayaranService(warungkuContext dbContext, ITransaksiService transaksiService
     ) : base(dbContext)
     {
          _transaksiService = transaksiService;
     }

     public async Task<Pembayaran> Add(Pembayaran newPembayaran)
     {
          await DbContext.AddAsync(newPembayaran);
          await UpdateStatus(newPembayaran.NoTransaksi);
          await DbContext.SaveChangesAsync();

          return newPembayaran;
     }

     public Task<bool> Delete(int id)
     {
          throw new NotImplementedException();
     }

     public Task<List<Pembayaran>> Get(int limit, int offset, string keyword)
     {
          throw new NotImplementedException();
     }

     public Task<Pembayaran?> Get(int id)
     {
          return DbContext.Pembayarans.FirstOrDefaultAsync(x => x.IdPembayaran == id);
     }

     public Task<Pembayaran?> Get(Expression<Func<Pembayaran, bool>> func)
     {
          throw new NotImplementedException();
     }

     public Task<List<Pembayaran>> GetAll()
     {
          throw new NotImplementedException();
     }

     public async Task<PembayaranViewModel> GetByOrderId(int noTrans)
     {
          var data = await DbContext.Pembayarans.FirstOrDefaultAsync(x => x.NoTransaksi == noTrans);
          return new PembayaranViewModel(){
               NoTransaksi = data.NoTransaksi,
               IdPembayaran = data.IdPembayaran,
               IdCustomer = data.IdCustomer,
               TglBayar = data.TglBayar,
               Metode = data.Metode,
               Tujuan = data.Tujuan,
               TotalBayar = data.TotalBayar,
               BuktiBayar = data.BuktiBayar
          };
          // var result = await (from trans in DbContext.Transaksis join 
          //           pembayaran in DbContext.Pembayarans on trans.NoTransaksi equals pembayaran.NoTransaksi where trans.NoTransaksi == noTrans
          //           select new Pembayaran(){
          //                BuktiBayar = pembayaran.BuktiBayar,
          //                IdCustomer = pembayaran.IdCustomer,
          //                IdPembayaran = pembayaran.IdPembayaran,

          //           }).FirstOrDefaultAsync();
     }

     public async Task<Pembayaran> Update(Pembayaran obj)
     {

          var pembayaran = await DbContext.Pembayarans.FirstOrDefaultAsync(x => x.IdPembayaran == obj.IdPembayaran);
          if (pembayaran == null)
          {
               throw new InvalidOperationException("cannot find payment item in database");
          }
          var transaksi = await DbContext.Transaksis.FirstOrDefaultAsync(x => x.NoTransaksi == pembayaran.NoTransaksi);
          if (obj.TotalBayar < transaksi.JmlBayar)
          {
               throw new InvalidOperationException("Pembayaran gagal, jumlah uang kurang dari jumlah tagihan");
          }
          await UpdateStatus(transaksi.NoTransaksi);
          pembayaran.TotalBayar = obj.TotalBayar;
          pembayaran.Tujuan = obj.Tujuan;
          DbContext.Update(pembayaran);
          await DbContext.SaveChangesAsync();

          return pembayaran;
     }

     public async Task<Boolean> UpdateStatus(int noTrans)
     {
          var transaksi = await DbContext.Transaksis.FirstOrDefaultAsync(x => x.NoTransaksi == noTrans);
          if (transaksi == null)
          {
               throw new Exception("Transaksi tidak ditemukan di database");
               return false;
          }
          transaksi.StatusId = 2;
          DbContext.Update(transaksi);
          await DbContext.SaveChangesAsync();
          return true;
     }
}