using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaVirtual.AccesoDatos
{
    public class DaoFactory
    {
        private string tipo, cadenaConexion, usuario, password;

        public DaoFactory(string tipo, string cadenaConexion = null, string usuario = null, string password = null)
        {
            this.tipo = tipo;
            this.cadenaConexion = cadenaConexion;
            this.usuario = usuario;
            this.password = password;
        }

        public IDaoUsuario GetDaoUsuario()
        {
            switch (tipo)
            {
                case "coleccion": return new DaoUsuarioColecciones();
                case "SqlServer": return new DaoUsuarioSqlServer(cadenaConexion);
                default:
                    throw new NotImplementedException("No existe la opción " + tipo);
            }
        }

        public IDaoProducto GetDaoProducto()
        {
            switch (tipo)
            {
                case "coleccion": return new DaoProductoColecciones();
                case "SqlServer": return new DaoProductoSqlServer(cadenaConexion);
                default:
                    throw new NotImplementedException("No existe la opción " + tipo);
            }
        }

        public IDaoFactura GetDaoFactura()
        {
            return new DaoFacturaColecciones();
            //switch (tipo)
            //{
            //    case "coleccion": return new DaoFacturaColecciones();
            //    default:
            //        throw new NotImplementedException("No existe la opción " + tipo);
            //}
        }
    }
}
