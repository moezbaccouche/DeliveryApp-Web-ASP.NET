using Microsoft.EntityFrameworkCore.Migrations;

namespace DeliveryApp.Migrations
{
    public partial class RatingEdited : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rating_DeliveryMan_DeliveryManId",
                table: "Rating");

            migrationBuilder.DropIndex(
                name: "IX_Rating_DeliveryManId",
                table: "Rating");

            migrationBuilder.DropColumn(
                name: "DeliveryManId",
                table: "Rating");

            migrationBuilder.AddColumn<int>(
                name: "IdClient",
                table: "Rating",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdDeliveryMan",
                table: "Rating",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdClient",
                table: "Rating");

            migrationBuilder.DropColumn(
                name: "IdDeliveryMan",
                table: "Rating");

            migrationBuilder.AddColumn<int>(
                name: "DeliveryManId",
                table: "Rating",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rating_DeliveryManId",
                table: "Rating",
                column: "DeliveryManId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rating_DeliveryMan_DeliveryManId",
                table: "Rating",
                column: "DeliveryManId",
                principalTable: "DeliveryMan",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
