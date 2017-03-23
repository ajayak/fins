using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FINS.Migrations
{
    public partial class AddedItemProps : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Item_ItemGroup_ItemGroupId",
                table: "Item");

            migrationBuilder.DropIndex(
                name: "IX_Item_ItemGroupId",
                table: "Item");

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Item",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "Item",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<short>(
                name: "DaysToManufacture",
                table: "Item",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<bool>(
                name: "IsFinishedGood",
                table: "Item",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsSelfMade",
                table: "Item",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "ListPrice",
                table: "Item",
                type: "money",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Item",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "Quantity",
                table: "Item",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "ReorderPoint",
                table: "Item",
                nullable: true);

            migrationBuilder.AddColumn<short>(
                name: "SafetyStockLevel",
                table: "Item",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<DateTime>(
                name: "SellEndTime",
                table: "Item",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "SellStartDate",
                table: "Item",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Size",
                table: "Item",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "StandardCost",
                table: "Item",
                type: "money",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "UnitId",
                table: "Item",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "Weight",
                table: "Item",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_Item_ItemGroupId",
                table: "Item",
                column: "ItemGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Item_UnitId",
                table: "Item",
                column: "UnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_Item_ItemGroup_ItemGroupId",
                table: "Item",
                column: "ItemGroupId",
                principalTable: "ItemGroup",
                principalColumn: "ItemGroupId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Item_Unit_UnitId",
                table: "Item",
                column: "UnitId",
                principalTable: "Unit",
                principalColumn: "UnitId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Item_ItemGroup_ItemGroupId",
                table: "Item");

            migrationBuilder.DropForeignKey(
                name: "FK_Item_Unit_UnitId",
                table: "Item");

            migrationBuilder.DropIndex(
                name: "IX_Item_ItemGroupId",
                table: "Item");

            migrationBuilder.DropIndex(
                name: "IX_Item_UnitId",
                table: "Item");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "Item");

            migrationBuilder.DropColumn(
                name: "Color",
                table: "Item");

            migrationBuilder.DropColumn(
                name: "DaysToManufacture",
                table: "Item");

            migrationBuilder.DropColumn(
                name: "IsFinishedGood",
                table: "Item");

            migrationBuilder.DropColumn(
                name: "IsSelfMade",
                table: "Item");

            migrationBuilder.DropColumn(
                name: "ListPrice",
                table: "Item");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Item");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Item");

            migrationBuilder.DropColumn(
                name: "ReorderPoint",
                table: "Item");

            migrationBuilder.DropColumn(
                name: "SafetyStockLevel",
                table: "Item");

            migrationBuilder.DropColumn(
                name: "SellEndTime",
                table: "Item");

            migrationBuilder.DropColumn(
                name: "SellStartDate",
                table: "Item");

            migrationBuilder.DropColumn(
                name: "Size",
                table: "Item");

            migrationBuilder.DropColumn(
                name: "StandardCost",
                table: "Item");

            migrationBuilder.DropColumn(
                name: "UnitId",
                table: "Item");

            migrationBuilder.DropColumn(
                name: "Weight",
                table: "Item");

            migrationBuilder.CreateIndex(
                name: "IX_Item_ItemGroupId",
                table: "Item",
                column: "ItemGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Item_ItemGroup_ItemGroupId",
                table: "Item",
                column: "ItemGroupId",
                principalTable: "ItemGroup",
                principalColumn: "ItemGroupId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
