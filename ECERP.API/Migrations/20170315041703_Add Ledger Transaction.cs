using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ECERP.API.Migrations
{
    public partial class AddLedgerTransaction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DisplayName",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "MiddleName",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "LedgerAccounts",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "LedgerAccounts",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "LedgerAccounts",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "LedgerAccounts",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Version",
                table: "LedgerAccounts",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "ChartsOfAccounts",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "ChartsOfAccounts",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "ChartsOfAccounts",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "ChartsOfAccounts",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Version",
                table: "ChartsOfAccounts",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Companies",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Companies",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "Companies",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "Companies",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Version",
                table: "Companies",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDate",
                table: "Users",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<string>(
                name: "ModifiedBy",
                table: "Users",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Users",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 50);

            migrationBuilder.AddColumn<byte[]>(
                name: "Version",
                table: "Users",
                rowVersion: true,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Transaction",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<string>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    ModifiedBy = table.Column<string>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    TransactionNumber = table.Column<string>(nullable: true),
                    Version = table.Column<byte[]>(rowVersion: true, nullable: true),
                    ChartOfAccountsId = table.Column<int>(nullable: true),
                    Description = table.Column<string>(maxLength: 500, nullable: true),
                    Documentation = table.Column<string>(maxLength: 50, nullable: true),
                    PostingDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transaction_ChartsOfAccounts_ChartOfAccountsId",
                        column: x => x.ChartOfAccountsId,
                        principalTable: "ChartsOfAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TransactionLine",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<string>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    ModifiedBy = table.Column<string>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    TransactionId = table.Column<int>(nullable: false),
                    Version = table.Column<byte[]>(rowVersion: true, nullable: true),
                    Amount = table.Column<decimal>(nullable: true),
                    IsDebit = table.Column<bool>(nullable: true),
                    LedgerAccountId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionLine", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransactionLine_Transaction_TransactionId",
                        column: x => x.TransactionId,
                        principalTable: "Transaction",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TransactionLine_LedgerAccounts_LedgerAccountId",
                        column: x => x.LedgerAccountId,
                        principalTable: "LedgerAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_ChartOfAccountsId",
                table: "Transaction",
                column: "ChartOfAccountsId");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionLine_TransactionId",
                table: "TransactionLine",
                column: "TransactionId");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionLine_LedgerAccountId",
                table: "TransactionLine",
                column: "LedgerAccountId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TransactionLine");

            migrationBuilder.DropTable(
                name: "Transaction");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "LedgerAccounts");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "LedgerAccounts");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "LedgerAccounts");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "LedgerAccounts");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "LedgerAccounts");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "ChartsOfAccounts");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "ChartsOfAccounts");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "ChartsOfAccounts");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "ChartsOfAccounts");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "ChartsOfAccounts");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "Users");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDate",
                table: "Users",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ModifiedBy",
                table: "Users",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Users",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "DisplayName",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Users",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Users",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MiddleName",
                table: "Users",
                maxLength: 50,
                nullable: true);
        }
    }
}
