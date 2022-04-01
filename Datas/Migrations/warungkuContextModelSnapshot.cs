﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WarungKuApp.Datas;

#nullable disable

namespace WarungKuApp.Datas.Migrations
{
    [DbContext(typeof(warungkuContext))]
    partial class warungkuContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseCollation("utf8mb4_general_ci")
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.HasCharSet(modelBuilder, "utf8mb4");

            modelBuilder.Entity("WarungKuApp.Datas.Entities.Admin", b =>
                {
                    b.Property<int>("IdAdmin")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int(11)")
                        .HasColumnName("id_admin");

                    b.Property<string>("Nama")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("nama");

                    b.Property<string>("NoHp")
                        .HasMaxLength(13)
                        .HasColumnType("varchar(13)")
                        .HasColumnName("no_hp");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("password");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("username");

                    b.HasKey("IdAdmin")
                        .HasName("PRIMARY");

                    b.ToTable("admin", (string)null);
                });

            modelBuilder.Entity("WarungKuApp.Datas.Entities.Alamat", b =>
                {
                    b.Property<int>("IdAlamat")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int(11)")
                        .HasColumnName("id_alamat");

                    b.Property<string>("Detail")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("detail");

                    b.Property<int>("IdCustomer")
                        .HasColumnType("int(11)")
                        .HasColumnName("id_customer");

                    b.Property<string>("KabKota")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("kab_kota");

                    b.Property<string>("Kec")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("kec");

                    b.Property<string>("Kel")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("kel");

                    b.Property<string>("KodePos")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("varchar(25)")
                        .HasColumnName("kode_pos");

                    b.Property<string>("Prov")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("prov");

                    b.HasKey("IdAlamat")
                        .HasName("PRIMARY");

                    b.HasIndex(new[] { "IdCustomer" }, "fk_id_customer");

                    b.ToTable("alamat", (string)null);
                });

            modelBuilder.Entity("WarungKuApp.Datas.Entities.Customer", b =>
                {
                    b.Property<int>("IdCustomer")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int(11)")
                        .HasColumnName("id_customer");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("email");

                    b.Property<string>("Nama")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("nama");

                    b.Property<string>("NoHp")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("varchar(13)")
                        .HasColumnName("no_hp");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("password");

                    b.Property<string>("ProfilPic")
                        .HasColumnType("text")
                        .HasColumnName("profil_pic");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("username");

                    b.HasKey("IdCustomer")
                        .HasName("PRIMARY");

                    b.ToTable("customer", (string)null);
                });

            modelBuilder.Entity("WarungKuApp.Datas.Entities.KategoriProduk", b =>
                {
                    b.Property<int>("IdKategoriProduk")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int(11)")
                        .HasColumnName("id_kategori_produk");

                    b.Property<string>("Deskripsi")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("deskripsi");

                    b.Property<string>("Icon")
                        .IsRequired()
                        .HasMaxLength(225)
                        .HasColumnType("varchar(225)")
                        .HasColumnName("icon");

                    b.Property<string>("Nama")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("nama");

                    b.HasKey("IdKategoriProduk")
                        .HasName("PRIMARY");

                    b.ToTable("kategori_produk", (string)null);
                });

            modelBuilder.Entity("WarungKuApp.Datas.Entities.Keranjang", b =>
                {
                    b.Property<int>("IdKeranjang")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int(11)")
                        .HasColumnName("id_keranjang");

                    b.Property<int>("IdCustomer")
                        .HasColumnType("int(11)")
                        .HasColumnName("id_customer");

                    b.Property<int>("IdProduk")
                        .HasColumnType("int");

                    b.Property<int>("JmlBarang")
                        .HasColumnType("int(11)")
                        .HasColumnName("jml_barang");

                    b.Property<decimal>("SubTotal")
                        .HasPrecision(10)
                        .HasColumnType("decimal(10)")
                        .HasColumnName("sub_total");

                    b.HasKey("IdKeranjang")
                        .HasName("PRIMARY");

                    b.ToTable("keranjang", (string)null);
                });

            modelBuilder.Entity("WarungKuApp.Datas.Entities.Pembayaran", b =>
                {
                    b.Property<int>("IdPembayaran")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int(11)")
                        .HasColumnName("id_pembayaran");

                    b.Property<int>("IdCustomer")
                        .HasColumnType("int(11)")
                        .HasColumnName("id_customer");

                    b.Property<string>("Metode")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("metode");

                    b.Property<int>("NoTransaksi")
                        .HasColumnType("int(11)")
                        .HasColumnName("no_transaksi");

                    b.Property<decimal>("Pajak")
                        .HasPrecision(10, 2)
                        .HasColumnType("decimal(10,2)")
                        .HasColumnName("pajak");

                    b.Property<DateTime>("TglBayar")
                        .HasColumnType("datetime")
                        .HasColumnName("tgl_bayar");

                    b.Property<decimal>("TotalBayar")
                        .HasPrecision(10)
                        .HasColumnType("decimal(10)")
                        .HasColumnName("total_bayar");

                    b.Property<string>("Tujuan")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("tujuan");

                    b.HasKey("IdPembayaran")
                        .HasName("PRIMARY");

                    b.ToTable("pembayaran", (string)null);
                });

            modelBuilder.Entity("WarungKuApp.Datas.Entities.Pengiriman", b =>
                {
                    b.Property<int>("IdPengiriman")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int(11)")
                        .HasColumnName("id_pengiriman");

                    b.Property<int>("IdAlamat")
                        .HasColumnType("int(11)")
                        .HasColumnName("id_alamat");

                    b.Property<string>("Kurir")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("kurir");

                    b.Property<int>("NoTransaksi")
                        .HasColumnType("int(11)")
                        .HasColumnName("no_transaksi");

                    b.Property<decimal>("Ongkir")
                        .HasPrecision(10, 2)
                        .HasColumnType("decimal(10,2)")
                        .HasColumnName("ongkir");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("status");

                    b.HasKey("IdPengiriman")
                        .HasName("PRIMARY");

                    b.ToTable("pengiriman", (string)null);
                });

            modelBuilder.Entity("WarungKuApp.Datas.Entities.ProdKategori", b =>
                {
                    b.Property<int>("IdProdKategori")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int(11)")
                        .HasColumnName("id_prod_kategori");

                    b.Property<int>("IdKategoriProduk")
                        .HasColumnType("int(11)")
                        .HasColumnName("id_kategori_produk");

                    b.Property<int>("IdProduk")
                        .HasColumnType("int(11)")
                        .HasColumnName("id_produk");

                    b.HasKey("IdProdKategori")
                        .HasName("PRIMARY");

                    b.HasIndex(new[] { "IdKategoriProduk" }, "fk_id_kategori");

                    b.HasIndex(new[] { "IdProduk" }, "fk_id_produk");

                    b.ToTable("prod_kategori", (string)null);
                });

            modelBuilder.Entity("WarungKuApp.Datas.Entities.Produk", b =>
                {
                    b.Property<int>("IdProduk")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int(11)")
                        .HasColumnName("id_produk");

                    b.Property<string>("Deskripsi")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("deskripsi");

                    b.Property<string>("Gambar")
                        .HasColumnType("text")
                        .HasColumnName("gambar");

                    b.Property<decimal>("Harga")
                        .HasPrecision(10, 2)
                        .HasColumnType("decimal(10,2)")
                        .HasColumnName("harga");

                    b.Property<string>("Nama")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("nama");

                    b.Property<int>("Stok")
                        .HasColumnType("int(11)")
                        .HasColumnName("stok");

                    b.HasKey("IdProduk")
                        .HasName("PRIMARY");

                    b.ToTable("produk", (string)null);
                });

            modelBuilder.Entity("WarungKuApp.Datas.Entities.StatusOrder", b =>
                {
                    b.Property<int>("IdSatus")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int(11)")
                        .HasColumnName("id_satus");

                    b.Property<int>("Nama")
                        .HasColumnType("int(11)")
                        .HasColumnName("nama");

                    b.HasKey("IdSatus")
                        .HasName("PRIMARY");

                    b.ToTable("status_order", (string)null);
                });

            modelBuilder.Entity("WarungKuApp.Datas.Entities.Transaksi", b =>
                {
                    b.Property<int>("NoTransaksi")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int(11)")
                        .HasColumnName("no_transaksi");

                    b.Property<int>("IdAlamat")
                        .HasColumnType("int(11)")
                        .HasColumnName("id_alamat");

                    b.Property<int>("IdCustomer")
                        .HasColumnType("int(11)")
                        .HasColumnName("id_customer");

                    b.Property<int>("IdKeranjang")
                        .HasColumnType("int(11)")
                        .HasColumnName("id_keranjang");

                    b.Property<decimal>("JmlBayar")
                        .HasPrecision(10, 2)
                        .HasColumnType("decimal(10,2)")
                        .HasColumnName("jml_bayar");

                    b.Property<string>("Notes")
                        .HasColumnType("text")
                        .HasColumnName("notes");

                    b.Property<int>("StatusId")
                        .HasColumnType("int(11)")
                        .HasColumnName("status_id");

                    b.HasKey("NoTransaksi")
                        .HasName("PRIMARY");

                    b.HasIndex(new[] { "StatusId" }, "fk_status_id");

                    b.ToTable("transaksi", (string)null);
                });

            modelBuilder.Entity("WarungKuApp.Datas.Entities.Alamat", b =>
                {
                    b.HasOne("WarungKuApp.Datas.Entities.Customer", "IdCustomerNavigation")
                        .WithMany("Alamats")
                        .HasForeignKey("IdCustomer")
                        .IsRequired()
                        .HasConstraintName("fk_id_customer");

                    b.Navigation("IdCustomerNavigation");
                });

            modelBuilder.Entity("WarungKuApp.Datas.Entities.ProdKategori", b =>
                {
                    b.HasOne("WarungKuApp.Datas.Entities.KategoriProduk", "IdKategoriProdukNavigation")
                        .WithMany("ProdKategoris")
                        .HasForeignKey("IdKategoriProduk")
                        .IsRequired()
                        .HasConstraintName("fk_id_kategori");

                    b.HasOne("WarungKuApp.Datas.Entities.Produk", "IdProdukNavigation")
                        .WithMany("ProdKategoris")
                        .HasForeignKey("IdProduk")
                        .IsRequired()
                        .HasConstraintName("fk_id_produk");

                    b.Navigation("IdKategoriProdukNavigation");

                    b.Navigation("IdProdukNavigation");
                });

            modelBuilder.Entity("WarungKuApp.Datas.Entities.Transaksi", b =>
                {
                    b.HasOne("WarungKuApp.Datas.Entities.StatusOrder", "Status")
                        .WithMany("Transaksis")
                        .HasForeignKey("StatusId")
                        .IsRequired()
                        .HasConstraintName("fk_status_id");

                    b.Navigation("Status");
                });

            modelBuilder.Entity("WarungKuApp.Datas.Entities.Customer", b =>
                {
                    b.Navigation("Alamats");
                });

            modelBuilder.Entity("WarungKuApp.Datas.Entities.KategoriProduk", b =>
                {
                    b.Navigation("ProdKategoris");
                });

            modelBuilder.Entity("WarungKuApp.Datas.Entities.Produk", b =>
                {
                    b.Navigation("ProdKategoris");
                });

            modelBuilder.Entity("WarungKuApp.Datas.Entities.StatusOrder", b =>
                {
                    b.Navigation("Transaksis");
                });
#pragma warning restore 612, 618
        }
    }
}
