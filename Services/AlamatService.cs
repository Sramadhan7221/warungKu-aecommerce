using WarungKuApp.Interfaces;
using WarungKuApp.Datas;
using WarungKuApp.Datas.Entities;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using WarungKuApp.ViewModels;

namespace WarungKuApp.Services;
public class AlamatService : BaseDbService, IAlamatService
{
     public AlamatService(warungkuContext dbContext) : base(dbContext)
     {

     }

     public async Task<Alamat> Add(Alamat obj, int idAlamat)
     {
          if (await DbContext.Alamats.AnyAsync(x => x.IdAlamat == obj.IdAlamat))
          {
               throw new InvalidOperationException($"Alamat with ID {obj.IdAlamat} is already exist");
          }

          await DbContext.AddAsync(obj);
          await DbContext.SaveChangesAsync();

          DbContext.Alamats.Add(new Alamat
          {
               IdAlamat = idAlamat,
               IdCustomer = obj.IdCustomer,
               Prov = obj.Prov,
               KabKota = obj.KabKota,
               Kec = obj.Kec,
               Kel = obj.Kel,
               KodePos = obj.KodePos,
               Detail = obj.Detail,
               IdCustomerNavigation = obj.IdCustomerNavigation
          });

          return obj;
     }

     public async Task<Alamat> Add(Alamat obj)
     {
          if (await DbContext.Alamats.AnyAsync(x => x.IdAlamat == obj.IdAlamat))
          {
               throw new InvalidOperationException($"Alamat with ID {obj.IdAlamat} is already exist");
          }

          await DbContext.AddAsync(obj);
          await DbContext.SaveChangesAsync();

          return obj;
     }

     public async Task<bool> Delete(int id)
     {
          var Alamat = await DbContext.Alamats.FirstOrDefaultAsync(x => x.IdAlamat == id);

          if (Alamat == null)
          {
               throw new InvalidOperationException($"Alamat with ID {id} doesn't exist");
          }

          DbContext.Alamats.RemoveRange(DbContext.Alamats.Where(x => x.IdAlamat == id));
          DbContext.Remove(Alamat);
          await DbContext.SaveChangesAsync();

          return true;
     }

     public async Task<List<Alamat>> Get(int limit, int offset, string keyword)
     {
          if (string.IsNullOrEmpty(keyword))
          {
               keyword = "";
          }

          return await DbContext.Alamats.Skip(offset).Take(limit).ToListAsync();
     }

     public async Task<Alamat?> Get(int id)
     {
          var result = await DbContext.Alamats.FirstOrDefaultAsync(x => x.IdAlamat == id);
          if (result == null)
          {
               throw new InvalidOperationException($"Alamat with ID {id} doesn't exist");
          }
          return result;
     }

     async Task<List<AlamatViewModel>> IAlamatService.GetAlamat(int idCustomer)
     {
          var result = await (from a in DbContext.Alamats
                              where a.IdCustomer == idCustomer
                              select new AlamatViewModel
                              {
                                   IdAlamat = a.IdAlamat,
                                   IdCustomer = a.IdCustomer,
                                   Prov = a.Prov,
                                   KabKota = a.KabKota,
                                   Kec = a.Kec,
                                   Kel = a.Kel,
                                   Detail = a.Detail,
                                   KodePos = a.KodePos
                              }).ToListAsync();
          return result;
     }

     public Task<Alamat?> Get(Expression<Func<Alamat, bool>> func)
     {
          throw new NotImplementedException();
     }

     public async Task<Alamat> Update(Alamat obj)
     {
          if (obj == null)
          {
               throw new ArgumentException("Alamat cannot be null");
          }

          var Alamat = await DbContext.Alamats.FirstOrDefaultAsync(x => x.IdAlamat == obj.IdAlamat);

          if (Alamat == null)
          {
               throw new InvalidOperationException($"Alamat with ID{obj.IdAlamat} doesn't exist in database");
          }

          Alamat.Prov = obj.Prov;
          Alamat.KabKota = obj.KabKota;
          Alamat.Kec = obj.Kec;
          Alamat.Kel = obj.Kel;
          Alamat.KodePos = obj.KodePos;
          Alamat.Detail = obj.Detail;

          DbContext.Update(Alamat);
          await DbContext.SaveChangesAsync();

          return Alamat;
     }
     public async Task<List<Alamat>> GetAll(int idCustomer)
     {
          return await DbContext.Alamats.Where(x => x.IdCustomer == idCustomer).ToListAsync();
     }
     public async Task<List<Alamat>> GetAll()
     {
          return await DbContext.Alamats.ToListAsync();
     }
}