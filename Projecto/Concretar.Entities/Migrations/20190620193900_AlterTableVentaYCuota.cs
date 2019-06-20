using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Concretar.Entities.Migrations
{
    public partial class AlterTableVentaYCuota : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Venta_Cuota_CuotaId",
                table: "Venta");

            migrationBuilder.DropIndex(
                name: "IX_Venta_CuotaId",
                table: "Venta");

            migrationBuilder.DropColumn(
                name: "CuotaId",
                table: "Venta");

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaVencimiento",
                table: "Cuota",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "VentaId",
                table: "Cuota",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Cuota_VentaId",
                table: "Cuota",
                column: "VentaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cuota_Venta_VentaId",
                table: "Cuota",
                column: "VentaId",
                principalTable: "Venta",
                principalColumn: "VentaId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cuota_Venta_VentaId",
                table: "Cuota");

            migrationBuilder.DropIndex(
                name: "IX_Cuota_VentaId",
                table: "Cuota");

            migrationBuilder.DropColumn(
                name: "FechaVencimiento",
                table: "Cuota");

            migrationBuilder.DropColumn(
                name: "VentaId",
                table: "Cuota");

            migrationBuilder.AddColumn<int>(
                name: "CuotaId",
                table: "Venta",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Venta_CuotaId",
                table: "Venta",
                column: "CuotaId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Venta_Cuota_CuotaId",
                table: "Venta",
                column: "CuotaId",
                principalTable: "Cuota",
                principalColumn: "CuotaId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
