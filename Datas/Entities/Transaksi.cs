using System;
using System.Collections.Generic;

namespace WarungKuApp.Datas.Entities
{
    public partial class Transaksi
    {
        public int NoTransaksi { get; set; }
        public int IdKeranjang { get; set; }
        public decimal JmlBayar { get; set; }
        public int IdAlamat { get; set; }
        public int IdCustomer { get; set; }
        public int StatusId { get; set; }
        public string? Notes { get; set; }

        public virtual StatusOrder Status { get; set; } = null!;
    }
}
