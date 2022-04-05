namespace WarungKuApp.Interfaces;
using WarungKuApp.Datas.Entities;
using WarungKuApp.ViewModels;

public interface IAccountService
{
     Task<Admin> Login(string username, string password);
     Task<Customer> LoginCustomer(string username, string password);
     Task<Customer> Register(RegisterViewModel request);
     Task<List<Tuple<int, string>>> GetAlamat(int idCustomer);
}