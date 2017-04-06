using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ECERP.API.Migrations
{
    public partial class Removeuniqueledgeraccountnameconstraint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_LedgerAccounts_Name",
                table: "LedgerAccounts");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_LedgerAccounts_Name",
                table: "LedgerAccounts",
                column: "Name",
                unique: true);
        }
    }
}
