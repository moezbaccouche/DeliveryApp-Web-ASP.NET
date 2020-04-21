using Microsoft.EntityFrameworkCore.Migrations;

namespace DeliveryApp.Migrations
{
    public partial class DeliveryInfosModified : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeliveryInfos_DeliveryMan_DeliveryManId",
                table: "DeliveryInfos");

            migrationBuilder.DropForeignKey(
                name: "FK_DeliveryInfos_Order_OrderId",
                table: "DeliveryInfos");

            migrationBuilder.AlterColumn<int>(
                name: "OrderId",
                table: "DeliveryInfos",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "DeliveryManId",
                table: "DeliveryInfos",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "IdDeliveryMan",
                table: "DeliveryInfos",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdOrder",
                table: "DeliveryInfos",
                nullable: false,
                defaultValue: 0);

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeliveryInfos_DeliveryMan_DeliveryManId",
                table: "DeliveryInfos");

            migrationBuilder.DropForeignKey(
                name: "FK_DeliveryInfos_Order_OrderId",
                table: "DeliveryInfos");

            migrationBuilder.DropColumn(
                name: "IdDeliveryMan",
                table: "DeliveryInfos");

            migrationBuilder.DropColumn(
                name: "IdOrder",
                table: "DeliveryInfos");

            migrationBuilder.AlterColumn<int>(
                name: "OrderId",
                table: "DeliveryInfos",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DeliveryManId",
                table: "DeliveryInfos",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DeliveryInfos_DeliveryMan_DeliveryManId",
                table: "DeliveryInfos",
                column: "DeliveryManId",
                principalTable: "DeliveryMan",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DeliveryInfos_Order_OrderId",
                table: "DeliveryInfos",
                column: "OrderId",
                principalTable: "Order",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
