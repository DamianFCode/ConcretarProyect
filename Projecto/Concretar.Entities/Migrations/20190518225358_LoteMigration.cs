using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Concretar.Entities.Migrations
{
    public partial class LoteMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Lote",
                newName: "Ubicacion");

            migrationBuilder.RenameColumn(
                name: "Medida",
                table: "Lote",
                newName: "Nombre");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Lote",
                newName: "LoteId");

            migrationBuilder.AlterColumn<string>(
                name: "Precio",
                table: "Proyecto",
                nullable: true,
                oldClrType: typeof(float));

            migrationBuilder.AlterColumn<string>(
                name: "Precio",
                table: "Lote",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<string>(
                name: "Dimension",
                table: "Lote",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProyectId",
                table: "Lote",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Dimension",
                table: "Lote");

            migrationBuilder.DropColumn(
                name: "ProyectId",
                table: "Lote");

            migrationBuilder.RenameColumn(
                name: "Ubicacion",
                table: "Lote",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "Nombre",
                table: "Lote",
                newName: "Medida");

            migrationBuilder.RenameColumn(
                name: "LoteId",
                table: "Lote",
                newName: "Id");

            migrationBuilder.AlterColumn<float>(
                name: "Precio",
                table: "Proyecto",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Precio",
                table: "Lote",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
