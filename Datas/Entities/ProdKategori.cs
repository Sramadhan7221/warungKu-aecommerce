using System;
using System.Collections.Generic;

namespace WarungKuApp.Datas.Entities
{
    public partial class ProdKategori
    {
        public int IdProdKategori { get; set; }
        public int IdProduk { get; set; }
        public int IdKategoriProduk { get; set; }

        public virtual KategoriProduk IdKategoriProdukNavigation { get; set; } = null!;
        public virtual Produk IdProdukNavigation { get; set; } = null!;
    }
}
