using System;
using System.Collections.Generic;

namespace WarungKuApp.Datas.Entities
{
    public partial class Produk
    {
        public Produk()
        {
            Keranjangs = new HashSet<Keranjang>();
            ProdKategoris = new HashSet<ProdKategori>();
        }

        public int IdProduk { get; set; }
        public string Nama { get; set; } = null!;
        public decimal Harga { get; set; }
        public int Stok { get; set; }
        public string? Gambar { get; set; }
        public string Deskripsi { get; set; } = null!;

        public virtual ICollection<Keranjang> Keranjangs { get; set; }
        public virtual ICollection<ProdKategori> ProdKategoris { get; set; }
    }
}
