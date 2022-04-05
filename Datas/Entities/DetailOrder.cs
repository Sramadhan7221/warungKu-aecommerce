namespace WarungKuApp.Datas.Entities;

public class DetailOrder
{
     public int Id { get; set; }
     public int IdOrder { get; set; }
     public int IdProduk { get; set; }
     public decimal Harga { get; set; }
     public int JmlBarang { get; set; }
     public decimal SubTotal { get; set; }

     public virtual Transaksi Order { get; set; } = null!;
}