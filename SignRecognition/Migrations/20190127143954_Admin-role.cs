using Microsoft.EntityFrameworkCore.Migrations;

namespace SignRecognition.Migrations
{
    public partial class Adminrole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "2f2006a5-1834-4953-80c3-8ea1058f2109", "d3afc2c7-4223-48d6-8f7f-6e5d93c7eb4f", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "2f2006a5-1834-4953-80c3-8ea1058f2109", "d3afc2c7-4223-48d6-8f7f-6e5d93c7eb4f" });
        }
    }
}
