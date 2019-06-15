using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Concretar.Entities.Migrations
{
    public partial class AddTableCuotaVentaPago : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "TSCreado",
                table: "Proyecto",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "TSEliminado",
                table: "Proyecto",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "TSModificado",
                table: "Proyecto",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "TSCreado",
                table: "Lote",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "TSEliminado",
                table: "Lote",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "TSModificado",
                table: "Lote",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "TSCreado",
                table: "Cliente",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "TSEliminado",
                table: "Cliente",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "TSModificado",
                table: "Cliente",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Cuota",
                columns: table => new
                {
                    CuotaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Confirmado = table.Column<bool>(nullable: false),
                    MontoMora = table.Column<string>(nullable: true),
                    Mora = table.Column<bool>(nullable: false),
                    NumeroCuota = table.Column<int>(nullable: false),
                    Precio = table.Column<string>(nullable: true),
                    SubTotal = table.Column<string>(nullable: true),
                    TSCreado = table.Column<DateTime>(nullable: false),
                    TSEliminado = table.Column<DateTime>(nullable: true),
                    TSModificado = table.Column<DateTime>(nullable: true),
                    TotalPagado = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cuota", x => x.CuotaId);
                });

            migrationBuilder.CreateTable(
                name: "Pago",
                columns: table => new
                {
                    PagoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CuotaId = table.Column<int>(nullable: false),
                    Fecha = table.Column<DateTime>(nullable: false),
                    NumeroComprobante = table.Column<string>(nullable: true),
                    TSCreado = table.Column<DateTime>(nullable: false),
                    TSEliminado = table.Column<DateTime>(nullable: true),
                    TSModificado = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pago", x => x.PagoId);
                    table.ForeignKey(
                        name: "FK_Pago_Cuota_CuotaId",
                        column: x => x.CuotaId,
                        principalTable: "Cuota",
                        principalColumn: "CuotaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Venta",
                columns: table => new
                {
                    VentaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CantidadCuotas = table.Column<int>(nullable: false),
                    ClienteId = table.Column<int>(nullable: false),
                    CuotaId = table.Column<int>(nullable: false),
                    Fecha = table.Column<DateTime>(nullable: false),
                    Interes = table.Column<string>(nullable: true),
                    LoteId = table.Column<int>(nullable: false),
                    ProyectoId = table.Column<int>(nullable: false),
                    TSCreado = table.Column<DateTime>(nullable: false),
                    TSEliminado = table.Column<DateTime>(nullable: true),
                    TSModificado = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Venta", x => x.VentaId);
                    table.ForeignKey(
                        name: "FK_Venta_Cliente_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Cliente",
                        principalColumn: "ClienteId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Venta_Cuota_CuotaId",
                        column: x => x.CuotaId,
                        principalTable: "Cuota",
                        principalColumn: "CuotaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Venta_Lote_LoteId",
                        column: x => x.LoteId,
                        principalTable: "Lote",
                        principalColumn: "LoteId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Venta_Proyecto_ProyectoId",
                        column: x => x.ProyectoId,
                        principalTable: "Proyecto",
                        principalColumn: "ProyectoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pago_CuotaId",
                table: "Pago",
                column: "CuotaId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Venta_ClienteId",
                table: "Venta",
                column: "ClienteId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Venta_CuotaId",
                table: "Venta",
                column: "CuotaId",
                unique: true);

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pago");

            migrationBuilder.DropTable(
                name: "Venta");

            migrationBuilder.DropTable(
                name: "Cuota");

            migrationBuilder.DropColumn(
                name: "TSCreado",
                table: "Proyecto");

            migrationBuilder.DropColumn(
                name: "TSEliminado",
                table: "Proyecto");

            migrationBuilder.DropColumn(
                name: "TSModificado",
                table: "Proyecto");

            migrationBuilder.DropColumn(
                name: "TSCreado",
                table: "Lote");

            migrationBuilder.DropColumn(
                name: "TSEliminado",
                table: "Lote");

            migrationBuilder.DropColumn(
                name: "TSModificado",
                table: "Lote");

            migrationBuilder.DropColumn(
                name: "TSCreado",
                table: "Cliente");

            migrationBuilder.DropColumn(
                name: "TSEliminado",
                table: "Cliente");

            migrationBuilder.DropColumn(
                name: "TSModificado",
                table: "Cliente");
        }
    }
}
