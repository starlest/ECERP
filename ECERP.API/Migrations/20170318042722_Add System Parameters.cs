using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ECERP.API.Migrations
{
    public partial class AddSystemParameters : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_ChartsOfAccounts_ChartOfAccountsId",
                table: "Transaction");

            migrationBuilder.DropIndex(
                name: "IX_Transaction_ChartOfAccountsId",
                table: "Transaction");

            migrationBuilder.DropColumn(
                name: "ChartOfAccountsId",
                table: "Transaction");

            migrationBuilder.AddColumn<int>(
                name: "LedgerAccountId",
                table: "LedgerAccountBalances",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "SystemParameters",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedBy = table.Column<string>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    Value = table.Column<string>(nullable: true),
                    Version = table.Column<byte[]>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemParameters", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LedgerAccountBalances_LedgerAccountId",
                table: "LedgerAccountBalances",
                column: "LedgerAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_LedgerAccountBalances_LedgerAccounts_LedgerAccountId",
                table: "LedgerAccountBalances",
                column: "LedgerAccountId",
                principalTable: "LedgerAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LedgerAccountBalances_LedgerAccounts_LedgerAccountId",
                table: "LedgerAccountBalances");

            migrationBuilder.DropTable(
                name: "SystemParameters");

            migrationBuilder.DropIndex(
                name: "IX_LedgerAccountBalances_LedgerAccountId",
                table: "LedgerAccountBalances");

            migrationBuilder.DropColumn(
                name: "LedgerAccountId",
                table: "LedgerAccountBalances");

            migrationBuilder.AddColumn<int>(
                name: "ChartOfAccountsId",
                table: "Transaction",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_ChartOfAccountsId",
                table: "Transaction",
                column: "ChartOfAccountsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_ChartsOfAccounts_ChartOfAccountsId",
                table: "Transaction",
                column: "ChartOfAccountsId",
                principalTable: "ChartsOfAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
