using System.ComponentModel.DataAnnotations;
using WarungKuApp.Datas.Entities;

namespace WarungKuApp.ViewModels;

public class KategoriViewModel
     {
          public KategoriViewModel()
          {
               Nama = string.Empty;
               Deskripsi = string.Empty;
               Icon = string.Empty;
          }
          public int IdKategoriProduk { get; set; }
          [Required]
          public string Nama { get; set; }
          public string Deskripsi { get; set; }
          public string Icon { get; set; }
          public IFormFile? IconFile { get; set; }

          public KategoriProduk ConvertToDbModel()
          {
               return new KategoriProduk
               {
                    IdKategoriProduk = this.IdKategoriProduk,
                    Nama = this.Nama,
                    Deskripsi = this.Deskripsi,
                    Icon = this.Icon
               };
          }
     }
