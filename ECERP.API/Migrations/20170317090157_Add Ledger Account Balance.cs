using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ECERP.API.Migrations
{
    public partial class AddLedgerAccountBalance : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TransactionNumber",
                table: "Transaction");

            migrationBuilder.CreateTable(
                name: "LedgerAccountBalances",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Balance1 = table.Column<decimal>(nullable: false),
                    Balance10 = table.Column<decimal>(nullable: false),
                    Balance11 = table.Column<decimal>(nullable: false),
                    Balance12 = table.Column<decimal>(nullable: false),
                    Balance2 = table.Column<decimal>(nullable: false),
                    Balance3 = table.Column<decimal>(nullable: false),
                    Balance4 = table.Column<decimal>(nullable: false),
                    Balance5 = table.Column<decimal>(nullable: false),
                    Balance6 = table.Column<decimal>(nullable: false),
                    Balance7 = table.Column<decimal>(nullable: false),
                    Balance8 = table.Column<decimal>(nullable: false),
                    Balance9 = table.Column<decimal>(nullable: false),
                    BeginningBalance = table.Column<decimal>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedBy = table.Column<string>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    Version = table.Column<byte[]>(rowVersion: true, nullable: true),
                    Year = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LedgerAccountBalances", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LedgerAccountBalances");

            migrationBuilder.AddColumn<string>(
                name: "TransactionNumber",
                table: "Transaction",
                nullable: true);
        }
    }
}
