using System.ComponentModel.DataAnnotations;
using WarungKuApp.Datas.Entities;

namespace WarungKuApp.ViewModels
{
     public class PengirimanViewModel
     {
          public int NoTransaksi { get; set; }
          public int IdPengiriman { get; set; }
          [Required]
          public string Kurir { get; set; } = null!;
          public decimal Ongkir { get; set; }
          [Required]
          public string Status { get; set; } = null!;
          public string? noResi { get; set; }
          public int? IdAlamat { get; set; }

          public Pengiriman convertToDbModel()
          {
               return new Pengiriman()
               {
                    Kurir = string.IsNullOrEmpty(this.Kurir) ? string.Empty : this.Kurir,
                    Ongkir = this.Ongkir,
                    Status = string.IsNullOrEmpty(this.Status) ? string.Empty : this.Status,
                    NoResi = noResi,
                    NoTransaksi = this.NoTransaksi,
                    IdAlamat = this.IdAlamat.Value
               };
          }
     }
}