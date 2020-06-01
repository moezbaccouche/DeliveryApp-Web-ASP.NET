using Microsoft.EntityFrameworkCore.Migrations;

namespace DeliveryApp.Migrations
{
    public partial class PlayerIdAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PlayerId",
                table: "DeliveryMan",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PlayerId",
                table: "Client",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PlayerId",
                table: "DeliveryMan");

            migrationBuilder.DropColumn(
                name: "PlayerId",
                table: "Client");
        }
    }
}
