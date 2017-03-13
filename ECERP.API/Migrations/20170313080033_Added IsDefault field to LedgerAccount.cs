using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ECERP.API.Migrations
{
    public partial class AddedIsDefaultfieldtoLedgerAccount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsDebitNormal",
                table: "LedgerAccounts",
                newName: "IsDefault");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "LedgerAccounts",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 150);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsDefault",
                table: "LedgerAccounts",
                newName: "IsDebitNormal");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "LedgerAccounts",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 500);
        }
    }
}
