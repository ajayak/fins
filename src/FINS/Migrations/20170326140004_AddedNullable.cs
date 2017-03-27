using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FINS.Migrations
{
    public partial class AddedNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Weight",
                table: "Item",
                nullable: true,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<decimal>(
                name: "StandardCost",
                table: "Item",
                type: "money",
                maxLength: 250,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "money",
                oldMaxLength: 250);

            migrationBuilder.AlterColumn<short>(
                name: "SafetyStockLevel",
                table: "Item",
                type: "smallint",
                nullable: true,
                oldClrType: typeof(short),
                oldType: "smallint");

            migrationBuilder.AlterColumn<decimal>(
                name: "ListPrice",
                table: "Item",
                type: "money",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "money");

            migrationBuilder.AlterColumn<short>(
                name: "DaysToManufacture",
                table: "Item",
                type: "smallint",
                nullable: true,
                oldClrType: typeof(short),
                oldType: "smallint");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Weight",
                table: "Item",
                nullable: false,
                oldClrType: typeof(decimal),
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "StandardCost",
                table: "Item",
                type: "money",
                maxLength: 250,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "money",
                oldMaxLength: 250,
                oldNullable: true);

            migrationBuilder.AlterColumn<short>(
                name: "SafetyStockLevel",
                table: "Item",
                type: "smallint",
                nullable: false,
                oldClrType: typeof(short),
                oldType: "smallint",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ListPrice",
                table: "Item",
                type: "money",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "money",
                oldNullable: true);

            migrationBuilder.AlterColumn<short>(
                name: "DaysToManufacture",
                table: "Item",
                type: "smallint",
                nullable: false,
                oldClrType: typeof(short),
                oldType: "smallint",
                oldNullable: true);
        }
    }
}
