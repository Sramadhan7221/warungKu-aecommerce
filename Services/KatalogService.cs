using WarungKuApp.Interfaces;
using WarungKuApp.Datas;
using WarungKuApp.Datas.Entities;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace WarungKuApp.Services;

public class KatalogService :BaseDbService, IKatalogService<Produk>{
     public KatalogService(warungkuContext dbContext) : base(dbContext)
     {
          
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

}