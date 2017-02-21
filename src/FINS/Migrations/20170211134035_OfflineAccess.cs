using Microsoft.EntityFrameworkCore.Migrations;

namespace FINS.Migrations
{
    public partial class OfflineAccess : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Subject",
                table: "OpenIddictToken",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationId",
                table: "OpenIddictAuthorization",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Subject",
                table: "OpenIddictAuthorization",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OpenIddictAuthorization_ApplicationId",
                table: "OpenIddictAuthorization",
                column: "ApplicationId");

            migrationBuilder.AddForeignKey(
                name: "FK_OpenIddictAuthorization_OpenIddictApplication_ApplicationId",
                table: "OpenIddictAuthorization",
                column: "ApplicationId",
                principalTable: "OpenIddictApplication",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OpenIddictAuthorization_OpenIddictApplication_ApplicationId",
                table: "OpenIddictAuthorization");

            migrationBuilder.DropIndex(
                name: "IX_OpenIddictAuthorization_ApplicationId",
                table: "OpenIddictAuthorization");

            migrationBuilder.DropColumn(
                name: "Subject",
                table: "OpenIddictToken");

            migrationBuilder.DropColumn(
                name: "ApplicationId",
                table: "OpenIddictAuthorization");

            migrationBuilder.DropColumn(
                name: "Subject",
                table: "OpenIddictAuthorization");
        }
    }
}
