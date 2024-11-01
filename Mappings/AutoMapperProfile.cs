using AutoMapper;
using Sistema_ArgenMotos.DTOs;
using Sistema_ArgenMotos.Entidades;

namespace Sistema_ArgenMotos.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Mapeo para Articulo
            CreateMap<Articulo, ArticuloDTO>().ReverseMap();
            CreateMap<ArticuloCreateUpdateDTO, Articulo>().ReverseMap();

            // Mapeo para Proveedor
            CreateMap<Proveedor, ProveedorDTO>().ReverseMap();
            CreateMap<ProveedorCreateUpdateDTO, Proveedor>().ReverseMap();

            // Mapeo para OrdenDeCompra
            CreateMap<OrdenDeCompra, OrdenDeCompraDTO>().ReverseMap();
            CreateMap<OrdenDeCompraCreateUpdateDTO, OrdenDeCompra>().ReverseMap();
            CreateMap<OrdenDeCompra_Articulo, OrdenDeCompraArticuloDTO>().ReverseMap();
            CreateMap<OrdenDeCompraCreateUpdateArticuloDTO, OrdenDeCompra_Articulo>().ReverseMap();

            // Mapeo para Vendedor
            CreateMap<Vendedor, VendedorDTO>().ReverseMap();
            CreateMap<VendedorCreateUpdateDTO, Vendedor>().ReverseMap();

            // Mapeo para Cliente
            CreateMap<Cliente, ClienteDTO>().ReverseMap();
            CreateMap<ClienteCreateUpdateDTO, Cliente>().ReverseMap();

            // Mapeo para Ventas
            CreateMap<Venta, VentaDTO>().ReverseMap();
            CreateMap<VentaCreateUpdateDTO, Venta>().ReverseMap();

            // Mapeo para Factura
            CreateMap<FacturaCreateUpdateDTO, Factura>().ReverseMap();
            CreateMap<FacturaCreateUpdateArticuloDTO, Factura_Articulo>().ReverseMap();
            CreateMap<Factura, FacturaDTO>().ReverseMap();
            CreateMap<Factura_Articulo, FacturaArticuloDTO>().ReverseMap();

            // Mapeo para Nota de Débito
            CreateMap<NotaDebito, NotaDebitoDTO>().ReverseMap();
            
            // Mapeo para Nota de Crédito
            CreateMap<NotaCredito, NotaCreditoDTO>().ReverseMap();

            // Mapeo para Cobranza
            CreateMap<Cobranza, CobranzaDTO>().ReverseMap();
            CreateMap<CobranzaCreateUpdateDTO, Cobranza>().ReverseMap();
        }
    }
}
