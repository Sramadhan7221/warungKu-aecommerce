using System;
using System.Collections.Generic;

namespace WarungKuApp.Datas.Entities
{
    public partial class Customer
    {
        public Customer()
        {
            Alamats = new HashSet<Alamat>();
        }

        public int IdCustomer { get; set; }
        public string Nama { get; set; } = null!;
        public string NoHp { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? ProfilPic { get; set; }
        public string Email { get; set; } = null!;

        public virtual ICollection<Alamat> Alamats { get; set; }
    }
}
