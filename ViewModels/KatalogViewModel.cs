using System.ComponentModel.DataAnnotations;
using WarungKuApp.Datas.Entities;

namespace WarungKuApp.ViewModels;
public class KatalogViewModel : ProdukBaseViewModel
{
     public KatalogViewModel()
     {
          Kategories = new List<KategoriViewModel>();
     }
     public KatalogViewModel(int id, string nama, decimal harga)
     {
          IdProduk = id;
          Nama = nama;
          Harga = harga;
          Kategories = new List<KategoriViewModel>();
     }
     public List<KategoriViewModel> Kategories { get; set; }
     public List<ProdukViewModel> Produk {get; set;}

     public Produk ConvertToDbModel()
     {
          return new Produk()
          {
               IdProduk = this.IdProduk,
               Nama = this.Nama,
               Harga = this.Harga,
               Gambar = this.Gambar ?? string.Empty,
          };
     }
}

