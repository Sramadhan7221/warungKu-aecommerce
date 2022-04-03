using WarungKuApp.Datas.Entities;
namespace WarungKuApp.ViewModels;

public class AlamatViewModel
{
     public AlamatViewModel()
     {

     }
     public AlamatViewModel(int idAlamat, int idCustomer)
     {
          IdAlamat = idAlamat;
          IdCustomer = idCustomer;
     }
     public int IdAlamat { get; set; }
     public int IdCustomer { get; set; }
     public string Prov { get; set; } = null!;
     public string KabKota { get; set; } = null!;
     public string Kec { get; set; } = null!;
     public string Kel { get; set; } = null!;
     public string Detail { get; set; } = null!;
     public string KodePos { get; set; } = null!;

     // public virtual Customer IdCustomerNavigation { get; set; } = null!;

     public Alamat ConvertToDbModel()
     {
          return new Alamat()
          {
               IdAlamat = this.IdAlamat,
               IdCustomer = this.IdCustomer,
               Prov = this.Prov,
               KabKota = this.KabKota,
               Kec = this.Kec,
               Kel = this.Kel,
               KodePos = this.KodePos,
               Detail = this.Detail,
               // IdCustomerNavigation = this.IdCustomerNavigation
          };
     }
}