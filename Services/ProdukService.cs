using WarungKuApp.Interfaces;
using WarungKuApp.Datas;
using WarungKuApp.Datas.Entities;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace WarungKuApp.Services;
public class ProdukService : BaseDbService, IProdukService
{
     public ProdukService(warungkuContext dbContext) : base(dbContext)
     {

     }

     public async Task<Produk> Add(Produk obj, int idKategoriProduk)
     {
          if (await DbContext.Produks.AnyAsync(x => x.IdProduk == obj.IdProduk))
          {
               throw new InvalidOperationException($"Produk with ID {obj.IdProduk} is already exist");
          }

          await DbContext.AddAsync(obj);
          await DbContext.SaveChangesAsync();

          DbContext.ProdKategoris.Add(new ProdKategori
          {
               IdKategoriProduk = idKategoriProduk,
               IdProduk = obj.IdProduk
          });

          return obj;
     }

     public async Task<Produk> Add(Produk obj)
     {
          if (await DbContext.Produks.AnyAsync(x => x.IdProduk == obj.IdProduk))
          {
               throw new InvalidOperationException($"Produk with ID {obj.IdProduk} is already exist");
          }

          await DbContext.AddAsync(obj);
          await DbContext.SaveChangesAsync();

          return obj;
     }

     public async Task<bool> Delete(int id)
     {
          var Produk = await DbContext.Produks.FirstOrDefaultAsync(x => x.IdProduk == id);

          if (Produk == null)
          {
               throw new InvalidOperationException($"Produk with ID {id} doesn't exist");
          }

          DbContext.ProdKategoris.RemoveRange(DbContext.ProdKategoris.Where(x => x.IdProduk == id));
          DbContext.Remove(Produk);
          await DbContext.SaveChangesAsync();

          return true;
     }

     public async Task<List<Produk>> Get(int limit, int offset, string keyword)
     {
          if (string.IsNullOrEmpty(keyword))
          {
               keyword = "";
          }

          return await DbContext.Produks.Skip(offset).Take(limit).ToListAsync();
     }

     public async Task<Produk?> Get(int id)
     {
          var result = await DbContext.Produks.FirstOrDefaultAsync(x => x.IdProduk == id);
          if (result == null)
          {
               throw new InvalidOperationException($"Produk with ID {id} doesn't exist");
          }
          return result;
     }

     public Task<Produk?> Get(Expression<Func<Produk, bool>> func)
     {
          throw new NotImplementedException();
     }

     public async Task<List<Produk>> GetAll()
     {
          return await DbContext.Produks
        .Include(x => x.ProdKategoris)
        .ThenInclude(x => x.IdKategoriProdukNavigation)
        .ToListAsync();
     }

     public async Task<Produk> Update(Produk obj)
     {
          if (obj == null)
          {
               throw new ArgumentException("Produk cannot be null");
          }

          var Produk = await DbContext.Produks.FirstOrDefaultAsync(x => x.IdProduk == obj.IdProduk);

          if (Produk == null)
          {
               throw new InvalidOperationException($"Produk with ID{obj.IdProduk} doesn't exist in database");
          }

          Produk.Nama = obj.Nama;
          Produk.Deskripsi = obj.Deskripsi;
          Produk.Gambar = (string.IsNullOrEmpty(obj.Gambar)) ? Produk.Gambar : obj.Gambar;

          DbContext.Update(Produk);
          await DbContext.SaveChangesAsync();

          return Produk;
     }
}