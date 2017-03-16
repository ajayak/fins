﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FINS.Migrations
{
    public partial class UpdatedItemGroupName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Item");

            migrationBuilder.CreateTable(
                name: "ItemGroup",
                columns: table => new
                {
                    ItemGroupId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DisplayName = table.Column<string>(maxLength: 200, nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsPrimary = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    OrganizationId = table.Column<int>(nullable: false),
                    ParentId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemGroup", x => x.ItemGroupId);
                    table.ForeignKey(
                        name: "FK_ItemGroup_Organization_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organization",
                        principalColumn: "OrganizationId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ItemGroup_OrganizationId",
                table: "ItemGroup",
                column: "OrganizationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemGroup");

            migrationBuilder.CreateTable(
                name: "Item",
                columns: table => new
                {
                    ItemId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DisplayName = table.Column<string>(maxLength: 200, nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsPrimary = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    OrganizationId = table.Column<int>(nullable: false),
                    ParentId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Item", x => x.ItemId);
                    table.ForeignKey(
                        name: "FK_Item_Organization_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organization",
                        principalColumn: "OrganizationId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Item_OrganizationId",
                table: "Item",
                column: "OrganizationId");
        }
    }
}
