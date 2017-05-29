using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ECERP.API.Migrations
{
    public partial class makeproductidunique : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductSupplier_Products_ProductId",
                table: "ProductSupplier");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductSupplier_Suppliers_SupplierId",
                table: "ProductSupplier");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductSupplier",
                table: "ProductSupplier");

            migrationBuilder.RenameTable(
                name: "ProductSupplier",
                newName: "ProductSuppliers");

            migrationBuilder.RenameIndex(
                name: "IX_ProductSupplier_ProductId_SupplierId",
                table: "ProductSuppliers",
                newName: "IX_ProductSuppliers_ProductId_SupplierId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductSupplier_SupplierId",
                table: "ProductSuppliers",
                newName: "IX_ProductSuppliers_SupplierId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductSuppliers",
                table: "ProductSuppliers",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductId",
                table: "Products",
                column: "ProductId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSuppliers_Products_ProductId",
                table: "ProductSuppliers",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSuppliers_Suppliers_SupplierId",
                table: "ProductSuppliers",
                column: "SupplierId",
                principalTable: "Suppliers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductSuppliers_Products_ProductId",
                table: "ProductSuppliers");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductSuppliers_Suppliers_SupplierId",
                table: "ProductSuppliers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductSuppliers",
                table: "ProductSuppliers");

            migrationBuilder.DropIndex(
                name: "IX_Products_ProductId",
                table: "Products");

            migrationBuilder.RenameTable(
                name: "ProductSuppliers",
                newName: "ProductSupplier");

            migrationBuilder.RenameIndex(
                name: "IX_ProductSuppliers_ProductId_SupplierId",
                table: "ProductSupplier",
                newName: "IX_ProductSupplier_ProductId_SupplierId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductSuppliers_SupplierId",
                table: "ProductSupplier",
                newName: "IX_ProductSupplier_SupplierId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductSupplier",
                table: "ProductSupplier",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSupplier_Products_ProductId",
                table: "ProductSupplier",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSupplier_Suppliers_SupplierId",
                table: "ProductSupplier",
                column: "SupplierId",
                principalTable: "Suppliers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
