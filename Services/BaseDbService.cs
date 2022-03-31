using WarungKuApp.Interfaces;
using WarungKuApp.Datas;
using WarungKuApp.Datas.Entities;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace WarungKuApp.Services;

public class BaseDbService{
     protected readonly warungkuContext DbContext;
     public BaseDbService(warungkuContext dbContext)
     {
          DbContext = dbContext;
     }
     
}
