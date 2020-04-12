using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DeliveryApp.Migrations
{
    public partial class Base64ImageForUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "ImageBase64",
                table: "DeliveryMan",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "ImageBase64",
                table: "Client",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "ImageBase64",
                table: "Admin",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageBase64",
                table: "DeliveryMan");

            migrationBuilder.DropColumn(
                name: "ImageBase64",
                table: "Client");

            migrationBuilder.DropColumn(
                name: "ImageBase64",
                table: "Admin");
        }
    }
}
