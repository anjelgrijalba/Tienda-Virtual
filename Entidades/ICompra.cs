using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaVirtual.Entidades
{
    public interface ICompra
    {
        IUsuario Usuario { get; set; }

        IEnumerable<ILineaFactura> LineasFactura { get; }

        decimal ImporteSinIva { get; }
        decimal Iva { get; }
        decimal Total { get; }

        void Add(IProducto producto);

        void Add(IProducto producto, int cantidad);
    }
}
