using WarungKuApp.Interfaces;
using WarungKuApp.Datas;
using WarungKuApp.Datas.Entities;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using WarungKuApp.ViewModels;

namespace WarungKuApp.Services;
public class StatusService : BaseDbService, IStatusService
{
     public StatusService(warungkuContext dbContext) : base(dbContext)
     {
     }

     public async Task<List<StatusOrder>> Get()
     {
          return await DbContext.StatusOrders.ToListAsync();
     }
}