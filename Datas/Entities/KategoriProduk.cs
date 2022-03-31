using System;
using System.Collections.Generic;

namespace WarungKuApp.Datas.Entities
{
    public partial class KategoriProduk
    {
        public KategoriProduk()
        {
            ProdKategoris = new HashSet<ProdKategori>();
        }

        public int IdKategoriProduk { get; set; }
        public string Nama { get; set; } = null!;
        public string Deskripsi { get; set; } = null!;
        public string Icon { get; set; } = null!;

        public virtual ICollection<ProdKategori> ProdKategoris { get; set; }
    }
}
