﻿// <auto-generated />
using Concretar.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace Concretar.Entities.Migrations
{
    [DbContext(typeof(ConcretarContext))]
    [Migration("20190620193900_AlterTableVentaYCuota")]
    partial class AlterTableVentaYCuota
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Concretar.Entities.Cliente", b =>
                {
                    b.Property<int>("ClienteId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Apellido");

                    b.Property<string>("Correo");

                    b.Property<string>("DNI");

                    b.Property<string>("Domicilio");

                    b.Property<string>("Edad");

                    b.Property<DateTime>("FechaNacimiento");

                    b.Property<string>("Nombre");

                    b.Property<string>("NumeroDomicilio");

                    b.Property<string>("Observacion");

                    b.Property<DateTime>("TSCreado");

                    b.Property<DateTime?>("TSEliminado");

                    b.Property<DateTime?>("TSModificado");

                    b.Property<string>("Telefono");

                    b.HasKey("ClienteId");

                    b.ToTable("Cliente");
                });

            modelBuilder.Entity("Concretar.Entities.Cuota", b =>
                {
                    b.Property<int>("CuotaId")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Confirmado");

                    b.Property<DateTime>("FechaVencimiento");

                    b.Property<string>("MontoMora");

                    b.Property<bool>("Mora");

                    b.Property<int>("NumeroCuota");

                    b.Property<string>("Precio");

                    b.Property<string>("SubTotal");

                    b.Property<DateTime>("TSCreado");

                    b.Property<DateTime?>("TSEliminado");

                    b.Property<DateTime?>("TSModificado");

                    b.Property<string>("TotalPagado");

                    b.Property<int>("VentaId");

                    b.HasKey("CuotaId");

                    b.HasIndex("VentaId");

                    b.ToTable("Cuota");
                });

            modelBuilder.Entity("Concretar.Entities.Lote", b =>
                {
                    b.Property<int>("LoteId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Descripcion");

                    b.Property<string>("Dimension");

                    b.Property<string>("Nombre");

                    b.Property<string>("Precio");

                    b.Property<int>("ProyectId");

                    b.Property<DateTime>("TSCreado");

                    b.Property<DateTime?>("TSEliminado");

                    b.Property<DateTime?>("TSModificado");

                    b.Property<string>("Ubicacion");

                    b.HasKey("LoteId");

                    b.ToTable("Lote");
                });

            modelBuilder.Entity("Concretar.Entities.Pago", b =>
                {
                    b.Property<int>("PagoId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CuotaId");

                    b.Property<DateTime>("Fecha");

                    b.Property<string>("NumeroComprobante");

                    b.Property<DateTime>("TSCreado");

                    b.Property<DateTime?>("TSEliminado");

                    b.Property<DateTime?>("TSModificado");

                    b.HasKey("PagoId");

                    b.HasIndex("CuotaId")
                        .IsUnique();

                    b.ToTable("Pago");
                });

            modelBuilder.Entity("Concretar.Entities.Parametro", b =>
                {
                    b.Property<int>("ParametroId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Categoria");

                    b.Property<string>("Clave");

                    b.Property<string>("Descripcion");

                    b.Property<string>("Valor");

                    b.HasKey("ParametroId");

                    b.ToTable("Parametro");
                });

            modelBuilder.Entity("Concretar.Entities.Permiso", b =>
                {
                    b.Property<int>("PermisoId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Descripcion");

                    b.Property<string>("Funcionalidad");

                    b.Property<int>("VistaId");

                    b.HasKey("PermisoId");

                    b.HasIndex("VistaId");

                    b.ToTable("Permiso");
                });

            modelBuilder.Entity("Concretar.Entities.Proyecto", b =>
                {
                    b.Property<int>("ProyectoId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Descripcion");

                    b.Property<string>("Dimension");

                    b.Property<string>("Nombre");

                    b.Property<string>("Precio");

                    b.Property<DateTime>("TSCreado");

                    b.Property<DateTime?>("TSEliminado");

                    b.Property<DateTime?>("TSModificado");

                    b.Property<string>("Ubicacion");

                    b.HasKey("ProyectoId");

                    b.ToTable("Proyecto");
                });

            modelBuilder.Entity("Concretar.Entities.Reunion", b =>
                {
                    b.Property<int>("ReunionId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ClienteId");

                    b.Property<DateTime>("Fecha");

                    b.Property<string>("Motivo");

                    b.Property<string>("Resultado");

                    b.Property<int>("UsuarioId");

                    b.HasKey("ReunionId");

                    b.HasIndex("ClienteId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Reunion");
                });

            modelBuilder.Entity("Concretar.Entities.Rol", b =>
                {
                    b.Property<int>("RolId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Codigo");

                    b.Property<string>("Nombre");

                    b.HasKey("RolId");

                    b.ToTable("Rol");
                });

            modelBuilder.Entity("Concretar.Entities.RolPermiso", b =>
                {
                    b.Property<int>("RolId");

                    b.Property<int>("PermisoId");

                    b.HasKey("RolId", "PermisoId");

                    b.HasIndex("PermisoId");

                    b.ToTable("RolPermiso");
                });

            modelBuilder.Entity("Concretar.Entities.Usuario", b =>
                {
                    b.Property<int>("UsuarioId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Apellido");

                    b.Property<string>("Contrasena");

                    b.Property<bool>("ContrasenaActualizada");

                    b.Property<string>("Email");

                    b.Property<bool>("Habilitado");

                    b.Property<string>("Nombre");

                    b.Property<string>("PathImagenPerfil");

                    b.Property<DateTime>("TSCreado");

                    b.Property<DateTime?>("TSEliminado");

                    b.Property<DateTime?>("TSModificado");

                    b.HasKey("UsuarioId");

                    b.ToTable("Usuario");
                });

            modelBuilder.Entity("Concretar.Entities.UsuarioRol", b =>
                {
                    b.Property<int>("RolId");

                    b.Property<int>("UsuarioId");

                    b.HasKey("RolId", "UsuarioId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("UsuarioRol");
                });

            modelBuilder.Entity("Concretar.Entities.UsuarioToken", b =>
                {
                    b.Property<int>("UsuarioTokenId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("FechaExpiracion");

                    b.Property<string>("Token");

                    b.Property<bool>("Usado");

                    b.Property<int>("UsuarioId");

                    b.HasKey("UsuarioTokenId");

                    b.ToTable("UsuarioToken");
                });

            modelBuilder.Entity("Concretar.Entities.Venta", b =>
                {
                    b.Property<int>("VentaId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Anticipo");

                    b.Property<int>("CantidadCuotas");

                    b.Property<int>("ClienteId");

                    b.Property<DateTime>("Fecha");

                    b.Property<string>("Interes");

                    b.Property<int?>("LoteId");

                    b.Property<int?>("ProyectoId");

                    b.Property<DateTime>("TSCreado");

                    b.Property<DateTime?>("TSEliminado");

                    b.Property<DateTime?>("TSModificado");

                    b.HasKey("VentaId");

                    b.HasIndex("ClienteId")
                        .IsUnique();

                    b.HasIndex("LoteId")
                        .IsUnique()
                        .HasFilter("[LoteId] IS NOT NULL");

                    b.HasIndex("ProyectoId")
                        .IsUnique()
                        .HasFilter("[ProyectoId] IS NOT NULL");

                    b.ToTable("Venta");
                });

            modelBuilder.Entity("Concretar.Entities.Vista", b =>
                {
                    b.Property<int>("VistaId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Descripcion");

                    b.Property<string>("Nombre");

                    b.HasKey("VistaId");

                    b.ToTable("Vista");
                });

            modelBuilder.Entity("Concretar.Entities.Cuota", b =>
                {
                    b.HasOne("Concretar.Entities.Venta", "Venta")
                        .WithMany("Cuota")
                        .HasForeignKey("VentaId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Concretar.Entities.Pago", b =>
                {
                    b.HasOne("Concretar.Entities.Cuota", "Cuota")
                        .WithOne("Pago")
                        .HasForeignKey("Concretar.Entities.Pago", "CuotaId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Concretar.Entities.Permiso", b =>
                {
                    b.HasOne("Concretar.Entities.Vista", "Vista")
                        .WithMany("Permisos")
                        .HasForeignKey("VistaId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Concretar.Entities.Reunion", b =>
                {
                    b.HasOne("Concretar.Entities.Cliente", "Cliente")
                        .WithMany()
                        .HasForeignKey("ClienteId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Concretar.Entities.Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Concretar.Entities.RolPermiso", b =>
                {
                    b.HasOne("Concretar.Entities.Permiso", "Permiso")
                        .WithMany("RolPermisos")
                        .HasForeignKey("PermisoId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Concretar.Entities.Rol", "Rol")
                        .WithMany("RolPermisos")
                        .HasForeignKey("RolId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Concretar.Entities.UsuarioRol", b =>
                {
                    b.HasOne("Concretar.Entities.Rol", "Rol")
                        .WithMany("UsuarioRoles")
                        .HasForeignKey("RolId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Concretar.Entities.Usuario", "Usuario")
                        .WithMany("UsuarioRoles")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Concretar.Entities.Venta", b =>
                {
                    b.HasOne("Concretar.Entities.Cliente", "Cliente")
                        .WithOne("Venta")
                        .HasForeignKey("Concretar.Entities.Venta", "ClienteId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Concretar.Entities.Lote", "Lote")
                        .WithOne("Venta")
                        .HasForeignKey("Concretar.Entities.Venta", "LoteId");

                    b.HasOne("Concretar.Entities.Proyecto", "Proyecto")
                        .WithOne("Venta")
                        .HasForeignKey("Concretar.Entities.Venta", "ProyectoId");
                });
#pragma warning restore 612, 618
        }
    }
}
