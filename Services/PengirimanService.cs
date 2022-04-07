using WarungKuApp.Interfaces;
using WarungKuApp.Datas;
using WarungKuApp.Datas.Entities;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using WarungKuApp.ViewModels;
using System.Linq;

namespace WarungKuApp.Services;
public class PengirimanService : BaseDbService, IPengirimanService
{
     private readonly ITransaksiService _transaksiService;
     public PengirimanService(warungkuContext dbContext, ITransaksiService transaksiService
     ) : base(dbContext)
     {
          _transaksiService = transaksiService;
     }

     public async Task<Pengiriman> Add(Pengiriman obj)
     {
          await DbContext.AddAsync(obj);
          await DbContext.SaveChangesAsync();

          return obj;
     }

     public Task<bool> Delete(int id)
     {
          throw new NotImplementedException();
     }

     public Task<List<Pengiriman>> Get(int limit, int offset, string keyword)
     {
          throw new NotImplementedException();
     }

     public Task<Pengiriman?> Get(int id)
     {
          throw new NotImplementedException();
     }

     public Task<Pengiriman?> Get(Expression<Func<Pengiriman, bool>> func)
     {
          throw new NotImplementedException();
     }

     public Task<List<Pengiriman>> GetAll()
     {
          throw new NotImplementedException();
     }

     public Task<Pengiriman> Update(Pengiriman obj)
     {
          throw new NotImplementedException();
     }
}