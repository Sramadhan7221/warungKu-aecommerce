using WarungKuApp.Interfaces;
using WarungKuApp.Datas;
using WarungKuApp.Datas.Entities;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using WarungKuApp.ViewModels;
using System.Linq;

namespace WarungKuApp.Services;
public class KeranjangService : BaseDbService, IKeranjangService
{
     private readonly IProdukService _produkService;
     public KeranjangService(warungkuContext dbContext, IProdukService produkService
     ) : base(dbContext)
     {
          _produkService = produkService;
     }

     public async Task<Keranjang> Add(Keranjang obj)
     {
          if (await DbContext.Keranjangs.AnyAsync(x => x.IdProduk == obj.IdProduk && x.IdCustomer == obj.IdCustomer))
          {
               return obj;
          }

          //get data produk
          var produk = await _produkService.Get(obj.IdProduk);

          if (produk == null)
          {
               throw new InvalidOperationException("Produk tidak ditemukan");
          }

          if (obj.JmlBarang < 1)
          {
               obj.JmlBarang = 1;
          }

          //rumus subtotal = harga * jumlah produk
          obj.SubTotal = produk.Harga * obj.JmlBarang;

          await DbContext.AddAsync(obj);
          await DbContext.SaveChangesAsync();

          return obj;
     }

     public async Task<bool> Delete(int id)
    {
        var keranjang = await DbContext.Keranjangs.FirstOrDefaultAsync(x=>x.IdKeranjang == id);

        if(keranjang == null)
        {
            throw new InvalidOperationException("cannot find cart item in database");
        }

        DbContext.Remove(keranjang);
        await DbContext.SaveChangesAsync();

        return true;
    }

     public Task<List<Keranjang>> Get(int limit, int offset, string keyword)
     {
          throw new NotImplementedException();
     }

     public Task<Keranjang?> Get(int id)
     {
          throw new NotImplementedException();
     }

     public Task<Keranjang?> Get(Expression<Func<Keranjang, bool>> func)
     {
          throw new NotImplementedException();
     }

     public Task<List<Keranjang>> GetAll()
     {
          throw new NotImplementedException();
     }

     public async Task<Keranjang> Update(Keranjang obj)
     {
          var keranjang = await DbContext.Keranjangs.FirstOrDefaultAsync(x => x.IdKeranjang == obj.IdKeranjang);

          if (keranjang == null)
          {
               throw new InvalidOperationException("cannot find cart item in database");
          }

          //get data produk
          var produk = await _produkService.Get(obj.IdProduk);

          if (produk == null)
          {
               throw new InvalidOperationException("Produk tidak ditemukan");
          }

          if (obj.JmlBarang < 1)
          {
               obj.JmlBarang = 1;
          }

          //rumus subtotal = harga * jumlah produk
          keranjang.SubTotal = produk.Harga * obj.JmlBarang;

          DbContext.Update(keranjang);
          await DbContext.SaveChangesAsync();

          return keranjang;
     }

     async Task<List<KeranjangViewModel>> IKeranjangService.Get(int idCustomer)
     {
          var result = await (from a in DbContext.Keranjangs
                              join b in DbContext.Produks on a.IdProduk equals b.IdProduk
                              where a.IdCustomer == idCustomer
                              select new KeranjangViewModel
                              {
                                   IdKeranjang = a.IdKeranjang,
                                   IdCustomer = a.IdCustomer,
                                   IdProduk = a.IdProduk,
                                   Image = b.Gambar,
                                   JmlBarang = a.JmlBarang,
                                   Subtotal = a.SubTotal,
                                   NamaProduk = b.Nama,
                                   harga = b.Harga
                              }).ToListAsync();

          return result;
     }
}