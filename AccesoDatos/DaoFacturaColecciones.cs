using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaVirtual.Entidades;

namespace TiendaVirtual.AccesoDatos
{
    public class DaoFacturaColecciones : IDaoFactura
    {
        private IDictionary<string, IFactura> facturas = new SortedDictionary<string, IFactura>();

        public void Alta(IFactura factura)
        {
            int ultimoEntero = 0;

            string ultimoNumero = null;

            if (facturas.Count > 0)
                ultimoNumero = facturas.Keys.Last();

            if (ultimoNumero != null)
                ultimoEntero = int.Parse(ultimoNumero);

            factura.Numero = (ultimoEntero + 1).ToString("000000");

            facturas.Add(factura.Numero, factura);
        }
    }
}
