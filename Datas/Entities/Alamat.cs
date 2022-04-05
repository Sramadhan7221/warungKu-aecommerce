using System;
using System.Collections.Generic;

namespace WarungKuApp.Datas.Entities
{
    public partial class Alamat
    {
        public Alamat()
        {
            Orders = new HashSet<Transaksi>();
            Pengiriman = new HashSet<Pengiriman>();
        }
        public int IdAlamat { get; set; }
        public int IdCustomer { get; set; }
        public string Prov { get; set; } = null!;
        public string KabKota { get; set; } = null!;
        public string Kec { get; set; } = null!;
        public string Kel { get; set; } = null!;
        public string Detail { get; set; } = null!;
        public string KodePos { get; set; } = null!;

        public virtual Customer IdCustomerNavigation { get; set; } = null!;
        public virtual ICollection<Transaksi> Orders { get; set; }
        public virtual ICollection<Pengiriman> Pengiriman { get; set; }
    }
}
