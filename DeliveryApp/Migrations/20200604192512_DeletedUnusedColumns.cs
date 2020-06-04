using Microsoft.EntityFrameworkCore.Migrations;

namespace DeliveryApp.Migrations
{
    public partial class DeletedUnusedColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Client_ClientId",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_DeliveryMan_DeliveryManId",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_Order_ClientId",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_Order_DeliveryManId",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "DeliveryManId",
                table: "Order");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClientId",
                table: "Order",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeliveryManId",
                table: "Order",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Order_ClientId",
                table: "Order",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_DeliveryManId",
                table: "Order",
                column: "DeliveryManId");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Client_ClientId",
                table: "Order",
                column: "ClientId",
                principalTable: "Client",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_DeliveryMan_DeliveryManId",
                table: "Order",
                column: "DeliveryManId",
                principalTable: "DeliveryMan",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
