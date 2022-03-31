using WarungKuApp.Interfaces;
using WarungKuApp.Datas;
using WarungKuApp.Datas.Entities;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

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
}