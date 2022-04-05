namespace WarungKuApp.Interfaces;
using WarungKuApp.Datas.Entities;
using WarungKuApp.ViewModels;

public interface IStatusService
{
     Task<List<StatusOrder>> Get();
}