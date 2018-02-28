using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaVirtual.Entidades;

namespace TiendaVirtual.AccesoDatos
{
    public interface IDaoFactura
    {
        void Alta(IFactura factura);
        void AltaLineas(IFactura factura, int id);
         IEnumerable<IFactura> ListarTodas();
        int GetIdFactura(string numero);
        string GenerarNumero();
        IFactura BuscarPorNumero(string numero);
        void Baja(int id);

    }
}
