using System.ComponentModel.DataAnnotations;
using WarungKuApp.Datas.Entities;

namespace WarungKuApp.ViewModels;
public class KeranjangUpdateViewModel
{
     public KeranjangUpdateViewModel()
     {
     }

     [Required]
     public int IdKeranjang { get; set; }
     [Required]
     public int JmlBarang { get; set; }
}