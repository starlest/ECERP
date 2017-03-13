using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ECERP.API.Migrations
{
    public partial class ConfigureCOAandCompanyrelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ChartsOfAccounts_CompanyId",
                table: "ChartsOfAccounts");

            migrationBuilder.CreateIndex(
                name: "IX_ChartsOfAccounts_CompanyId",
                table: "ChartsOfAccounts",
                column: "CompanyId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ChartsOfAccounts_CompanyId",
                table: "ChartsOfAccounts");

            migrationBuilder.CreateIndex(
                name: "IX_ChartsOfAccounts_CompanyId",
                table: "ChartsOfAccounts",
                column: "CompanyId");
        }
    }
}
