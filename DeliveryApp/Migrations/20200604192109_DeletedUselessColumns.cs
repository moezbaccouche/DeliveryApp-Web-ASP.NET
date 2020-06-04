using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DeliveryApp.Migrations
{
    public partial class DeletedUselessColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeliveryInfos_DeliveryMan_DeliveryManId",
                table: "DeliveryInfos");

            migrationBuilder.DropForeignKey(
                name: "FK_DeliveryInfos_Order_OrderId",
                table: "DeliveryInfos");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductOrder_Product_ArticleId",
                table: "ProductOrder");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductOrder_Order_OrderId",
                table: "ProductOrder");

            migrationBuilder.DropIndex(
                name: "IX_ProductOrder_ArticleId",
                table: "ProductOrder");

            migrationBuilder.DropIndex(
                name: "IX_ProductOrder_OrderId",
                table: "ProductOrder");

            migrationBuilder.DropIndex(
                name: "IX_DeliveryInfos_DeliveryManId",
                table: "DeliveryInfos");

            migrationBuilder.DropIndex(
                name: "IX_DeliveryInfos_OrderId",
                table: "DeliveryInfos");

            migrationBuilder.DropColumn(
                name: "ArticleId",
                table: "ProductOrder");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "ProductOrder");

            migrationBuilder.DropColumn(
                name: "EstimatedDeliveryTime",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "RealDeliveryTime",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "DeliveryManId",
                table: "DeliveryInfos");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "DeliveryInfos");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ArticleId",
                table: "ProductOrder",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "ProductOrder",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EstimatedDeliveryTime",
                table: "Order",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "RealDeliveryTime",
                table: "Order",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "DeliveryManId",
                table: "DeliveryInfos",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "DeliveryInfos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductOrder_ArticleId",
                table: "ProductOrder",
                column: "ArticleId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductOrder_OrderId",
                table: "ProductOrder",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryInfos_DeliveryManId",
                table: "DeliveryInfos",
                column: "DeliveryManId");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryInfos_OrderId",
                table: "DeliveryInfos",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_DeliveryInfos_DeliveryMan_DeliveryManId",
                table: "DeliveryInfos",
                column: "DeliveryManId",
                principalTable: "DeliveryMan",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DeliveryInfos_Order_OrderId",
                table: "DeliveryInfos",
                column: "OrderId",
                principalTable: "Order",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductOrder_Product_ArticleId",
                table: "ProductOrder",
                column: "ArticleId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductOrder_Order_OrderId",
                table: "ProductOrder",
                column: "OrderId",
                principalTable: "Order",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
