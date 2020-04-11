using Microsoft.EntityFrameworkCore.Migrations;

namespace DeliveryApp.Migrations
{
    public partial class cartProductEntityEdited : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartProducts_Client_ClientId",
                table: "CartProducts");

            migrationBuilder.DropIndex(
                name: "IX_CartProducts_ClientId",
                table: "CartProducts");

            migrationBuilder.AlterColumn<int>(
                name: "ClientId",
                table: "CartProducts",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ClientId",
                table: "CartProducts",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_CartProducts_ClientId",
                table: "CartProducts",
                column: "ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartProducts_Client_ClientId",
                table: "CartProducts",
                column: "ClientId",
                principalTable: "Client",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
