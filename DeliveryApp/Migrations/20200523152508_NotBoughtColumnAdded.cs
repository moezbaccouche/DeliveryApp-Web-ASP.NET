using Microsoft.EntityFrameworkCore.Migrations;

namespace DeliveryApp.Migrations
{
    public partial class NotBoughtColumnAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Bought",
                table: "ProductOrder");

            migrationBuilder.AddColumn<bool>(
                name: "NotBought",
                table: "ProductOrder",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NotBought",
                table: "ProductOrder");

            migrationBuilder.AddColumn<bool>(
                name: "Bought",
                table: "ProductOrder",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
