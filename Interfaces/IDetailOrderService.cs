using WarungKuApp.Datas.Entities;
using WarungKuApp.ViewModels;

namespace WarungKuApp.Interfaces
{
     public interface IDetailOrderService
     {
          Task<List<DetailOrderViewModel>> Get(int idCustomer);
     }
}