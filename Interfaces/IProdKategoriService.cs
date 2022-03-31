namespace WarungKuApp.Interfaces;
using WarungKuApp.Datas.Entities;
public interface IProdKategoriService
{
     Task<int[]> GetKategoriIds(int produkId);
     Task Remove(int produkId, int idKategori);
}