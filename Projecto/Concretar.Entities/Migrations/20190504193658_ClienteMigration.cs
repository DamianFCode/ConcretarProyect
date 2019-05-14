using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Concretar.Entities.Migrations
{
    public partial class ClienteMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cliente",
                columns: table => new
                {
                    ClienteId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Apellido = table.Column<string>(nullable: true),
                    Correo = table.Column<string>(nullable: true),
                    Domicilio = table.Column<string>(nullable: true),
                    Edad = table.Column<int>(nullable: false),
                    FechaNacimiento = table.Column<DateTime>(nullable: false),
                    Nombre = table.Column<string>(nullable: true),
                    NumeroDomicilio = table.Column<int>(nullable: false),
                    Observacion = table.Column<string>(nullable: true),
                    Telefono = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cliente", x => x.ClienteId);
                });

            migrationBuilder.CreateTable(
                name: "Lote",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Descripcion = table.Column<string>(nullable: true),
                    Medida = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Precio = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lote", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cliente");

            migrationBuilder.DropTable(
                name: "Lote");
        }
    }
}
