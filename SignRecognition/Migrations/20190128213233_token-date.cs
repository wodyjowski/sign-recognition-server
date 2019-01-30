using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SignRecognition.Migrations
{
    public partial class tokendate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "0e189c95-c8bd-4fb9-b05c-8216b29999f3", "8b489bd5-b5d5-4a97-868f-b0a11e0209b7" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "1d6392e4-36e4-4b30-a160-1499094fd652", "265a1705-5039-4be5-bfc6-330c4b1a8867" });

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "AppTokens",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "2bffeb40-14a3-4d4b-a5e8-4d3a880a3111", "eff60537-30cb-43de-ba56-b3fd392c8ada", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f916d3c9-f95c-4bc7-a9d0-e0a178259ad2", "9a283276-0cf2-4d78-a347-40a35cc930b4", "User", "USER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "2bffeb40-14a3-4d4b-a5e8-4d3a880a3111", "eff60537-30cb-43de-ba56-b3fd392c8ada" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "f916d3c9-f95c-4bc7-a9d0-e0a178259ad2", "9a283276-0cf2-4d78-a347-40a35cc930b4" });

            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "AppTokens");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "1d6392e4-36e4-4b30-a160-1499094fd652", "265a1705-5039-4be5-bfc6-330c4b1a8867", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "0e189c95-c8bd-4fb9-b05c-8216b29999f3", "8b489bd5-b5d5-4a97-868f-b0a11e0209b7", "User", "USER" });
        }
    }
}
