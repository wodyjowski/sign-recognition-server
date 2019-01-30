using Microsoft.EntityFrameworkCore.Migrations;

namespace SignRecognition.Migrations
{
    public partial class apptoken : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "1abfad4a-3976-4b74-b155-be4f1dd1b046", "7ec43729-6b82-41ae-9bb9-80a283b25a1e" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "38158553-4a67-485c-85b3-adb35663fac0", "17e83e40-1708-49c2-ad97-d89e4d3869bb" });

            migrationBuilder.CreateTable(
                name: "AppTokens",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "13562c3f-5527-4e23-ac2a-5ba0d36fafd5", "4c3316ef-899f-49ea-896a-bacba7c42ff2", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "45908fb1-c976-423d-8a4f-adf5585ed184", "1fbf5a59-3a08-4a83-b9f0-81da45f6812e", "User", "USER" });

            migrationBuilder.CreateIndex(
                name: "IX_AppTokens_UserId",
                table: "AppTokens",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppTokens");

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
                values: new object[] { "1abfad4a-3976-4b74-b155-be4f1dd1b046", "7ec43729-6b82-41ae-9bb9-80a283b25a1e", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "38158553-4a67-485c-85b3-adb35663fac0", "17e83e40-1708-49c2-ad97-d89e4d3869bb", "User", "USER" });
        }
    }
}
