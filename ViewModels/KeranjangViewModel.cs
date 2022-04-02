using System.ComponentModel.DataAnnotations;
using WarungKuApp.Datas.Entities;

namespace WarungKuApp.ViewModels;
public class KeranjangViewModel
{
     public KeranjangViewModel()
     {
     }

     public KeranjangViewModel(int idKeranjang, int idProduk, string img, string namaProduk, int idcustomer, int jmlBarang, decimal subTotal)
     {
          IdKeranjang = idKeranjang;
          IdProduk = idProduk;
          NamaProduk = namaProduk;
          IdCustomer = idcustomer;
          JmlBarang = jmlBarang;
          Subtotal = subTotal;
     }

     public int IdKeranjang { get; set; }
     public int IdProduk { get; set; }
     public string Image { get; set; }
     public string NamaProduk { get; set; }
     public int IdCustomer { get; set; }
     public int JmlBarang { get; set; }
     public decimal Subtotal { get; set; }
     public decimal harga { get; set; }

}