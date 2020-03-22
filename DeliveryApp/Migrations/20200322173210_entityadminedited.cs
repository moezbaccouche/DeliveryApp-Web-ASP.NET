using Microsoft.EntityFrameworkCore.Migrations;

namespace DeliveryApp.Migrations
{
    public partial class entityadminedited : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LocationId",
                table: "Admin",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PicturePath",
                table: "Admin",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Admin_LocationId",
                table: "Admin",
                column: "LocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Admin_Location_LocationId",
                table: "Admin",
                column: "LocationId",
                principalTable: "Location",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Admin_Location_LocationId",
                table: "Admin");

            migrationBuilder.DropIndex(
                name: "IX_Admin_LocationId",
                table: "Admin");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "Admin");

            migrationBuilder.DropColumn(
                name: "PicturePath",
                table: "Admin");
        }
    }
}
