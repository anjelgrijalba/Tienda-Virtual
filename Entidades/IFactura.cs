using System;
using System.Collections.Generic;

namespace TiendaVirtual.Entidades
{
    public interface IFactura: ICompra
    {
        
        string Numero { get; set; }
        DateTime Fecha { get; set; }

        void ImportarLineas(IEnumerable<ILineaFactura> lineas);
    }
}
