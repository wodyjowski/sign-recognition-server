using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SignRecognition.Migrations
{
    public partial class Filename_EditDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "EditDate",
                table: "Predictions",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "Predictions",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EditDate",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EditDate",
                table: "Predictions");

            migrationBuilder.DropColumn(
                name: "FileName",
                table: "Predictions");

            migrationBuilder.DropColumn(
                name: "EditDate",
                table: "AspNetUsers");
        }
    }
}
