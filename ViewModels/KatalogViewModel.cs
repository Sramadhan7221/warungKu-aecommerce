using System.ComponentModel.DataAnnotations;
using WarungKuApp.Datas.Entities;

namespace WarungKuApp.ViewModels;
public class KatalogViewModel : ProdukViewModel
{
     public KatalogViewModel()
     {
          Kategories = new List<KategoriViewModel>();
     }
     public KatalogViewModel(int id, string nama, string deskripsi, decimal harga)
     {
          IdProduk = id;
          Nama = nama;
          Harga = harga;
          Kategories = new List<KategoriViewModel>();
     }
     public List<ProdukViewModel> Produk {get; set;}
     public string Deskripsi { get; set; }
     public int Stok { get; set; }
     public int Terjual { get; set; }
}

