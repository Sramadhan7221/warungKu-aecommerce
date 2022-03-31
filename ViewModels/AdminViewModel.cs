using WarungKuApp.Datas.Entities;
namespace WarungKuApp.ViewModels
{
     public class AdminViewModel
     {
          public AdminViewModel()
          {

          }
          public AdminViewModel(int admin, string nama, string noHp, string username, string password)
          {
               IdAdmin = admin;
               Nama = nama;
               NoHp = noHp;
               Username = username;
               Password = password;
          }
          public int IdAdmin { get; set; }
          public string Nama { get; set; } = null!;
          public string? NoHp { get; set; }
          public string Username { get; set; } = null!;
          public string Password { get; set; } = null!;
          public Admin ConvertToDbModel()
          {
               return new Admin
               {
                    IdAdmin = this.IdAdmin,
                    Nama = this.Nama,
                    NoHp = this.NoHp,
                    Username = this.Username,
                    Password = this.Password
               };
          }
     }

}