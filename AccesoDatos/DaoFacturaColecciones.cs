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

        public IEnumerable<IFactura> ListarTodas()
        {
            return null;
        }
        public void AltaLineas(IFactura factura, int id)
        {

        }

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

        public int GetIdFactura(string numero)
        {
            throw new NotImplementedException();
        }

        public string GenerarNumero()
        {
            throw new NotImplementedException();
        }

        public IFactura BuscarPorNumero(string numero)
        {
            throw new NotImplementedException();
        }

        public void Baja(string numero)
        {
            throw new NotImplementedException();
        }
     
    }
}
