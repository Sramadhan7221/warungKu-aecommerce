using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WarungKuApp.Datas.Migrations
{
    public partial class AlterTableAdmin01 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "admin",
                columns: table => new
                {
                    id_admin = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nama = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    no_hp = table.Column<string>(type: "varchar(13)", maxLength: 13, nullable: true, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    username = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    password = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id_admin);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateTable(
                name: "customer",
                columns: table => new
                {
                    id_customer = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nama = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    no_hp = table.Column<string>(type: "varchar(13)", maxLength: 13, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    username = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    password = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    profil_pic = table.Column<string>(type: "text", nullable: true, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    email = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id_customer);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateTable(
                name: "kategori_produk",
                columns: table => new
                {
                    id_kategori_produk = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nama = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    deskripsi = table.Column<string>(type: "text", nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    icon = table.Column<string>(type: "varchar(225)", maxLength: 225, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id_kategori_produk);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateTable(
                name: "keranjang",
                columns: table => new
                {
                    id_keranjang = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    id_customer = table.Column<int>(type: "int(11)", nullable: false),
                    jml_barang = table.Column<int>(type: "int(11)", nullable: false),
                    sub_total = table.Column<decimal>(type: "decimal(10)", precision: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id_keranjang);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateTable(
                name: "pembayaran",
                columns: table => new
                {
                    id_pembayaran = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    metode = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    total_bayar = table.Column<decimal>(type: "decimal(10)", precision: 10, nullable: false),
                    no_transaksi = table.Column<int>(type: "int(11)", nullable: false),
                    id_customer = table.Column<int>(type: "int(11)", nullable: false),
                    tgl_bayar = table.Column<DateTime>(type: "datetime", nullable: false),
                    pajak = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    tujuan = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id_pembayaran);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateTable(
                name: "pengiriman",
                columns: table => new
                {
                    id_pengiriman = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    no_transaksi = table.Column<int>(type: "int(11)", nullable: false),
                    kurir = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ongkir = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    id_alamat = table.Column<int>(type: "int(11)", nullable: false),
                    status = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id_pengiriman);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateTable(
                name: "produk",
                columns: table => new
                {
                    id_produk = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nama = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    harga = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    stok = table.Column<int>(type: "int(11)", nullable: false),
                    gambar = table.Column<string>(type: "text", nullable: true, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    deskripsi = table.Column<string>(type: "text", nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id_produk);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateTable(
                name: "status_order",
                columns: table => new
                {
                    id_satus = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nama = table.Column<int>(type: "int(11)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id_satus);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateTable(
                name: "alamat",
                columns: table => new
                {
                    id_alamat = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    id_customer = table.Column<int>(type: "int(11)", nullable: false),
                    prov = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    kab_kota = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    kec = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    kel = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    detail = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    kode_pos = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id_alamat);
                    table.ForeignKey(
                        name: "fk_id_customer",
                        column: x => x.id_customer,
                        principalTable: "customer",
                        principalColumn: "id_customer");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateTable(
                name: "prod_kategori",
                columns: table => new
                {
                    id_prod_kategori = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    id_produk = table.Column<int>(type: "int(11)", nullable: false),
                    id_kategori_produk = table.Column<int>(type: "int(11)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id_prod_kategori);
                    table.ForeignKey(
                        name: "fk_id_kategori",
                        column: x => x.id_kategori_produk,
                        principalTable: "kategori_produk",
                        principalColumn: "id_kategori_produk");
                    table.ForeignKey(
                        name: "fk_id_produk",
                        column: x => x.id_produk,
                        principalTable: "produk",
                        principalColumn: "id_produk");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateTable(
                name: "transaksi",
                columns: table => new
                {
                    no_transaksi = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    id_keranjang = table.Column<int>(type: "int(11)", nullable: false),
                    jml_bayar = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    id_alamat = table.Column<int>(type: "int(11)", nullable: false),
                    id_customer = table.Column<int>(type: "int(11)", nullable: false),
                    status_id = table.Column<int>(type: "int(11)", nullable: false),
                    notes = table.Column<string>(type: "text", nullable: true, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.no_transaksi);
                    table.ForeignKey(
                        name: "fk_status_id",
                        column: x => x.status_id,
                        principalTable: "status_order",
                        principalColumn: "id_satus");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateIndex(
                name: "fk_id_customer",
                table: "alamat",
                column: "id_customer");

            migrationBuilder.CreateIndex(
                name: "fk_id_kategori",
                table: "prod_kategori",
                column: "id_kategori_produk");

            migrationBuilder.CreateIndex(
                name: "fk_id_produk",
                table: "prod_kategori",
                column: "id_produk");

            migrationBuilder.CreateIndex(
                name: "fk_status_id",
                table: "transaksi",
                column: "status_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "admin");

            migrationBuilder.DropTable(
                name: "alamat");

            migrationBuilder.DropTable(
                name: "keranjang");

            migrationBuilder.DropTable(
                name: "pembayaran");

            migrationBuilder.DropTable(
                name: "pengiriman");

            migrationBuilder.DropTable(
                name: "prod_kategori");

            migrationBuilder.DropTable(
                name: "transaksi");

            migrationBuilder.DropTable(
                name: "customer");

            migrationBuilder.DropTable(
                name: "kategori_produk");

            migrationBuilder.DropTable(
                name: "produk");

            migrationBuilder.DropTable(
                name: "status_order");
        }
    }
}
