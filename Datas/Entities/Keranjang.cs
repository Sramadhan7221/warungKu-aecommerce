using System;
using System.Collections.Generic;

namespace WarungKuApp.Datas.Entities
{
     public partial class Keranjang
     {
          public Keranjang()
          {

          }
          public int IdKeranjang { get; set; }
          public int IdProduk { get; set; }
          public int IdCustomer { get; set; }
          public int JmlBarang { get; set; }
          public decimal SubTotal { get; set; }

          public virtual Customer IdCustomerNavigation { get; set; } = null!;
          public virtual Produk IdProdukNavigation { get; set; } = null!;
     }
}
