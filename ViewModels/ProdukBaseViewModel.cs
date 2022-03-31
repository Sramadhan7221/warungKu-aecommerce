namespace WarungKuApp.ViewModels;
public class ProdukBaseViewModel
{
     public ProdukBaseViewModel()
     {

     }
     public ProdukBaseViewModel(int id, string nama, decimal harga)
     {
          IdProduk = id;
          Nama = nama;
          Harga = harga;
     }
     public int IdProduk { get; set; }
     public string Nama { get; set; } = null!;
     public decimal Harga { get; set; }
     public string? Gambar { get; set; }
     public string GambarSrc
     {
          get
          {
               return (string.IsNullOrEmpty(Gambar) ? "images/default.png" : Gambar);
          }
     }

}

