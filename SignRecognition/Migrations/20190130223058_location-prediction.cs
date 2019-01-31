using Microsoft.EntityFrameworkCore.Migrations;

namespace SignRecognition.Migrations
{
    public partial class locationprediction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "2bffeb40-14a3-4d4b-a5e8-4d3a880a3111", "eff60537-30cb-43de-ba56-b3fd392c8ada" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "f916d3c9-f95c-4bc7-a9d0-e0a178259ad2", "9a283276-0cf2-4d78-a347-40a35cc930b4" });

            migrationBuilder.AddColumn<string>(
                name: "LocationId",
                table: "Predictions",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b61c4e49-ef3d-4586-9063-879ec1f926d2", "cb470dbd-03a0-4284-9d23-be7640481f92", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "e6c47985-e77c-4667-9c86-6e97afb471c5", "43ced77b-4027-443d-8543-93148343eefb", "User", "USER" });

            migrationBuilder.CreateIndex(
                name: "IX_Predictions_LocationId",
                table: "Predictions",
                column: "LocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Predictions_Locations_LocationId",
                table: "Predictions",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Predictions_Locations_LocationId",
                table: "Predictions");

            migrationBuilder.DropIndex(
                name: "IX_Predictions_LocationId",
                table: "Predictions");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "b61c4e49-ef3d-4586-9063-879ec1f926d2", "cb470dbd-03a0-4284-9d23-be7640481f92" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "e6c47985-e77c-4667-9c86-6e97afb471c5", "43ced77b-4027-443d-8543-93148343eefb" });

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "Predictions");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "2bffeb40-14a3-4d4b-a5e8-4d3a880a3111", "eff60537-30cb-43de-ba56-b3fd392c8ada", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f916d3c9-f95c-4bc7-a9d0-e0a178259ad2", "9a283276-0cf2-4d78-a347-40a35cc930b4", "User", "USER" });
        }
    }
}
