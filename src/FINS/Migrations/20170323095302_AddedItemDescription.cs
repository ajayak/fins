using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FINS.Migrations
{
    public partial class AddedItemDescription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Item_ItemGroup_ItemGroupId",
                table: "Item");

            migrationBuilder.DropIndex(
                name: "IX_Item_ItemGroupId",
                table: "Item");

            migrationBuilder.AlterColumn<string>(
                name: "ReorderPoint",
                table: "Item",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Item",
                maxLength: 1000,
                nullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Item_ItemGroup_ItemGroupId",
                table: "Item");

            migrationBuilder.DropIndex(
                name: "IX_Item_ItemGroupId",
                table: "Item");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Item");

            migrationBuilder.AlterColumn<string>(
                name: "ReorderPoint",
                table: "Item",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

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
