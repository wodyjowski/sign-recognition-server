using Microsoft.EntityFrameworkCore.Migrations;

namespace SignRecognition.Migrations
{
    public partial class PredictionName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "071bbd5c-b4b4-496e-9369-3f194f813092", "c41f903c-2a31-4913-a705-b447df6205b8" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "ab03594b-682d-439f-9eca-a4712d9811b6", "31e15396-854b-437a-b287-f2b7ff32f74e" });

            migrationBuilder.AddColumn<string>(
                name: "LocationName",
                table: "Predictions",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "1abfad4a-3976-4b74-b155-be4f1dd1b046", "7ec43729-6b82-41ae-9bb9-80a283b25a1e", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "38158553-4a67-485c-85b3-adb35663fac0", "17e83e40-1708-49c2-ad97-d89e4d3869bb", "User", "USER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "1abfad4a-3976-4b74-b155-be4f1dd1b046", "7ec43729-6b82-41ae-9bb9-80a283b25a1e" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "38158553-4a67-485c-85b3-adb35663fac0", "17e83e40-1708-49c2-ad97-d89e4d3869bb" });

            migrationBuilder.DropColumn(
                name: "LocationName",
                table: "Predictions");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "071bbd5c-b4b4-496e-9369-3f194f813092", "c41f903c-2a31-4913-a705-b447df6205b8", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ab03594b-682d-439f-9eca-a4712d9811b6", "31e15396-854b-437a-b287-f2b7ff32f74e", "User", "USER" });
        }
    }
}
