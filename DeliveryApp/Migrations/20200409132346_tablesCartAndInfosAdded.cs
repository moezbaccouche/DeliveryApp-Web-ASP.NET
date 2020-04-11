using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DeliveryApp.Migrations
{
    public partial class tablesCartAndInfosAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CartProductId",
                table: "Product",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CartProducts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(nullable: true),
                    Amount = table.Column<string>(nullable: true),
                    ClientId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CartProducts_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Client",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CartProducts_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DeliveryInfos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EstimatedDeliveryTime = table.Column<DateTime>(nullable: false),
                    RealDeliveryTime = table.Column<DateTime>(nullable: false),
                    DeliveryManId = table.Column<int>(nullable: true),
                    OrderId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeliveryInfos_DeliveryMan_DeliveryManId",
                        column: x => x.DeliveryManId,
                        principalTable: "DeliveryMan",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DeliveryInfos_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Product_CartProductId",
                table: "Product",
                column: "CartProductId");

            migrationBuilder.CreateIndex(
                name: "IX_CartProducts_ClientId",
                table: "CartProducts",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_CartProducts_ProductId",
                table: "CartProducts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryInfos_DeliveryManId",
                table: "DeliveryInfos",
                column: "DeliveryManId");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryInfos_OrderId",
                table: "DeliveryInfos",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_CartProducts_CartProductId",
                table: "Product",
                column: "CartProductId",
                principalTable: "CartProducts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_CartProducts_CartProductId",
                table: "Product");

            migrationBuilder.DropTable(
                name: "CartProducts");

            migrationBuilder.DropTable(
                name: "DeliveryInfos");

            migrationBuilder.DropIndex(
                name: "IX_Product_CartProductId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "CartProductId",
                table: "Product");
        }
    }
}
