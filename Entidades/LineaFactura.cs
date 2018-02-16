using System;
using System.Collections.Generic;

namespace TiendaVirtual.Entidades
{
    public class LineaFactura: ILineaFactura, IEquatable<LineaFactura>
    {
        private const decimal IVA = 0.21m;

        public LineaFactura(IProducto producto, int cantidad)
        {
            Producto = producto;
            Cantidad = cantidad;
        }

        public IProducto Producto { get; set; }
        public int Cantidad { get; set; }

        public decimal ImporteSinIva => Producto.Precio * Cantidad;

        public decimal Iva => ImporteSinIva * IVA;

        public decimal Total => ImporteSinIva + Iva;

        public override bool Equals(object obj)
        {
            return Equals(obj as LineaFactura);
        }

        public bool Equals(LineaFactura other)
        {
            return other != null &&
                   EqualityComparer<IProducto>.Default.Equals(Producto, other.Producto) &&
                   Cantidad == other.Cantidad &&
                   ImporteSinIva == other.ImporteSinIva &&
                   Iva == other.Iva &&
                   Total == other.Total;
        }

        public override int GetHashCode()
        {
            var hashCode = -1529308099;
            hashCode = hashCode * -1521134295 + EqualityComparer<IProducto>.Default.GetHashCode(Producto);
            hashCode = hashCode * -1521134295 + Cantidad.GetHashCode();
            hashCode = hashCode * -1521134295 + ImporteSinIva.GetHashCode();
            hashCode = hashCode * -1521134295 + Iva.GetHashCode();
            hashCode = hashCode * -1521134295 + Total.GetHashCode();
            return hashCode;
        }

        public static bool operator ==(LineaFactura factura1, LineaFactura factura2)
        {
            return EqualityComparer<LineaFactura>.Default.Equals(factura1, factura2);
        }

        public static bool operator !=(LineaFactura factura1, LineaFactura factura2)
        {
            return !(factura1 == factura2);
        }

        public override string ToString()
        {
            return $"{Producto.Id}, {Producto.Nombre}, {Producto.Precio}, {Cantidad}, {ImporteSinIva}, {Iva}, {Total}";
        }
    }
}