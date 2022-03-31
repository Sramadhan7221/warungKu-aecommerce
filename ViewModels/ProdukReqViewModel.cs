using System.ComponentModel.DataAnnotations;
using WarungKuApp.Datas.Entities;

namespace WarungKuApp.ViewModels;
public class ProdukReqViewModel : ProdukBaseViewModel
{
     public ProdukReqViewModel()
     {
          IdKategoriProduk = Array.Empty<int>();
     }
     public ProdukReqViewModel(int id, string nama, string deskripsi, decimal harga)
     {
          IdProduk = id;
          Nama = nama;
          Deskripsi = deskripsi;
          Harga = harga;
          Stok = 100;
          IdKategoriProduk = Array.Empty<int>();
     }
     [Required]
     public new int IdProduk { get; set; }
     public new string Nama { get; set; } = null!;
     public string Deskripsi { get; set; } = null!;
     [Required]
     public new decimal Harga { get; set; }
     public int Stok { get; set; }
     public IFormFile? GambarFile { get; set; }
     public int[] IdKategoriProduk { get; set; }
     public Produk ConvertToDbModel()
     {
          return new Produk()
          {
               IdProduk = this.IdProduk,
               Nama = this.Nama,
               Deskripsi = this.Deskripsi,
               Harga = this.Harga,
               Gambar = this.Gambar ?? string.Empty,
               Stok = this.Stok
          };
     } 
}