using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Concretar.Entities.Migrations
{
    public partial class AlterTableCuotaPagoVenta : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Confirmado",
                table: "Cuota");

            migrationBuilder.RenameColumn(
                name: "TSCreado",
                table: "Venta",
                newName: "TSCreate");

            migrationBuilder.RenameColumn(
                name: "TSCreado",
                table: "Pago",
                newName: "TSCreate");

            migrationBuilder.RenameColumn(
                name: "TotalPagado",
                table: "Cuota",
                newName: "Estado");

            migrationBuilder.RenameColumn(
                name: "TSCreado",
                table: "Cuota",
                newName: "TSCreate");

            migrationBuilder.AddColumn<string>(
                name: "TotalPagado",
                table: "Pago",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalPagado",
                table: "Pago");

            migrationBuilder.RenameColumn(
                name: "TSCreate",
                table: "Venta",
                newName: "TSCreado");

            migrationBuilder.RenameColumn(
                name: "TSCreate",
                table: "Pago",
                newName: "TSCreado");

            migrationBuilder.RenameColumn(
                name: "TSCreate",
                table: "Cuota",
                newName: "TSCreado");

            migrationBuilder.RenameColumn(
                name: "Estado",
                table: "Cuota",
                newName: "TotalPagado");

            migrationBuilder.AddColumn<bool>(
                name: "Confirmado",
                table: "Cuota",
                nullable: false,
                defaultValue: false);
        }
    }
}
