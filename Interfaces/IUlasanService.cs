namespace WarungKuApp.Interfaces;
using WarungKuApp.Datas.Entities;
using WarungKuApp.ViewModels;

public interface IUlasanService : ICrudService<Ulasan>
{
   Task<List<UlasanViewModel>> UserUlasan(int idCustomer);
   Task<Ulasan> UpdateNew(UlasanViewModel obj);
   Task<Boolean> isExist(int noTrans,int idProduk);
}