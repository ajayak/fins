using Microsoft.EntityFrameworkCore.Migrations;

namespace FINS.Migrations
{
    public partial class NullableOrganization : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUser_Organization_OrganizationId",
                table: "ApplicationUser");

            migrationBuilder.AlterColumn<int>(
                name: "OrganizationId",
                table: "ApplicationUser",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUser_Organization_OrganizationId",
                table: "ApplicationUser",
                column: "OrganizationId",
                principalTable: "Organization",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUser_Organization_OrganizationId",
                table: "ApplicationUser");

            migrationBuilder.AlterColumn<int>(
                name: "OrganizationId",
                table: "ApplicationUser",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUser_Organization_OrganizationId",
                table: "ApplicationUser",
                column: "OrganizationId",
                principalTable: "Organization",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
