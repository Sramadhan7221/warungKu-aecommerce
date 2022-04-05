namespace WarungKuApp.Interfaces;
using WarungKuApp.Datas.Entities;
using WarungKuApp.ViewModels;

public interface ITransaksiService
{
     Task<Transaksi> Checkout(Transaksi newTransaksi);

     Task<List<TransaksiViewModel>>Riwayat(int idCustomer);
}