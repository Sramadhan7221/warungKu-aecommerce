using WarungKuApp.Interfaces;
using WarungKuApp.Datas;
using WarungKuApp.Datas.Entities;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using WarungKuApp.ViewModels;

namespace WarungKuApp.Services;
public class UlasanService : BaseDbService, IUlasanService
{
   public UlasanService(warungkuContext dbContext) : base(dbContext)
   {
   }

   public async Task<Ulasan> Add(Ulasan obj)
   {
      await DbContext.Ulasans.AddAsync(obj);
      await DbContext.SaveChangesAsync();

      return obj;
   }

   public Task<bool> Delete(int id)
   {
      throw new NotImplementedException();
   }

   public async Task<List<StatusOrder>> Get()
   {
      return await DbContext.StatusOrders.ToListAsync();
   }

   public Task<List<Ulasan>> Get(int limit, int offset, string keyword)
   {
      throw new NotImplementedException();
   }

   public Task<Ulasan?> Get(int id)
   {
      throw new NotImplementedException();
   }

   public Task<Ulasan?> Get(Expression<Func<Ulasan, bool>> func)
   {
      throw new NotImplementedException();
   }

   public Task<List<Ulasan>> GetAll()
   {
      throw new NotImplementedException();
   }

   public async Task<Ulasan>UpdateNew(UlasanViewModel obj)
   {
      var ulasan = await DbContext.Ulasans.FirstOrDefaultAsync(x => x.IdUlasan == obj.IdUlasan);
      if (ulasan == null)
      {
         throw new InvalidOperationException("Tidak menemukan item ulasan database");
      }
      ulasan.Rating = obj.Rating;
      ulasan.Komentar = obj.Komentar;
      ulasan.Gambar = obj.Gambar;
      DbContext.Update(ulasan);
      await DbContext.SaveChangesAsync();

      return ulasan;
   }

   public Task<Ulasan> Update(Ulasan obj)
   {
      throw new NotImplementedException();
   }

   public async Task<List<UlasanViewModel>> UserUlasan(int idCustomer)
   {
      var data = await DbContext.Ulasans.Where(x => x.IdCustomer == idCustomer).ToListAsync();
      var result = new List<UlasanViewModel>();
      foreach (var item in data)
      {
         var produk = await DbContext.Produks.FirstOrDefaultAsync(x => x.IdProduk == item.IdProduk);
         result.Add(new UlasanViewModel
         {
            IdUlasan = item.IdUlasan,
            IdProduk = produk.IdProduk,
            IdCustomer = idCustomer,
            NamaProduk = produk.Nama,
            Rating = item.Rating,
            Komentar = item.Komentar,
            Gambar = item.Gambar,
            GambarProduk = produk.Gambar
         });
      }
      return result;
   }
   public Task<Boolean> isExist(int idCustomer, int idProduk)
   {
      return DbContext.Ulasans.AnyAsync(x => x.IdCustomer == idCustomer && x.IdProduk == idProduk);
   }
}