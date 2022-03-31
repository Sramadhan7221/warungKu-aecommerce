using WarungKuApp.Interfaces;
using WarungKuApp.Datas;
using WarungKuApp.Datas.Entities;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace WarungKuApp.Services;
public class KategoriProdukService : BaseDbService, IKategoriService
{
     public KategoriProdukService(warungkuContext dbContext) : base(dbContext)
     {
     }

     public async Task<KategoriProduk> Add(KategoriProduk obj)
     {
          if (await DbContext.KategoriProduks.AnyAsync(x => x.IdKategoriProduk == obj.IdKategoriProduk))
          {
               throw new InvalidOperationException($"KategoriProduk with ID {obj.IdKategoriProduk} is already exist");
          }

          await DbContext.AddAsync(obj);
          await DbContext.SaveChangesAsync();

          return obj;
     }

     public async Task<bool> Delete(int id)
     {
          var KategoriProduk = await DbContext.KategoriProduks.FirstOrDefaultAsync(x => x.IdKategoriProduk == id);

          if (KategoriProduk == null)
          {
               throw new InvalidOperationException($"KategoriProduk with ID {id} doesn't exist");
          }

          DbContext.ProdKategoris.RemoveRange(DbContext.ProdKategoris.Where(x => x.IdKategoriProduk == id));
          DbContext.Remove(KategoriProduk);
          await DbContext.SaveChangesAsync();

          return true;
     }

     public async Task<List<KategoriProduk>> Get(int limit, int offset, string keyword)
     {
          if (string.IsNullOrEmpty(keyword))
          {
               keyword = "";
          }

          return await DbContext.KategoriProduks
          .Skip(offset)
          .Take(limit).ToListAsync();
     }

     public async Task<KategoriProduk?> Get(int id)
     {
          var result = await DbContext.KategoriProduks.FirstOrDefaultAsync(x => x.IdKategoriProduk == id);

          if (result == null)
          {
               throw new InvalidOperationException($"Kategori with ID {id} doesn't exist");
          }

          return result;
     }

     public Task<KategoriProduk?> Get(Expression<Func<KategoriProduk, bool>> func)
     {
          throw new NotImplementedException();
     }

     public async Task<List<KategoriProduk>> GetAll()
     {
          return await DbContext.KategoriProduks.ToListAsync();
     }

     public async Task<KategoriProduk> Update(KategoriProduk obj)
     {
          if (obj == null)
          {
               throw new ArgumentNullException("KategoriProduk cannot be null");
          }

          var KategoriProduk = await DbContext.KategoriProduks.FirstOrDefaultAsync(x => x.IdKategoriProduk == obj.IdKategoriProduk);

          if (KategoriProduk == null)
          {
               throw new InvalidOperationException($"KategoriProduk with ID {obj.IdKategoriProduk} doesn't exist in database");
          }

          KategoriProduk.Nama = obj.Nama;
          KategoriProduk.Deskripsi = obj.Deskripsi;
          KategoriProduk.Icon = obj.Icon;

          DbContext.Update(KategoriProduk);
          await DbContext.SaveChangesAsync();

          return KategoriProduk;
     }
}