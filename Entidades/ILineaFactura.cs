namespace TiendaVirtual.Entidades
{
    public interface ILineaFactura
    {
        IProducto Producto { get; set; }
        int Cantidad { get; set; }
        decimal ImporteSinIva { get; }
        decimal Iva { get; }
        decimal Total { get; }
    }
}