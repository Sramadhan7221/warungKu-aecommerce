using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using WarungKuApp.Datas.Entities;

namespace WarungKuApp.Datas
{
     public partial class warungkuContext : DbContext
     {
          public warungkuContext()
          {
          }

          public warungkuContext(DbContextOptions<warungkuContext> options)
              : base(options)
          {
          }

          public virtual DbSet<Admin> Admins { get; set; } = null!;
          public virtual DbSet<Alamat> Alamats { get; set; } = null!;
          public virtual DbSet<Customer> Customers { get; set; } = null!;
          public virtual DbSet<KategoriProduk> KategoriProduks { get; set; } = null!;
          public virtual DbSet<Keranjang> Keranjangs { get; set; } = null!;
          public virtual DbSet<Pembayaran> Pembayarans { get; set; } = null!;
          public virtual DbSet<Pengiriman> Pengirimen { get; set; } = null!;
          public virtual DbSet<ProdKategori> ProdKategoris { get; set; } = null!;
          public virtual DbSet<Produk> Produks { get; set; } = null!;
          public virtual DbSet<StatusOrder> StatusOrders { get; set; } = null!;
          public virtual DbSet<Transaksi> Transaksis { get; set; } = null!;
          public virtual DbSet<DetailOrder> DetailOrders { get; set; } = null!;

          protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
          {
               if (!optionsBuilder.IsConfigured)
               {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                    optionsBuilder.UseMySql("server=localhost;user=root;database=warungku", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.4.17-mariadb"));
               }
          }

          protected override void OnModelCreating(ModelBuilder modelBuilder)
          {
               modelBuilder.UseCollation("utf8mb4_general_ci")
                   .HasCharSet("utf8mb4");

               modelBuilder.Entity<Admin>(entity =>
               {
                    entity.HasKey(e => e.IdAdmin)
                     .HasName("PRIMARY");

                    entity.ToTable("admin");

                    entity.Property(e => e.IdAdmin)
                     .HasColumnType("int(11)")
                     .HasColumnName("id_admin");

                    entity.Property(e => e.Nama)
                     .HasMaxLength(255)
                     .HasColumnName("nama");

                    entity.Property(e => e.NoHp)
                     .HasMaxLength(13)
                     .HasColumnName("no_hp");

                    entity.Property(e => e.Password)
                     .HasMaxLength(255)
                     .HasColumnName("password");

                    entity.Property(e => e.Username)
                     .HasMaxLength(255)
                     .HasColumnName("username");
               });

               modelBuilder.Entity<Alamat>(entity =>
               {
                    entity.HasKey(e => e.IdAlamat)
                     .HasName("PRIMARY");

                    entity.ToTable("alamat");

                    entity.HasIndex(e => e.IdCustomer, "fk_id_customer");

                    entity.Property(e => e.IdAlamat)
                     .HasColumnType("int(11)")
                     .HasColumnName("id_alamat");

                    entity.Property(e => e.Detail)
                     .HasMaxLength(255)
                     .HasColumnName("detail");

                    entity.Property(e => e.IdCustomer)
                     .HasColumnType("int(11)")
                     .HasColumnName("id_customer");

                    entity.Property(e => e.KabKota)
                     .HasMaxLength(255)
                     .HasColumnName("kab_kota");

                    entity.Property(e => e.Kec)
                     .HasMaxLength(255)
                     .HasColumnName("kec");

                    entity.Property(e => e.Kel)
                     .HasMaxLength(255)
                     .HasColumnName("kel");

                    entity.Property(e => e.KodePos)
                     .HasMaxLength(25)
                     .HasColumnName("kode_pos");

                    entity.Property(e => e.Prov)
                     .HasMaxLength(255)
                     .HasColumnName("prov");

                    entity.HasOne(d => d.IdCustomerNavigation)
                     .WithMany(p => p.Alamats)
                     .HasForeignKey(d => d.IdCustomer)
                     .OnDelete(DeleteBehavior.ClientSetNull)
                     .HasConstraintName("fk_id_customer");
               });

               modelBuilder.Entity<Customer>(entity =>
               {
                    entity.HasKey(e => e.IdCustomer)
                     .HasName("PRIMARY");

                    entity.ToTable("customer");

                    entity.Property(e => e.IdCustomer)
                     .HasColumnType("int(11)")
                     .HasColumnName("id_customer");

                    entity.Property(e => e.Email)
                     .HasMaxLength(100)
                     .HasColumnName("email");

                    entity.Property(e => e.Nama)
                     .HasMaxLength(255)
                     .HasColumnName("nama");

                    entity.Property(e => e.NoHp)
                     .HasMaxLength(13)
                     .HasColumnName("no_hp");

                    entity.Property(e => e.Password)
                     .HasMaxLength(255)
                     .HasColumnName("password");

                    entity.Property(e => e.ProfilPic)
                     .HasColumnType("text")
                     .HasColumnName("profil_pic");

                    entity.Property(e => e.Username)
                     .HasMaxLength(255)
                     .HasColumnName("username");
               });

               modelBuilder.Entity<KategoriProduk>(entity =>
               {
                    entity.HasKey(e => e.IdKategoriProduk)
                     .HasName("PRIMARY");

                    entity.ToTable("kategori_produk");

                    entity.Property(e => e.IdKategoriProduk)
                     .HasColumnType("int(11)")
                     .HasColumnName("id_kategori_produk");

                    entity.Property(e => e.Deskripsi)
                     .HasColumnType("text")
                     .HasColumnName("deskripsi");

                    entity.Property(e => e.Icon)
                     .HasMaxLength(225)
                     .HasColumnName("icon");

                    entity.Property(e => e.Nama)
                     .HasMaxLength(255)
                     .HasColumnName("nama");
               });

               modelBuilder.Entity<Keranjang>(entity =>
               {
                    entity.HasKey(e => e.IdKeranjang)
                     .HasName("PRIMARY");

                    entity.ToTable("keranjang");

                    entity.HasIndex(e => e.IdProduk, "keranjang_FK");
                    entity.HasIndex(e => e.IdCustomer, "keranjang_FK_1");
                    entity.Property(e => e.IdKeranjang)
                     .HasColumnType("int(11)")
                     .HasColumnName("id_keranjang");

                    entity.Property(e => e.IdCustomer)
                     .HasColumnType("int(11)")
                     .HasColumnName("id_customer");

                    entity.Property(e => e.IdProduk).HasColumnName("id_produk");

                    entity.Property(e => e.JmlBarang)
                     .HasColumnType("int(11)")
                     .HasColumnName("jml_barang");

                    entity.Property(e => e.SubTotal)
                     .HasPrecision(10)
                     .HasColumnName("sub_total");

                    entity.HasOne(d => d.IdCustomerNavigation)
                     .WithMany(p => p.Keranjangs)
                     .HasForeignKey(d => d.IdCustomer)
                     .OnDelete(DeleteBehavior.ClientSetNull)
                     .HasConstraintName("keranjang_FK_1");
                    entity.HasOne(d => d.IdProdukNavigation)
                     .WithMany(p => p.Keranjangs)
                     .HasForeignKey(d => d.IdProduk)
                     .OnDelete(DeleteBehavior.ClientSetNull)
                     .HasConstraintName("keranjang_FK");
               });

               modelBuilder.Entity<DetailOrder>(entity =>
                  {

                       entity.ToTable("detail_order");

                       entity.HasIndex(e => e.IdOrder, "detail_order_FK_1");

                       entity.Property(e => e.Id).HasColumnName("id");

                       entity.Property(e => e.IdOrder).HasColumnName("id_order");

                       entity.Property(e => e.IdProduk).HasColumnName("id_produk");

                       entity.Property(e => e.Harga).HasColumnName("harga")
                       .HasPrecision(10);

                       entity.Property(e => e.JmlBarang).HasColumnName("jml_barang");

                       entity.Property(e => e.SubTotal).HasColumnName("subtotal")
                       .HasPrecision(10);

                       entity.HasOne(d => d.Order)
                           .WithMany(p => p.DetailOrders)
                           .HasForeignKey(d => d.IdOrder)
                           .OnDelete(DeleteBehavior.ClientSetNull)
                           .HasConstraintName("detail_order_FK_1");
                  });

               modelBuilder.Entity<Pembayaran>(entity =>
               {
                    entity.HasKey(e => e.IdPembayaran)
                     .HasName("PRIMARY");

                    entity.ToTable("pembayaran");

                    entity.Property(e => e.IdPembayaran)
                     .HasColumnType("int(11)")
                     .HasColumnName("id_pembayaran");

                    entity.Property(e => e.IdCustomer)
                     .HasColumnType("int(11)")
                     .HasColumnName("id_customer");

                    entity.Property(e => e.Metode)
                     .HasMaxLength(50)
                     .HasColumnName("metode");

                    entity.Property(e => e.NoTransaksi)
                     .HasColumnType("int(11)")
                     .HasColumnName("no_transaksi");

                    entity.Property(e => e.Pajak)
                     .HasPrecision(10, 2)
                     .HasColumnName("pajak");

                    entity.Property(e => e.TglBayar)
                     .HasColumnType("datetime")
                     .HasColumnName("tgl_bayar");

                    entity.Property(e => e.TotalBayar)
                     .HasPrecision(10)
                     .HasColumnName("total_bayar");

                    entity.Property(e => e.Tujuan)
                     .HasMaxLength(255)
                     .HasColumnName("tujuan");

                    entity.Property(e => e.BuktiBayar)
                     .HasMaxLength(255)
                     .HasColumnName("bukti_pembayaran");
               });

               modelBuilder.Entity<Pengiriman>(entity =>
               {
                    entity.HasKey(e => e.IdPengiriman)
                     .HasName("PRIMARY");

                    entity.ToTable("pengiriman");

                    entity.Property(e => e.IdPengiriman)
                     .HasColumnType("int(11)")
                     .HasColumnName("id_pengiriman");

                    entity.Property(e => e.IdAlamat)
                     .HasColumnType("int(11)")
                     .HasColumnName("id_alamat");

                    entity.Property(e => e.Kurir)
                     .HasMaxLength(255)
                     .HasColumnName("kurir");

                    entity.Property(e => e.NoTransaksi)
                     .HasColumnType("int(11)")
                     .HasColumnName("no_transaksi");

                    entity.Property(e => e.Ongkir)
                     .HasPrecision(10, 2)
                     .HasColumnName("ongkir");

                    entity.Property(e => e.Status)
                     .HasMaxLength(50)
                     .HasColumnName("status");
               });

               modelBuilder.Entity<ProdKategori>(entity =>
               {
                    entity.HasKey(e => e.IdProdKategori)
                     .HasName("PRIMARY");

                    entity.ToTable("prod_kategori");

                    entity.HasIndex(e => e.IdKategoriProduk, "fk_id_kategori");

                    entity.HasIndex(e => e.IdProduk, "fk_id_produk");

                    entity.Property(e => e.IdProdKategori)
                     .HasColumnType("int(11)")
                     .HasColumnName("id_prod_kategori");

                    entity.Property(e => e.IdKategoriProduk)
                     .HasColumnType("int(11)")
                     .HasColumnName("id_kategori_produk");

                    entity.Property(e => e.IdProduk)
                     .HasColumnType("int(11)")
                     .HasColumnName("id_produk");

                    entity.HasOne(d => d.IdKategoriProdukNavigation)
                     .WithMany(p => p.ProdKategoris)
                     .HasForeignKey(d => d.IdKategoriProduk)
                     .OnDelete(DeleteBehavior.ClientSetNull)
                     .HasConstraintName("fk_id_kategori");

                    entity.HasOne(d => d.IdProdukNavigation)
                     .WithMany(p => p.ProdKategoris)
                     .HasForeignKey(d => d.IdProduk)
                     .OnDelete(DeleteBehavior.ClientSetNull)
                     .HasConstraintName("fk_id_produk");
               });

               modelBuilder.Entity<Produk>(entity =>
               {
                    entity.HasKey(e => e.IdProduk)
                     .HasName("PRIMARY");

                    entity.ToTable("produk");

                    entity.Property(e => e.IdProduk)
                     .HasColumnType("int(11)")
                     .HasColumnName("id_produk");

                    entity.Property(e => e.Deskripsi)
                     .HasColumnType("text")
                     .HasColumnName("deskripsi");

                    entity.Property(e => e.Gambar)
                     .HasColumnType("text")
                     .HasColumnName("gambar");

                    entity.Property(e => e.Harga)
                     .HasPrecision(10, 2)
                     .HasColumnName("harga");

                    entity.Property(e => e.Nama)
                     .HasMaxLength(255)
                     .HasColumnName("nama");

                    entity.Property(e => e.Stok)
                     .HasColumnType("int(11)")
                     .HasColumnName("stok");
               });

               modelBuilder.Entity<StatusOrder>(entity =>
               {
                    entity.HasKey(e => e.IdSatus)
                     .HasName("PRIMARY");

                    entity.ToTable("status_order");

                    entity.Property(e => e.IdSatus)
                     .HasColumnType("int(11)")
                     .HasColumnName("id_satus");

                    entity.Property(e => e.Nama)
                     .HasMaxLength(255)
                     .HasColumnName("nama");
               });

               modelBuilder.Entity<Transaksi>(entity =>
               {
                    entity.HasKey(e => e.NoTransaksi)
                     .HasName("PRIMARY");

                    entity.ToTable("transaksi");

                    entity.HasIndex(e => e.StatusId, "fk_status_id");

                    entity.Property(e => e.NoTransaksi)
                     .HasColumnType("int(11)")
                     .HasColumnName("no_transaksi");

                    entity.Property(e => e.IdAlamat)
                     .HasColumnType("int(11)")
                     .HasColumnName("id_alamat");

                    entity.Property(e => e.IdCustomer)
                     .HasColumnType("int(11)")
                     .HasColumnName("id_customer");

                    entity.Property(e => e.JmlBayar)
                     .HasPrecision(10, 2)
                     .HasColumnName("jml_bayar");

                    entity.Property(e => e.Notes)
                     .HasColumnType("text")
                     .HasColumnName("notes");

                    entity.Property(e => e.TglTransaksi)
                     .HasColumnType("datetime")
                     .HasColumnName("tgl_transaksi");

                    entity.Property(e => e.StatusId)
                     .HasColumnType("int(11)")
                     .HasColumnName("status_id");

                    entity.HasOne(d => d.IdAlamatNavigation)
                     .WithMany(p => p.Orders)
                     .HasForeignKey(d => d.IdAlamat)
                     .OnDelete(DeleteBehavior.ClientSetNull)
                     .HasConstraintName("order_FK_2");
                    entity.HasOne(d => d.IdCustomerNavigation)
                     .WithMany(p => p.Orders)
                     .HasForeignKey(d => d.IdCustomer)
                     .OnDelete(DeleteBehavior.ClientSetNull)
                     .HasConstraintName("order_FK_1");

                    entity.HasOne(d => d.StatusNavigation)
                     .WithMany(p => p.Transaksis)
                     .HasForeignKey(d => d.StatusId)
                     .OnDelete(DeleteBehavior.ClientSetNull)
                     .HasConstraintName("fk_status_id");
               });

               OnModelCreatingPartial(modelBuilder);
          }

          partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
     }
}
