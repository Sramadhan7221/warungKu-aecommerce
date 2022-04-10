using System.ComponentModel.DataAnnotations;
using WarungKuApp.Datas.Entities;

namespace WarungKuApp.ViewModels;
public class PembayaranViewModel
{
     public PembayaranViewModel()
     {

     }
     public decimal tagihan { get; set; }
     public int IdPembayaran { get; set; }
     public decimal TotalBayar { get; set; }
     public int NoTransaksi { get; set; }
     public int IdCustomer { get; set; }
     public DateTime TglBayar { get; set; }
     [Required]
     public string Metode { get; set; }
     [Required]
     public string Tujuan { get; set; }
     [Required]
     public IFormFile? GambarFile { get; set; }
     public string? BuktiBayar { get; set; }

     public Pembayaran ConvertToDbModel()
     {
          return new Pembayaran()
          {
               NoTransaksi = this.NoTransaksi,
               IdCustomer = this.IdCustomer,
               TotalBayar = this.TotalBayar,
               Tujuan = this.Tujuan ?? string.Empty,
               Metode = this.Metode ?? string.Empty,
               TglBayar = this.TglBayar,
               BuktiBayar = this.BuktiBayar ?? string.Empty
          };
     }
}