using System;
using System.Collections.Generic;

namespace WarungKuApp.Datas.Entities
{
    public partial class Pembayaran
    {
        public int IdPembayaran { get; set; }
        public string Metode { get; set; } = null!;
        public decimal TotalBayar { get; set; }
        public int NoTransaksi { get; set; }
        public int IdCustomer { get; set; }
        public DateTime TglBayar { get; set; }
        public decimal Pajak { get; set; }
        public string Tujuan { get; set; } = null!;
        public string? BuktiBayar { get; set; }
    }
}
