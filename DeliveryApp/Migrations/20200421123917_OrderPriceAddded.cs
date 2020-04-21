using Microsoft.EntityFrameworkCore.Migrations;

namespace DeliveryApp.Migrations
{
    public partial class OrderPriceAddded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "OrderPrice",
                table: "Order",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderPrice",
                table: "Order");
        }
    }
}
