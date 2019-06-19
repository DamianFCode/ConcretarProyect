using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Concretar.Entities.Migrations
{
    public partial class AlterColumnLoteIdProyectoIdAddColumnAnticipoToVenta : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Venta_Lote_LoteId",
                table: "Venta");

            migrationBuilder.DropForeignKey(
                name: "FK_Venta_Proyecto_ProyectoId",
                table: "Venta");

            migrationBuilder.DropIndex(
                name: "IX_Venta_LoteId",
                table: "Venta");

            migrationBuilder.DropIndex(
                name: "IX_Venta_ProyectoId",
                table: "Venta");

            migrationBuilder.AlterColumn<int>(
                name: "ProyectoId",
                table: "Venta",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "LoteId",
                table: "Venta",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<string>(
                name: "Anticipo",
                table: "Venta",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Venta_LoteId",
                table: "Venta",
                column: "LoteId",
                unique: true,
                filter: "[LoteId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Venta_ProyectoId",
                table: "Venta",
                column: "ProyectoId",
                unique: true,
                filter: "[ProyectoId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Venta_Lote_LoteId",
                table: "Venta",
                column: "LoteId",
                principalTable: "Lote",
                principalColumn: "LoteId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Venta_Proyecto_ProyectoId",
                table: "Venta",
                column: "ProyectoId",
                principalTable: "Proyecto",
                principalColumn: "ProyectoId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Venta_Lote_LoteId",
                table: "Venta");

            migrationBuilder.DropForeignKey(
                name: "FK_Venta_Proyecto_ProyectoId",
                table: "Venta");

            migrationBuilder.DropIndex(
                name: "IX_Venta_LoteId",
                table: "Venta");

            migrationBuilder.DropIndex(
                name: "IX_Venta_ProyectoId",
                table: "Venta");

            migrationBuilder.DropColumn(
                name: "Anticipo",
                table: "Venta");

            migrationBuilder.AlterColumn<int>(
                name: "ProyectoId",
                table: "Venta",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "LoteId",
                table: "Venta",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Venta_LoteId",
                table: "Venta",
                column: "LoteId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Venta_ProyectoId",
                table: "Venta",
                column: "ProyectoId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Venta_Lote_LoteId",
                table: "Venta",
                column: "LoteId",
                principalTable: "Lote",
                principalColumn: "LoteId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Venta_Proyecto_ProyectoId",
                table: "Venta",
                column: "ProyectoId",
                principalTable: "Proyecto",
                principalColumn: "ProyectoId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
