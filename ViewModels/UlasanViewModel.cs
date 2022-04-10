using WarungKuApp.Datas.Entities;

namespace WarungKuApp.ViewModels;

public class UlasanViewModel
{
   public UlasanViewModel()
   {

   }
   public int IdUlasan { get; set; }
   public int IdTransaksi { get; set; }
   public int IdProduk { get; set; }
   public int IdCustomer { get; set; }
   public string? NamaProduk { get; set; }
   public int Rating { get; set; }
   public string? Gambar { get; set; }
   public string? GambarProduk { get; set; }
   public string Komentar { get; set; } = null!;
   public IFormFile? GambarFile { get; set; }

   public Ulasan ConvertToDb()
   {
      return new Ulasan()
      {
         IdProduk = this.IdProduk,
         IdCustomer = this.IdCustomer,
         Rating = this.Rating,
         Gambar = this.Gambar,
         Komentar = this.Komentar
      };
   }

}