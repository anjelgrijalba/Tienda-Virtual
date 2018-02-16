using TiendaVirtual.Entidades;
using System.Collections.Generic;

namespace TiendaVirtual.AccesoDatos
{
    public interface IDaoUsuario
    {
        void Alta(IUsuario usuario);
        void Modificacion(IUsuario usuario);
        void Baja(IUsuario usuario);
        void Baja(int id);

        IUsuario BuscarPorId(int id);
        IEnumerable<IUsuario> BuscarTodos();
        IUsuario BuscarPorNick(string nick);
    }
}
