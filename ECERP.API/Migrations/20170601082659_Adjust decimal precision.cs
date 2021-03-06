﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ECERP.API.Migrations
{
    public partial class Adjustdecimalprecision : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "SalesPrice",
                table: "Products",
                type: "decimal(38, 28)",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<decimal>(
                name: "PurchasePrice",
                table: "Products",
                type: "decimal(38, 28)",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "LedgerTransactionLines",
                type: "decimal(38, 28)",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<decimal>(
                name: "BeginningBalance",
                table: "LedgerAccountBalances",
                type: "decimal(38, 28)",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<decimal>(
                name: "Balance9",
                table: "LedgerAccountBalances",
                type: "decimal(38, 28)",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<decimal>(
                name: "Balance8",
                table: "LedgerAccountBalances",
                type: "decimal(38, 28)",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<decimal>(
                name: "Balance7",
                table: "LedgerAccountBalances",
                type: "decimal(38, 28)",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<decimal>(
                name: "Balance6",
                table: "LedgerAccountBalances",
                type: "decimal(38, 28)",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<decimal>(
                name: "Balance5",
                table: "LedgerAccountBalances",
                type: "decimal(38, 28)",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<decimal>(
                name: "Balance4",
                table: "LedgerAccountBalances",
                type: "decimal(38, 28)",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<decimal>(
                name: "Balance3",
                table: "LedgerAccountBalances",
                type: "decimal(38, 28)",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<decimal>(
                name: "Balance2",
                table: "LedgerAccountBalances",
                type: "decimal(38, 28)",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<decimal>(
                name: "Balance12",
                table: "LedgerAccountBalances",
                type: "decimal(38, 28)",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<decimal>(
                name: "Balance11",
                table: "LedgerAccountBalances",
                type: "decimal(38, 28)",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<decimal>(
                name: "Balance10",
                table: "LedgerAccountBalances",
                type: "decimal(38, 28)",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<decimal>(
                name: "Balance1",
                table: "LedgerAccountBalances",
                type: "decimal(38, 28)",
                nullable: false,
                oldClrType: typeof(decimal));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "SalesPrice",
                table: "Products",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(38, 28)");

            migrationBuilder.AlterColumn<decimal>(
                name: "PurchasePrice",
                table: "Products",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(38, 28)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "LedgerTransactionLines",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(38, 28)");

            migrationBuilder.AlterColumn<decimal>(
                name: "BeginningBalance",
                table: "LedgerAccountBalances",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(38, 28)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Balance9",
                table: "LedgerAccountBalances",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(38, 28)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Balance8",
                table: "LedgerAccountBalances",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(38, 28)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Balance7",
                table: "LedgerAccountBalances",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(38, 28)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Balance6",
                table: "LedgerAccountBalances",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(38, 28)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Balance5",
                table: "LedgerAccountBalances",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(38, 28)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Balance4",
                table: "LedgerAccountBalances",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(38, 28)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Balance3",
                table: "LedgerAccountBalances",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(38, 28)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Balance2",
                table: "LedgerAccountBalances",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(38, 28)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Balance12",
                table: "LedgerAccountBalances",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(38, 28)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Balance11",
                table: "LedgerAccountBalances",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(38, 28)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Balance10",
                table: "LedgerAccountBalances",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(38, 28)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Balance1",
                table: "LedgerAccountBalances",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(38, 28)");
        }
    }
}
