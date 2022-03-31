using System;
using System.Collections.Generic;

namespace WarungKuApp.Datas.Entities
{
    public partial class StatusOrder
    {
        public StatusOrder()
        {
            Transaksis = new HashSet<Transaksi>();
        }

        public int IdSatus { get; set; }
        public int Nama { get; set; }

        public virtual ICollection<Transaksi> Transaksis { get; set; }
    }
}
