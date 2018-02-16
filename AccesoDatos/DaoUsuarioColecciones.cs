using System.Collections.Generic;
using System.Linq;
using TiendaVirtual.Entidades;
using System.Collections.ObjectModel;

namespace TiendaVirtual.AccesoDatos
{
    public class DaoUsuarioColecciones : IDaoUsuario
    {
        private IDictionary<int, IUsuario> usuarios = new Dictionary<int, IUsuario>();
        private IDictionary<string, IUsuario> usuariosPorNick = new Dictionary<string, IUsuario>();

        public DaoUsuarioColecciones()
        {
            Alta(new Usuario(1, "Javier", "Lete"));
            Alta(new Usuario(2, "Pepe", "Pérez"));
        }

        public void Alta(IUsuario usuario)
        {
            usuarios.Add(usuario.Id, usuario);
            usuariosPorNick.Add(usuario.Nick, usuario);
        }

        public void Baja(IUsuario usuario)
        {
            usuariosPorNick.Remove(usuario.Nick);
            usuarios.Remove(usuario.Id);
        }

        public void Baja(int id)
        {
            usuariosPorNick.Remove(BuscarPorId(id).Nick);
            usuarios.Remove(id);
        }

        public IUsuario BuscarPorId(int id)
        {
            return usuarios[id];
        }

        public IUsuario BuscarPorNick(string nick)
        {
            if (!usuariosPorNick.ContainsKey(nick))
                return null;

            return usuariosPorNick[nick];
        }

        public IEnumerable<IUsuario> BuscarTodos()
        {
            return new ReadOnlyCollection<IUsuario>(usuarios.Values.ToArray<IUsuario>());
        }

        public void Modificacion(Usuario usuario)
        {
            Modificacion((IUsuario)usuario);
        }

        public void Modificacion(IUsuario usuario)
        {
            usuarios[usuario.Id] = usuario;
            usuariosPorNick.Remove(BuscarPorId(usuario.Id).Nick);
            usuariosPorNick.Add(usuario.Nick, usuario);
        }
    }
}
