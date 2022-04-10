using System;
using System.Collections.Generic;

namespace WarungKuApp.Datas.Entities
{
     public partial class Pengiriman
     {
          public int IdPengiriman { get; set; }
          public int NoTransaksi { get; set; }
          public string Kurir { get; set; } = null!;
          public decimal Ongkir { get; set; }
          public int IdAlamat { get; set; }
          public string Status { get; set; } = null!;
          public string? NoResi { get; set; }
          public string? Notes { get; set; }
     }
}
