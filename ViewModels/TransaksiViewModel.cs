using System.ComponentModel.DataAnnotations;
using WarungKuApp.Datas.Entities;

namespace WarungKuApp.ViewModels;

public class TransaksiViewModel
{
     public TransaksiViewModel()
     {
          DetailOrders = Array.Empty<DetailOrderViewModel>();
     }

     public TransaksiViewModel(DateTime tglTrans, decimal jmlbayar, string alamat, int idCustomer, string status, string notes)
     {
          TglTransaksi = tglTrans;
          JmlBayar = jmlbayar;
          Alamat = alamat;
          Status = status;
     }
     public DateTime TglTransaksi { get; set; }
     public decimal JmlBayar { get; set; }
     public string Alamat { get; set; }
     public string Status { get; set; }
     public DetailOrderViewModel[] DetailOrders { get; set; }
}