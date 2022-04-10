using System;
using System.Collections.Generic;

namespace WarungKuApp.Datas.Entities
{
   public partial class Ulasan
   {
      public Ulasan()
      {

      }

      public int IdUlasan { get; set; }
      public int IdProduk { get; set; }
      public int Rating { get; set; }
      public string? Gambar { get; set; }
      public string Komentar { get; set; } = null!;
      public int IdCustomer { get; set; }
      public int NoTransaksi { get; set; }

      public virtual Produk IdProdukUlasan { get; set; }
   }
}
