using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WarungKuApp.Datas.Migrations
{
    public partial class DetailOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "id_keranjang",
                table: "transaksi");

            migrationBuilder.RenameColumn(
                name: "IdProduk",
                table: "keranjang",
                newName: "id_produk");

            migrationBuilder.AddColumn<DateTime>(
                name: "tgl_transaksi",
                table: "transaksi",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "AlamatIdAlamat",
                table: "pengiriman",
                type: "int(11)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TransaksiNoTransaksi",
                table: "pengiriman",
                type: "int(11)",
                nullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "total_bayar",
                table: "pembayaran",
                type: "decimal(10)",
                precision: 10,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,30)",
                oldPrecision: 10);

            migrationBuilder.AddColumn<int>(
                name: "CustomerIdCustomer",
                table: "pembayaran",
                type: "int(11)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TransaksiNoTransaksi",
                table: "pembayaran",
                type: "int(11)",
                nullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "sub_total",
                table: "keranjang",
                type: "decimal(10)",
                precision: 10,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,30)",
                oldPrecision: 10);

            migrationBuilder.AlterColumn<int>(
                name: "id_produk",
                table: "keranjang",
                type: "int(11)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "detail_order",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    id_order = table.Column<int>(type: "int(11)", nullable: false),
                    id_produk = table.Column<int>(type: "int", nullable: false),
                    harga = table.Column<decimal>(type: "decimal(10)", precision: 10, nullable: false),
                    jml_barang = table.Column<int>(type: "int", nullable: false),
                    subtotal = table.Column<decimal>(type: "decimal(10)", precision: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_detail_order", x => x.id);
                    table.ForeignKey(
                        name: "detail_order_FK_1",
                        column: x => x.id_order,
                        principalTable: "transaksi",
                        principalColumn: "no_transaksi");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_transaksi_id_alamat",
                table: "transaksi",
                column: "id_alamat");

            migrationBuilder.CreateIndex(
                name: "IX_transaksi_id_customer",
                table: "transaksi",
                column: "id_customer");

            migrationBuilder.CreateIndex(
                name: "IX_pengiriman_AlamatIdAlamat",
                table: "pengiriman",
                column: "AlamatIdAlamat");

            migrationBuilder.CreateIndex(
                name: "IX_pengiriman_TransaksiNoTransaksi",
                table: "pengiriman",
                column: "TransaksiNoTransaksi");

            migrationBuilder.CreateIndex(
                name: "IX_pembayaran_CustomerIdCustomer",
                table: "pembayaran",
                column: "CustomerIdCustomer");

            migrationBuilder.CreateIndex(
                name: "IX_pembayaran_TransaksiNoTransaksi",
                table: "pembayaran",
                column: "TransaksiNoTransaksi");

            migrationBuilder.CreateIndex(
                name: "keranjang_FK",
                table: "keranjang",
                column: "id_produk");

            migrationBuilder.CreateIndex(
                name: "keranjang_FK_1",
                table: "keranjang",
                column: "id_customer");

            migrationBuilder.CreateIndex(
                name: "detail_order_FK_1",
                table: "detail_order",
                column: "id_order");

            migrationBuilder.AddForeignKey(
                name: "keranjang_FK",
                table: "keranjang",
                column: "id_produk",
                principalTable: "produk",
                principalColumn: "id_produk");

            migrationBuilder.AddForeignKey(
                name: "keranjang_FK_1",
                table: "keranjang",
                column: "id_customer",
                principalTable: "customer",
                principalColumn: "id_customer");

            migrationBuilder.AddForeignKey(
                name: "FK_pembayaran_customer_CustomerIdCustomer",
                table: "pembayaran",
                column: "CustomerIdCustomer",
                principalTable: "customer",
                principalColumn: "id_customer");

            migrationBuilder.AddForeignKey(
                name: "FK_pembayaran_transaksi_TransaksiNoTransaksi",
                table: "pembayaran",
                column: "TransaksiNoTransaksi",
                principalTable: "transaksi",
                principalColumn: "no_transaksi");

            migrationBuilder.AddForeignKey(
                name: "FK_pengiriman_alamat_AlamatIdAlamat",
                table: "pengiriman",
                column: "AlamatIdAlamat",
                principalTable: "alamat",
                principalColumn: "id_alamat");

            migrationBuilder.AddForeignKey(
                name: "FK_pengiriman_transaksi_TransaksiNoTransaksi",
                table: "pengiriman",
                column: "TransaksiNoTransaksi",
                principalTable: "transaksi",
                principalColumn: "no_transaksi");

            migrationBuilder.AddForeignKey(
                name: "order_FK_1",
                table: "transaksi",
                column: "id_customer",
                principalTable: "customer",
                principalColumn: "id_customer");

            migrationBuilder.AddForeignKey(
                name: "order_FK_2",
                table: "transaksi",
                column: "id_alamat",
                principalTable: "alamat",
                principalColumn: "id_alamat");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "keranjang_FK",
                table: "keranjang");

            migrationBuilder.DropForeignKey(
                name: "keranjang_FK_1",
                table: "keranjang");

            migrationBuilder.DropForeignKey(
                name: "FK_pembayaran_customer_CustomerIdCustomer",
                table: "pembayaran");

            migrationBuilder.DropForeignKey(
                name: "FK_pembayaran_transaksi_TransaksiNoTransaksi",
                table: "pembayaran");

            migrationBuilder.DropForeignKey(
                name: "FK_pengiriman_alamat_AlamatIdAlamat",
                table: "pengiriman");

            migrationBuilder.DropForeignKey(
                name: "FK_pengiriman_transaksi_TransaksiNoTransaksi",
                table: "pengiriman");

            migrationBuilder.DropForeignKey(
                name: "order_FK_1",
                table: "transaksi");

            migrationBuilder.DropForeignKey(
                name: "order_FK_2",
                table: "transaksi");

            migrationBuilder.DropTable(
                name: "detail_order");

            migrationBuilder.DropIndex(
                name: "IX_transaksi_id_alamat",
                table: "transaksi");

            migrationBuilder.DropIndex(
                name: "IX_transaksi_id_customer",
                table: "transaksi");

            migrationBuilder.DropIndex(
                name: "IX_pengiriman_AlamatIdAlamat",
                table: "pengiriman");

            migrationBuilder.DropIndex(
                name: "IX_pengiriman_TransaksiNoTransaksi",
                table: "pengiriman");

            migrationBuilder.DropIndex(
                name: "IX_pembayaran_CustomerIdCustomer",
                table: "pembayaran");

            migrationBuilder.DropIndex(
                name: "IX_pembayaran_TransaksiNoTransaksi",
                table: "pembayaran");

            migrationBuilder.DropIndex(
                name: "keranjang_FK",
                table: "keranjang");

            migrationBuilder.DropIndex(
                name: "keranjang_FK_1",
                table: "keranjang");

            migrationBuilder.DropColumn(
                name: "tgl_transaksi",
                table: "transaksi");

            migrationBuilder.DropColumn(
                name: "AlamatIdAlamat",
                table: "pengiriman");

            migrationBuilder.DropColumn(
                name: "TransaksiNoTransaksi",
                table: "pengiriman");

            migrationBuilder.DropColumn(
                name: "CustomerIdCustomer",
                table: "pembayaran");

            migrationBuilder.DropColumn(
                name: "TransaksiNoTransaksi",
                table: "pembayaran");

            migrationBuilder.RenameColumn(
                name: "id_produk",
                table: "keranjang",
                newName: "IdProduk");

            migrationBuilder.AddColumn<int>(
                name: "id_keranjang",
                table: "transaksi",
                type: "int(11)",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<decimal>(
                name: "total_bayar",
                table: "pembayaran",
                type: "decimal(10,30)",
                precision: 10,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10)",
                oldPrecision: 10);

            migrationBuilder.AlterColumn<decimal>(
                name: "sub_total",
                table: "keranjang",
                type: "decimal(10,30)",
                precision: 10,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10)",
                oldPrecision: 10);

            migrationBuilder.AlterColumn<int>(
                name: "IdProduk",
                table: "keranjang",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int(11)");
        }
    }
}
