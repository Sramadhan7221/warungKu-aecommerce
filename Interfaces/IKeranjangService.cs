using WarungKuApp.Datas.Entities;
using WarungKuApp.ViewModels;

namespace WarungKuApp.Interfaces
{
     public interface IKeranjangService : ICrudService<Keranjang>
     {
          Task<List<KeranjangViewModel>> Get(int idCustomer);
          Task Clear(int idCustomer);
     }
}