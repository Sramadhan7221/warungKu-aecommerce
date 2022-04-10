namespace WarungKuApp.Interfaces;
using WarungKuApp.Datas.Entities;
using WarungKuApp.ViewModels;

public interface ITransaksiService
{
     Task<Transaksi> Checkout(Transaksi newTransaksi);

     Task<List<TransaksiViewModel>> Riwayat(int idCustomer);

     Task<Boolean> updatesStatus(UpdateStatusTransaksiViewModel req);

     Task<Boolean> KirimPesanan(DetailTransaksiViewModel req);
     Task<List<TransaksiViewModel>> GetV1(int limit, int offset, int? status, DateTime? date);
     Task<List<TransaksiViewModel>> GetV2(int limit, int offset, int? status, DateTime? date);
     Task<List<TransaksiViewModel>> GetV3(int limit, int offset, int? status = null, DateTime? date = null);
     Task<TransaksiViewModel> Get(int noTransaksi);
     Task<List<TransaksiViewModel>> GetAll();
}