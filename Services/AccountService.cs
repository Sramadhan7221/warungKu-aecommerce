using WarungKuApp.Interfaces;
using WarungKuApp.Datas;
using WarungKuApp.Datas.Entities;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using WarungKuApp.ViewModels;

namespace WarungKuApp.Services;
public class AccountService : BaseDbService, IAccountService
{
     public AccountService(warungkuContext dbContext) : base(dbContext)
     {
     }

     public async Task<Admin> Login(string username, string password)
     {
          var result = await DbContext.Admins.FirstOrDefaultAsync(x => x.Username == username && x.Password == password);

          return result;
     }

     public async Task<Customer> LoginCustomer(string username, string password)
     {
          var result = await DbContext.Customers.FirstOrDefaultAsync(x => x.Username == username && x.Password == password);

          return result;
     }

     public async Task<Customer> Register(RegisterViewModel request)
     {
          if (await DbContext.Customers.AnyAsync(x => x.Username == request.Username))
          {
               throw new InvalidOperationException($"Username {request.Username} Already exist");
          }
          if (await DbContext.Customers.AnyAsync(x => x.NoHp == request.NoHp))
          {
               throw new InvalidOperationException($"Phone number {request.NoHp} Already exist");
          }
          if (await DbContext.Customers.AnyAsync(x => x.Email == request.Email))
          {
               throw new InvalidOperationException($"Email Adress {request.Username} Already exist");
          }

          var newCustomer = request.ConvertToDataModel();
          await DbContext.Customers.AddAsync(newCustomer);

          await DbContext.SaveChangesAsync();

          return newCustomer;
     }

     public async Task<List<Tuple<int, string>>> GetAlamat(int idCustomer)
     {
          return await DbContext.Alamats.Where(x => x.IdCustomer == idCustomer)
          .Select(x => new Tuple<int, string>(x.IdAlamat, x.Detail))
          .ToListAsync();
     }
}