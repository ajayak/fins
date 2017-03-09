using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FINS.Migrations
{
    public partial class MovedPersonPropsToAccount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "CstNumber",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "ItPanNumber",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "LstNumber",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "ServiceTaxNumber",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "StateId",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "TinNumber",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "Ward",
                table: "Person");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Account",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CstNumber",
                table: "Account",
                maxLength: 15,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ItPanNumber",
                table: "Account",
                maxLength: 15,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LstNumber",
                table: "Account",
                maxLength: 15,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ServiceTaxNumber",
                table: "Account",
                maxLength: 15,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StateId",
                table: "Account",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "TinNumber",
                table: "Account",
                maxLength: 15,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ward",
                table: "Account",
                maxLength: 50,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Account");

            migrationBuilder.DropColumn(
                name: "CstNumber",
                table: "Account");

            migrationBuilder.DropColumn(
                name: "ItPanNumber",
                table: "Account");

            migrationBuilder.DropColumn(
                name: "LstNumber",
                table: "Account");

            migrationBuilder.DropColumn(
                name: "ServiceTaxNumber",
                table: "Account");

            migrationBuilder.DropColumn(
                name: "StateId",
                table: "Account");

            migrationBuilder.DropColumn(
                name: "TinNumber",
                table: "Account");

            migrationBuilder.DropColumn(
                name: "Ward",
                table: "Account");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Person",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CstNumber",
                table: "Person",
                maxLength: 15,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ItPanNumber",
                table: "Person",
                maxLength: 15,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LstNumber",
                table: "Person",
                maxLength: 15,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ServiceTaxNumber",
                table: "Person",
                maxLength: 15,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StateId",
                table: "Person",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "TinNumber",
                table: "Person",
                maxLength: 15,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ward",
                table: "Person",
                maxLength: 50,
                nullable: true);
        }
    }
}
