using TiendaVirtual.Entidades;
using System.Collections.Generic;

namespace TiendaVirtual.LogicaNegocio
{
    public interface ILogicaNegocio
    {
        void AltaUsuario(IUsuario usuario);
        void ModificarUsuario(IUsuario usuario);
        void BajaUsuario(IUsuario usuario);
        void BajaUsuario(int id);

        IUsuario BuscarUsuarioPorId(int id);
        IEnumerable<IUsuario> BuscarTodosUsuarios();
        int ValidarUsuario(string nick, string password);
        bool ExisteUsuario(string nick, string password);
        IUsuario ValidarUsuarioYDevolverUsuario(string nick, string password);
        IEnumerable<IProducto> ListadoProductos();
        IProducto BuscarProductoPorId(int v);
        void AgregarProductoACarrito(IProducto producto, ICarrito carrito);
        void AgregarProductoACarrito(IProducto producto, int cantidad, ICarrito carrito);
        IEnumerable<ILineaFactura> ListadoProductosCarrito(ICarrito carrito);
        IFactura FacturarCarrito(ICarrito carrito);
        void AltaProducto(IProducto producto);
        void ModificarProducto(IProducto producto);
        void BajaProducto(int id);

        
    }
}
