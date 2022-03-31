using WarungKuApp.Datas.Entities;
namespace WarungKuApp.Interfaces
{
     public interface IProdukService : ICrudService<Produk>
     {
          Task<Produk> Add(Produk obj, int idKategoriProduk);
     }
}