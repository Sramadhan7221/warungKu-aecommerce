using System.ComponentModel.DataAnnotations;
using WarungKuApp.Datas.Entities;

namespace WarungKuApp.ViewModels;

public class RegisterViewModel
{
     public RegisterViewModel()
     {
          Username = string.Empty;
          Password = string.Empty;
     }
     public RegisterViewModel(string username, string password)
     {
          Username = username;
          Password = password;
     }
     [Required]
     public string Nama { get; set; } = null!;
     [Required]
     public string NoHp { get; set; } = null!;
     [Required]
     public string Username { get; set; } = null!;
     [Required]
     public string Password { get; set; } = null!;
     [Required]
     [Compare(nameof(Password))]
     public string ConfirPassword { get; set; } = null!;
     public string? ProfilPic { get; set; }
     [Required]
     [EmailAddress]
     public string Email { get; set; } = null!;

     public Customer ConvertToDataModel(){
          return new Customer{
               Nama = this.Nama,
               Email = this.Email,
               NoHp = this.NoHp,
               Username = this.Username,
               Password = this.Password,
               ProfilPic = this.ProfilPic
          };
     }

}