namespace WarungKuApp.Interfaces;
using WarungKuApp.Datas.Entities;
using WarungKuApp.ViewModels;
public interface IAlamatService : ICrudService<Alamat>
{
     Task<List<Alamat>> GetAll(int idCustomer);
     Task<List<AlamatViewModel>> GetAlamat(int idCustomer);

}