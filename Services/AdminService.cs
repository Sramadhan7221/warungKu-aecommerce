using WarungKuApp.Interfaces;
using WarungKuApp.Datas;
using WarungKuApp.Datas.Entities;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace WarungKuApp.Services;
public class AdminService : BaseDbService, IAdminService
{
     public AdminService(warungkuContext dbContext) : base(dbContext)
     {

     }

     public async Task<Admin> Add(Admin obj, int idAdmin)
     {
          if (await DbContext.Admins.AnyAsync(x => x.IdAdmin == obj.IdAdmin))
          {
               throw new InvalidOperationException($"Admin with ID {obj.IdAdmin} is already exist");
          }

          await DbContext.AddAsync(obj);
          await DbContext.SaveChangesAsync();

          DbContext.Admins.Add(new Admin
          {
               IdAdmin = idAdmin,
               Nama = obj.Nama,
               Username = obj.Username,
               NoHp = obj.NoHp,
               Password = obj.Password,
          });

          return obj;
     }

     public async Task<Admin> Add(Admin obj)
     {
          if (await DbContext.Admins.AnyAsync(x => x.IdAdmin == obj.IdAdmin))
          {
               throw new InvalidOperationException($"Admin with ID {obj.IdAdmin} is already exist");
          }

          await DbContext.AddAsync(obj);
          await DbContext.SaveChangesAsync();

          return obj;
     }

     public async Task<bool> Delete(int id)
     {
          var Admin = await DbContext.Admins.FirstOrDefaultAsync(x => x.IdAdmin == id);

          if (Admin == null)
          {
               throw new InvalidOperationException($"Admin with ID {id} doesn't exist");
          }

          DbContext.Admins.RemoveRange(DbContext.Admins.Where(x => x.IdAdmin == id));
          DbContext.Remove(Admin);
          await DbContext.SaveChangesAsync();

          return true;
     }

     public async Task<List<Admin>> Get(int limit, int offset, string keyword)
     {
          if (string.IsNullOrEmpty(keyword))
          {
               keyword = "";
          }

          return await DbContext.Admins.Skip(offset).Take(limit).ToListAsync();
     }

     public async Task<Admin?> Get(int id)
     {
          var result = await DbContext.Admins.FirstOrDefaultAsync(x => x.IdAdmin == id);
          if (result == null)
          {
               throw new InvalidOperationException($"Admin with ID {id} doesn't exist");
          }
          return result;
     }

     public Task<Admin?> Get(Expression<Func<Admin, bool>> func)
     {
          throw new NotImplementedException();
     }

     public async Task<Admin> Update(Admin obj)
     {
          if (obj == null)
          {
               throw new ArgumentException("Admin cannot be null");
          }

          var Admin = await DbContext.Admins.FirstOrDefaultAsync(x => x.IdAdmin == obj.IdAdmin);

          if (Admin == null)
          {
               throw new InvalidOperationException($"Admin with ID{obj.IdAdmin} doesn't exist in database");
          }

          Admin.Nama = obj.Nama;
          Admin.Username = obj.Username;
          Admin.NoHp = obj.NoHp;
          Admin.Password = obj.Password;

          DbContext.Update(Admin);
          await DbContext.SaveChangesAsync();

          return Admin;
     }
     public async Task<List<Admin>> GetAll()
     {
          return await DbContext.Admins.ToListAsync();
     }
}