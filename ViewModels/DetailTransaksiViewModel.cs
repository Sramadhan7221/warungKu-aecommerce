namespace WarungKuApp.ViewModels;

public class DetailTransaksiViewModel : TransaksiViewModel
{
     public DetailTransaksiViewModel()
     {

     }

     public string? NamaCustomer { get; set; }
     public string? Nohp { get; set; }
     public string? NoResi { get; set; }
     public string? Notes { get; set; }
     public int IdPembayaran { get; set; }
     public decimal TotalBayar { get; set; }
     public int IdCustomer { get; set; }
     public DateTime TglBayar { get; set; }
     public string? Metode { get; set; }
     public string? Tujuan { get; set; }

}