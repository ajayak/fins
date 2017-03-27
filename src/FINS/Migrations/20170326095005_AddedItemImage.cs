using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FINS.Migrations
{
    public partial class AddedItemImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Item_ItemGroup_ItemGroupId",
                table: "Item");

            migrationBuilder.DropIndex(
                name: "IX_Item_ItemGroupId",
                table: "Item");

            migrationBuilder.RenameColumn(
                name: "ItemGroupId",
                table: "Item",
                newName: "ItemId");

            migrationBuilder.AddColumn<string>(
                name: "DisplayImageName",
                table: "Item",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageName",
                table: "Item",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ItemGroupId",
                table: "Item",
                nullable: false,
                defaultValue: 0);

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
                name: "DisplayImageName",
                table: "Item");

            migrationBuilder.DropColumn(
                name: "ImageName",
                table: "Item");

            migrationBuilder.DropColumn(
                name: "ItemGroupId",
                table: "Item");

            migrationBuilder.RenameColumn(
                name: "ItemId",
                table: "Item",
                newName: "ItemGroupId");

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
