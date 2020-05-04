using Microsoft.EntityFrameworkCore.Migrations;

namespace DeliveryApp.Migrations
{
    public partial class IdentityIdColumnAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HasValidatedEmail",
                table: "DeliveryMan",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "IdentityId",
                table: "DeliveryMan",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HasValidatedEmail",
                table: "Client",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "IdentityId",
                table: "Client",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HasValidatedEmail",
                table: "Admin",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "IdentityId",
                table: "Admin",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasValidatedEmail",
                table: "DeliveryMan");

            migrationBuilder.DropColumn(
                name: "IdentityId",
                table: "DeliveryMan");

            migrationBuilder.DropColumn(
                name: "HasValidatedEmail",
                table: "Client");

            migrationBuilder.DropColumn(
                name: "IdentityId",
                table: "Client");

            migrationBuilder.DropColumn(
                name: "HasValidatedEmail",
                table: "Admin");

            migrationBuilder.DropColumn(
                name: "IdentityId",
                table: "Admin");
        }
    }
}
