using System.ComponentModel.DataAnnotations;
using WarungKuApp.Datas.Entities;

namespace WarungKuApp.ViewModels;
public class UpdateStatusTransaksiViewModel
{
     public UpdateStatusTransaksiViewModel()
     {

     }

     public int NoTransaksi { get; set; }
     public int StatusId { get; set; }

}

