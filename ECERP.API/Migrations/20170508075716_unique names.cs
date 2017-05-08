using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ECERP.API.Migrations
{
    public partial class uniquenames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_LedgerAccounts_Name_ChartOfAccountsId",
                table: "LedgerAccounts",
                columns: new[] { "Name", "ChartOfAccountsId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_LedgerAccounts_Name_ChartOfAccountsId",
                table: "LedgerAccounts");
        }
    }
}
