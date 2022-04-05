using System;
using System.Collections.Generic;

namespace WarungKuApp.Datas.Entities
{
    public partial class Transaksi
    {
        public Transaksi()
        {

            Pembayarans = new HashSet<Pembayaran>();
            Pengiriman = new HashSet<Pengiriman>();
            DetailOrders = new HashSet<DetailOrder>();
        }
        public int NoTransaksi { get; set; }
        public DateTime TglTransaksi { get; set; }
        public decimal JmlBayar { get; set; }
        public int IdAlamat { get; set; }
        public int IdCustomer { get; set; }
        public int StatusId { get; set; }
        public string? Notes { get; set; }

        public virtual Alamat IdAlamatNavigation { get; set; } = null!;
        public virtual Customer IdCustomerNavigation { get; set; } = null!;
        public virtual ICollection<Pembayaran> Pembayarans { get; set; }
        public virtual ICollection<Pengiriman> Pengiriman { get; set; }
        public virtual ICollection<DetailOrder> DetailOrders { get; set; }
        public virtual StatusOrder StatusNavigation { get; set; } = null!;
    }
}
