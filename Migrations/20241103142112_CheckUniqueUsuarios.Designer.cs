﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Sistema_ArgenMotos.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20241103142112_CheckUniqueUsuarios")]
    partial class CheckUniqueUsuarios
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Sistema_ArgenMotos.Entidades.Articulo", b =>
                {
                    b.Property<int>("ArticuloId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ArticuloId"));

                    b.Property<string>("Anno")
                        .IsRequired()
                        .HasMaxLength(4)
                        .HasColumnType("character varying(4)");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<string>("Marca")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Modelo")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<decimal>("Precio")
                        .HasColumnType("numeric");

                    b.Property<int>("StockActual")
                        .HasColumnType("integer");

                    b.Property<int>("StockMaximo")
                        .HasColumnType("integer");

                    b.Property<int>("StockMinimo")
                        .HasColumnType("integer");

                    b.HasKey("ArticuloId");

                    b.ToTable("Articulos");
                });

            modelBuilder.Entity("Sistema_ArgenMotos.Entidades.Cliente", b =>
                {
                    b.Property<int>("ClienteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ClienteId"));

                    b.Property<string>("Apellido")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("DNI")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)");

                    b.Property<string>("Domicilio")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Estado")
                        .HasColumnType("integer");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Telefono")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Tipo")
                        .HasColumnType("integer");

                    b.HasKey("ClienteId");

                    b.ToTable("Clientes");
                });

            modelBuilder.Entity("Sistema_ArgenMotos.Entidades.Cobranza", b =>
                {
                    b.Property<int>("CobranzaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("CobranzaId"));

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("MetodoPago")
                        .HasColumnType("integer");

                    b.Property<decimal>("MontoTotal")
                        .HasColumnType("numeric");

                    b.Property<int>("VentaId")
                        .HasColumnType("integer");

                    b.HasKey("CobranzaId");

                    b.HasIndex("VentaId");

                    b.ToTable("Cobranzas");
                });

            modelBuilder.Entity("Sistema_ArgenMotos.Entidades.Factura", b =>
                {
                    b.Property<int>("FacturaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("FacturaId"));

                    b.Property<int>("ClienteId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("timestamp with time zone");

                    b.Property<decimal>("PrecioFinal")
                        .HasColumnType("numeric");

                    b.Property<int>("VendedorId")
                        .HasColumnType("integer");

                    b.Property<int>("VentaId")
                        .HasColumnType("integer");

                    b.HasKey("FacturaId");

                    b.HasIndex("ClienteId");

                    b.HasIndex("VendedorId");

                    b.HasIndex("VentaId")
                        .IsUnique();

                    b.ToTable("Facturas");
                });

            modelBuilder.Entity("Sistema_ArgenMotos.Entidades.Factura_Articulo", b =>
                {
                    b.Property<int>("FacturaId")
                        .HasColumnType("integer");

                    b.Property<int>("ArticuloId")
                        .HasColumnType("integer");

                    b.Property<int>("Cantidad")
                        .HasColumnType("integer");

                    b.Property<decimal>("PrecioUnitario")
                        .HasColumnType("numeric");

                    b.HasKey("FacturaId", "ArticuloId");

                    b.HasIndex("ArticuloId");

                    b.ToTable("Factura_Articulos");
                });

            modelBuilder.Entity("Sistema_ArgenMotos.Entidades.NotaCredito", b =>
                {
                    b.Property<int>("NotaCreditoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("NotaCreditoId"));

                    b.Property<int>("ClienteId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("timestamp with time zone");

                    b.Property<decimal>("Importe")
                        .HasColumnType("numeric");

                    b.Property<int>("VendedorId")
                        .HasColumnType("integer");

                    b.Property<int>("VentaId")
                        .HasColumnType("integer");

                    b.HasKey("NotaCreditoId");

                    b.HasIndex("ClienteId");

                    b.HasIndex("VendedorId");

                    b.HasIndex("VentaId");

                    b.ToTable("NotasCredito");
                });

            modelBuilder.Entity("Sistema_ArgenMotos.Entidades.NotaCredito_Articulo", b =>
                {
                    b.Property<int>("NotaCreditoId")
                        .HasColumnType("integer");

                    b.Property<int>("ArticuloId")
                        .HasColumnType("integer");

                    b.Property<int>("Cantidad")
                        .HasColumnType("integer");

                    b.Property<decimal>("PrecioUnitario")
                        .HasColumnType("numeric");

                    b.HasKey("NotaCreditoId", "ArticuloId");

                    b.HasIndex("ArticuloId");

                    b.ToTable("NotaCredito_Articulos");
                });

            modelBuilder.Entity("Sistema_ArgenMotos.Entidades.NotaDebito", b =>
                {
                    b.Property<int>("NotaDebitoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("NotaDebitoId"));

                    b.Property<int>("ClienteId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("timestamp with time zone");

                    b.Property<decimal>("Importe")
                        .HasColumnType("numeric");

                    b.Property<int>("VendedorId")
                        .HasColumnType("integer");

                    b.Property<int>("VentaId")
                        .HasColumnType("integer");

                    b.HasKey("NotaDebitoId");

                    b.HasIndex("ClienteId");

                    b.HasIndex("VendedorId");

                    b.HasIndex("VentaId");

                    b.ToTable("NotasDebito");
                });

            modelBuilder.Entity("Sistema_ArgenMotos.Entidades.NotaDebito_Articulo", b =>
                {
                    b.Property<int>("NotaDebitoId")
                        .HasColumnType("integer");

                    b.Property<int>("ArticuloId")
                        .HasColumnType("integer");

                    b.Property<int>("Cantidad")
                        .HasColumnType("integer");

                    b.Property<decimal>("PrecioUnitario")
                        .HasColumnType("numeric");

                    b.HasKey("NotaDebitoId", "ArticuloId");

                    b.HasIndex("ArticuloId");

                    b.ToTable("NotaDebito_Articulos");
                });

            modelBuilder.Entity("Sistema_ArgenMotos.Entidades.OrdenDeCompra", b =>
                {
                    b.Property<int>("OrdenDeCompraId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("OrdenDeCompraId"));

                    b.Property<int>("Estado")
                        .HasColumnType("integer");

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("timestamp with time zone");

                    b.Property<decimal>("PrecioTotal")
                        .HasColumnType("numeric");

                    b.Property<int>("ProveedorId")
                        .HasColumnType("integer");

                    b.HasKey("OrdenDeCompraId");

                    b.HasIndex("ProveedorId");

                    b.ToTable("OrdenesDeCompra");
                });

            modelBuilder.Entity("Sistema_ArgenMotos.Entidades.OrdenDeCompra_Articulo", b =>
                {
                    b.Property<int>("OrdenDeCompraId")
                        .HasColumnType("integer");

                    b.Property<int>("ArticuloId")
                        .HasColumnType("integer");

                    b.Property<int>("Cantidad")
                        .HasColumnType("integer");

                    b.Property<decimal>("PrecioUnitario")
                        .HasColumnType("numeric");

                    b.HasKey("OrdenDeCompraId", "ArticuloId");

                    b.HasIndex("ArticuloId");

                    b.ToTable("OrdenDeCompra_Articulos");
                });

            modelBuilder.Entity("Sistema_ArgenMotos.Entidades.Proveedor", b =>
                {
                    b.Property<int>("ProveedorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ProveedorId"));

                    b.Property<string>("CUIL")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("character varying(13)");

                    b.Property<string>("Domicilio")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Estado")
                        .HasColumnType("integer");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("RazonSocial")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Telefono")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("ProveedorId");

                    b.ToTable("Proveedores");
                });

            modelBuilder.Entity("Sistema_ArgenMotos.Entidades.Usuario", b =>
                {
                    b.Property<int>("UsuarioId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("UsuarioId"));

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.Property<int>("VendedorId")
                        .HasColumnType("integer");

                    b.HasKey("UsuarioId");

                    b.HasIndex("VendedorId")
                        .IsUnique();

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("Sistema_ArgenMotos.Entidades.Vendedor", b =>
                {
                    b.Property<int>("VendedorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("VendedorId"));

                    b.Property<string>("Apellido")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("DNI")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<int>("Estado")
                        .HasMaxLength(20)
                        .HasColumnType("integer");

                    b.Property<string>("Legajo")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("Telefono")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("character varying(15)");

                    b.HasKey("VendedorId");

                    b.ToTable("Vendedores");

                    b.HasData(
                        new
                        {
                            VendedorId = 1,
                            Apellido = "Admin",
                            DNI = "12345678",
                            Email = "admin@argenmotos.com",
                            Estado = 0,
                            Legajo = "V001",
                            Nombre = "Admin",
                            Telefono = "123456789"
                        });
                });

            modelBuilder.Entity("Sistema_ArgenMotos.Entidades.Venta", b =>
                {
                    b.Property<int>("VentaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("VentaId"));

                    b.Property<int>("ClienteId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("timestamp with time zone");

                    b.Property<decimal>("PrecioFinal")
                        .HasColumnType("numeric");

                    b.Property<int>("VendedorId")
                        .HasColumnType("integer");

                    b.HasKey("VentaId");

                    b.HasIndex("ClienteId");

                    b.HasIndex("VendedorId");

                    b.ToTable("Ventas");
                });

            modelBuilder.Entity("Sistema_ArgenMotos.Entidades.Venta_Articulo", b =>
                {
                    b.Property<int>("VentaId")
                        .HasColumnType("integer");

                    b.Property<int>("ArticuloId")
                        .HasColumnType("integer");

                    b.Property<int>("Cantidad")
                        .HasColumnType("integer");

                    b.Property<decimal>("PrecioUnitario")
                        .HasColumnType("numeric");

                    b.HasKey("VentaId", "ArticuloId");

                    b.HasIndex("ArticuloId");

                    b.ToTable("Venta_Articulos");
                });

            modelBuilder.Entity("Sistema_ArgenMotos.Entidades.Cobranza", b =>
                {
                    b.HasOne("Sistema_ArgenMotos.Entidades.Venta", "Venta")
                        .WithMany()
                        .HasForeignKey("VentaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Venta");
                });

            modelBuilder.Entity("Sistema_ArgenMotos.Entidades.Factura", b =>
                {
                    b.HasOne("Sistema_ArgenMotos.Entidades.Cliente", "Cliente")
                        .WithMany()
                        .HasForeignKey("ClienteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Sistema_ArgenMotos.Entidades.Vendedor", "Vendedor")
                        .WithMany()
                        .HasForeignKey("VendedorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Sistema_ArgenMotos.Entidades.Venta", "Venta")
                        .WithOne("Factura")
                        .HasForeignKey("Sistema_ArgenMotos.Entidades.Factura", "VentaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cliente");

                    b.Navigation("Vendedor");

                    b.Navigation("Venta");
                });

            modelBuilder.Entity("Sistema_ArgenMotos.Entidades.Factura_Articulo", b =>
                {
                    b.HasOne("Sistema_ArgenMotos.Entidades.Articulo", "Articulo")
                        .WithMany("Factura_Articulos")
                        .HasForeignKey("ArticuloId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Sistema_ArgenMotos.Entidades.Factura", "Factura")
                        .WithMany("Articulos")
                        .HasForeignKey("FacturaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Articulo");

                    b.Navigation("Factura");
                });

            modelBuilder.Entity("Sistema_ArgenMotos.Entidades.NotaCredito", b =>
                {
                    b.HasOne("Sistema_ArgenMotos.Entidades.Cliente", "Cliente")
                        .WithMany()
                        .HasForeignKey("ClienteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Sistema_ArgenMotos.Entidades.Vendedor", "Vendedor")
                        .WithMany()
                        .HasForeignKey("VendedorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Sistema_ArgenMotos.Entidades.Venta", "Venta")
                        .WithMany("NotasCredito")
                        .HasForeignKey("VentaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cliente");

                    b.Navigation("Vendedor");

                    b.Navigation("Venta");
                });

            modelBuilder.Entity("Sistema_ArgenMotos.Entidades.NotaCredito_Articulo", b =>
                {
                    b.HasOne("Sistema_ArgenMotos.Entidades.Articulo", "Articulo")
                        .WithMany("NotaCredito_Articulos")
                        .HasForeignKey("ArticuloId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Sistema_ArgenMotos.Entidades.NotaCredito", "NotaCredito")
                        .WithMany("Articulos")
                        .HasForeignKey("NotaCreditoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Articulo");

                    b.Navigation("NotaCredito");
                });

            modelBuilder.Entity("Sistema_ArgenMotos.Entidades.NotaDebito", b =>
                {
                    b.HasOne("Sistema_ArgenMotos.Entidades.Cliente", "Cliente")
                        .WithMany()
                        .HasForeignKey("ClienteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Sistema_ArgenMotos.Entidades.Vendedor", "Vendedor")
                        .WithMany()
                        .HasForeignKey("VendedorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Sistema_ArgenMotos.Entidades.Venta", "Venta")
                        .WithMany("NotasDebito")
                        .HasForeignKey("VentaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cliente");

                    b.Navigation("Vendedor");

                    b.Navigation("Venta");
                });

            modelBuilder.Entity("Sistema_ArgenMotos.Entidades.NotaDebito_Articulo", b =>
                {
                    b.HasOne("Sistema_ArgenMotos.Entidades.Articulo", "Articulo")
                        .WithMany("NotaDebito_Articulos")
                        .HasForeignKey("ArticuloId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Sistema_ArgenMotos.Entidades.NotaDebito", "NotaDebito")
                        .WithMany("Articulos")
                        .HasForeignKey("NotaDebitoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Articulo");

                    b.Navigation("NotaDebito");
                });

            modelBuilder.Entity("Sistema_ArgenMotos.Entidades.OrdenDeCompra", b =>
                {
                    b.HasOne("Sistema_ArgenMotos.Entidades.Proveedor", "Proveedor")
                        .WithMany()
                        .HasForeignKey("ProveedorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Proveedor");
                });

            modelBuilder.Entity("Sistema_ArgenMotos.Entidades.OrdenDeCompra_Articulo", b =>
                {
                    b.HasOne("Sistema_ArgenMotos.Entidades.Articulo", "Articulo")
                        .WithMany("OrdenDeCompra_Articulos")
                        .HasForeignKey("ArticuloId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Sistema_ArgenMotos.Entidades.OrdenDeCompra", "OrdenDeCompra")
                        .WithMany("Articulos")
                        .HasForeignKey("OrdenDeCompraId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Articulo");

                    b.Navigation("OrdenDeCompra");
                });

            modelBuilder.Entity("Sistema_ArgenMotos.Entidades.Usuario", b =>
                {
                    b.HasOne("Sistema_ArgenMotos.Entidades.Vendedor", "Vendedor")
                        .WithMany()
                        .HasForeignKey("VendedorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Vendedor");
                });

            modelBuilder.Entity("Sistema_ArgenMotos.Entidades.Venta", b =>
                {
                    b.HasOne("Sistema_ArgenMotos.Entidades.Cliente", "Cliente")
                        .WithMany()
                        .HasForeignKey("ClienteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Sistema_ArgenMotos.Entidades.Vendedor", "Vendedor")
                        .WithMany()
                        .HasForeignKey("VendedorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cliente");

                    b.Navigation("Vendedor");
                });

            modelBuilder.Entity("Sistema_ArgenMotos.Entidades.Venta_Articulo", b =>
                {
                    b.HasOne("Sistema_ArgenMotos.Entidades.Articulo", "Articulo")
                        .WithMany("Venta_Articulos")
                        .HasForeignKey("ArticuloId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Sistema_ArgenMotos.Entidades.Venta", "Venta")
                        .WithMany("Articulos")
                        .HasForeignKey("ArticuloId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Articulo");

                    b.Navigation("Venta");
                });

            modelBuilder.Entity("Sistema_ArgenMotos.Entidades.Articulo", b =>
                {
                    b.Navigation("Factura_Articulos");

                    b.Navigation("NotaCredito_Articulos");

                    b.Navigation("NotaDebito_Articulos");

                    b.Navigation("OrdenDeCompra_Articulos");

                    b.Navigation("Venta_Articulos");
                });

            modelBuilder.Entity("Sistema_ArgenMotos.Entidades.Factura", b =>
                {
                    b.Navigation("Articulos");
                });

            modelBuilder.Entity("Sistema_ArgenMotos.Entidades.NotaCredito", b =>
                {
                    b.Navigation("Articulos");
                });

            modelBuilder.Entity("Sistema_ArgenMotos.Entidades.NotaDebito", b =>
                {
                    b.Navigation("Articulos");
                });

            modelBuilder.Entity("Sistema_ArgenMotos.Entidades.OrdenDeCompra", b =>
                {
                    b.Navigation("Articulos");
                });

            modelBuilder.Entity("Sistema_ArgenMotos.Entidades.Venta", b =>
                {
                    b.Navigation("Articulos");

                    b.Navigation("Factura")
                        .IsRequired();

                    b.Navigation("NotasCredito");

                    b.Navigation("NotasDebito");
                });
#pragma warning restore 612, 618
        }
    }
}
