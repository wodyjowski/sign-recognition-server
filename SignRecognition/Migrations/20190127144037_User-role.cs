using Microsoft.EntityFrameworkCore.Migrations;

namespace SignRecognition.Migrations
{
    public partial class Userrole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "2f2006a5-1834-4953-80c3-8ea1058f2109", "d3afc2c7-4223-48d6-8f7f-6e5d93c7eb4f" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "071bbd5c-b4b4-496e-9369-3f194f813092", "c41f903c-2a31-4913-a705-b447df6205b8", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ab03594b-682d-439f-9eca-a4712d9811b6", "31e15396-854b-437a-b287-f2b7ff32f74e", "User", "USER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "071bbd5c-b4b4-496e-9369-3f194f813092", "c41f903c-2a31-4913-a705-b447df6205b8" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "ab03594b-682d-439f-9eca-a4712d9811b6", "31e15396-854b-437a-b287-f2b7ff32f74e" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "2f2006a5-1834-4953-80c3-8ea1058f2109", "d3afc2c7-4223-48d6-8f7f-6e5d93c7eb4f", "Admin", "ADMIN" });
        }
    }
}
