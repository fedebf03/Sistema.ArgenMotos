using Microsoft.EntityFrameworkCore;
using Sistema_ArgenMotos.Entidades;
using Sistema_ArgenMotos.Enums;
using Sistema_ArgenMotos.Services;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Proveedor> Proveedores { get; set; }
    public DbSet<Articulo> Articulos { get; set; }
    public DbSet<OrdenDeCompra> OrdenesDeCompra { get; set; }
    public DbSet<OrdenDeCompra_Articulo> OrdenDeCompra_Articulos { get; set; }
    public DbSet<Vendedor> Vendedores { get; set; }
    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Venta> Ventas { get; set; }
    public DbSet<Venta_Articulo> Venta_Articulos { get; set; }
    public DbSet<Factura> Facturas { get; set; }
    public DbSet<Factura_Articulo> Factura_Articulos { get; set; }
    public DbSet<NotaDebito> NotasDebito { get; set; }
    public DbSet<NotaDebito_Articulo> NotaDebito_Articulos { get; set; }
    public DbSet<NotaCredito> NotasCredito { get; set; }
    public DbSet<NotaCredito_Articulo> NotaCredito_Articulos { get; set; }
    public DbSet<Cobranza> Cobranzas { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }

    // Método para configurar el modelo
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<OrdenDeCompra_Articulo>()
            .HasKey(oc => new { oc.OrdenDeCompraId, oc.ArticuloId });

        modelBuilder.Entity<OrdenDeCompra_Articulo>()
            .HasOne(oc => oc.OrdenDeCompra)
            .WithMany(o => o.Articulos)
            .HasForeignKey(oc => oc.OrdenDeCompraId);

        modelBuilder.Entity<OrdenDeCompra_Articulo>()
            .HasOne(oc => oc.Articulo)
            .WithMany(a => a.OrdenDeCompra_Articulos)
            .HasForeignKey(oc => oc.ArticuloId);

        modelBuilder.Entity<Venta_Articulo>()
            .HasKey(va => new { va.VentaId, va.ArticuloId });

        modelBuilder.Entity<Venta_Articulo>()
            .HasOne(va => va.Venta)
            .WithMany(v => v.Articulos)
            .HasForeignKey(va => va.ArticuloId);

        modelBuilder.Entity<Venta_Articulo>()
            .HasOne(va => va.Articulo)
            .WithMany(v => v.Venta_Articulos)
            .HasForeignKey(va => va.ArticuloId);

        modelBuilder.Entity<Factura_Articulo>()
            .HasKey(fa => new { fa.FacturaId, fa.ArticuloId });

        modelBuilder.Entity<Factura_Articulo>()
            .HasOne(fa => fa.Factura)
            .WithMany(f => f.Articulos)
            .HasForeignKey(fa => fa.FacturaId);

        modelBuilder.Entity<Factura_Articulo>()
            .HasOne(fa => fa.Articulo)
            .WithMany(a => a.Factura_Articulos)
            .HasForeignKey(fa => fa.ArticuloId);

        modelBuilder.Entity<NotaDebito_Articulo>()
            .HasKey(nda => new { nda.NotaDebitoId, nda.ArticuloId });

        modelBuilder.Entity<NotaDebito_Articulo>()
            .HasOne(nda => nda.NotaDebito)
            .WithMany(nd => nd.Articulos)
            .HasForeignKey(nda => nda.NotaDebitoId);

        modelBuilder.Entity<NotaDebito_Articulo>()
            .HasOne(nda => nda.Articulo)
            .WithMany(a => a.NotaDebito_Articulos)
            .HasForeignKey(nda => nda.ArticuloId);

        modelBuilder.Entity<NotaCredito_Articulo>()
            .HasKey(nca => new { nca.NotaCreditoId, nca.ArticuloId });

        modelBuilder.Entity<NotaCredito_Articulo>()
            .HasOne(nca => nca.NotaCredito)
            .WithMany(nc => nc.Articulos)
            .HasForeignKey(nca => nca.NotaCreditoId);

        modelBuilder.Entity<NotaCredito_Articulo>()
            .HasOne(nca => nca.Articulo)
            .WithMany(a => a.NotaCredito_Articulos)
            .HasForeignKey(nca => nca.ArticuloId);

        modelBuilder.Entity<Usuario>()
            .HasIndex(u => u.VendedorId)
            .IsUnique();

        modelBuilder.Entity<Vendedor>().HasData(new Vendedor
        {
            VendedorId = 1,
            DNI = "12345678",
            Legajo = "V001",
            Nombre = "Admin",
            Apellido = "Admin",
            Telefono = "123456789",
            Email = "admin@argenmotos.com",
            Estado = EstadoVendedor.Activo
        });
    }
}
