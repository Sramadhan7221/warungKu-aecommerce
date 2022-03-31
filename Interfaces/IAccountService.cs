namespace WarungKuApp.Interfaces;
using WarungKuApp.Datas.Entities;
public interface IAccountService
{
     Task<Admin> Login(string username, string password);
}