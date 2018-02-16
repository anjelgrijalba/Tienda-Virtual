using TiendaVirtual.Entidades;
using System.Collections.Generic;

namespace TiendaVirtual.AccesoDatos
{
    public interface IDaoProducto
    {
        void Alta(IProducto producto);
        void Modificacion(IProducto producto);
        void Baja(IProducto producto);
        void Baja(int id);

        IProducto BuscarPorId(int id);
        
        IEnumerable<IProducto> BuscarTodos();
    }
}
