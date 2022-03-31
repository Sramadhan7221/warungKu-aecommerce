using WarungKuApp.Interfaces;
using WarungKuApp.Datas;
using WarungKuApp.Datas.Entities;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace WarungKuApp.Services;
public class ProdKategoriService : BaseDbService, IProdKategoriService
{
     public ProdKategoriService(warungkuContext dbContext) : base(dbContext)
     {

     }

     public async Task<int[]> GetKategoriIds(int produkId)
     {
          var result = await DbContext.ProdKategoris
          .Where(x => x.IdProduk == produkId)
          .Select(x => x.IdKategoriProduk)
          .Distinct()
          .ToArrayAsync();

          return result;
     }

     public async Task Remove(int produkId, int idKategori)
     {
          var item = await DbContext.ProdKategoris.FirstOrDefaultAsync(x => x.IdProduk == produkId && x.IdKategoriProduk == idKategori);

          if (item == null)
          {
               return;
          }

          DbContext.ProdKategoris.Remove(item);

          await DbContext.SaveChangesAsync();
     }
}