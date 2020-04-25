using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DeliveryApp.Migrations
{
    public partial class AcceptingOrderTimeAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "AcceptingOrderTime",
                table: "DeliveryInfos",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AcceptingOrderTime",
                table: "DeliveryInfos");
        }
    }
}
