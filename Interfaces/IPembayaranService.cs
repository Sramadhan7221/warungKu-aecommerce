using WarungKuApp.Datas.Entities;
using WarungKuApp.ViewModels;

namespace WarungKuApp.Interfaces
{
     public interface IPembayaranService : ICrudService<Pembayaran>
     {
          Task<Boolean> UpdateStatus(int noTrans);
          Task<PembayaranViewModel> GetByOrderId (int noTrans);
     }
}