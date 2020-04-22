using Microsoft.EntityFrameworkCore.Migrations;

namespace DeliveryApp.Migrations
{
    public partial class WithBillColumnAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "WithBill",
                table: "Order",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WithBill",
                table: "Order");
        }
    }
}
