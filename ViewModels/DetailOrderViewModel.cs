using WarungKuApp.Datas.Entities;
namespace WarungKuApp.ViewModels;
public class DetailOrderViewModel
{
     public DetailOrderViewModel()
     {

     }
     public int Id { get; set; }
     public int IdOrder { get; set; }
     public int IdProduk { get; set; }
     public decimal Harga { get; set; }
     public int JmlBarang { get; set; }
     public decimal SubTotal { get; set; }
     public string Image { get; set; }
     public string NamaProduk { get; set; }
     public int IdCustomer { get; set; }
     public DateTime tglTransaksi { get; set; }
}