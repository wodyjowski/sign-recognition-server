using Microsoft.EntityFrameworkCore.Migrations;

namespace SignRecognition.Migrations
{
    public partial class usertokenconnection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "13562c3f-5527-4e23-ac2a-5ba0d36fafd5", "4c3316ef-899f-49ea-896a-bacba7c42ff2" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "45908fb1-c976-423d-8a4f-adf5585ed184", "1fbf5a59-3a08-4a83-b9f0-81da45f6812e" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "1d6392e4-36e4-4b30-a160-1499094fd652", "265a1705-5039-4be5-bfc6-330c4b1a8867", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "0e189c95-c8bd-4fb9-b05c-8216b29999f3", "8b489bd5-b5d5-4a97-868f-b0a11e0209b7", "User", "USER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "0e189c95-c8bd-4fb9-b05c-8216b29999f3", "8b489bd5-b5d5-4a97-868f-b0a11e0209b7" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "1d6392e4-36e4-4b30-a160-1499094fd652", "265a1705-5039-4be5-bfc6-330c4b1a8867" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "13562c3f-5527-4e23-ac2a-5ba0d36fafd5", "4c3316ef-899f-49ea-896a-bacba7c42ff2", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "45908fb1-c976-423d-8a4f-adf5585ed184", "1fbf5a59-3a08-4a83-b9f0-81da45f6812e", "User", "USER" });
        }
    }
}
