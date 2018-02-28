using TiendaVirtual.Entidades;
using System.Collections.Generic;
using System;

namespace TiendaVirtual.LogicaNegocio
{
    public interface ILogicaNegocio
    {
        //usuarios
        void AltaUsuario(IUsuario usuario);
        void ModificarUsuario(IUsuario usuario);
        void BajaUsuario(IUsuario usuario);
        void BajaUsuario(int id);

        IUsuario BuscarUsuarioPorNick(string nick);
        IUsuario BuscarUsuarioPorId(int id);
        IEnumerable<IUsuario> BuscarTodosUsuarios();
        int ValidarUsuario(string nick, string password);
        bool ExisteUsuario(string nick, string password);
        bool ExisteNick(string nick);
        IUsuario ValidarUsuarioYDevolverUsuario(string nick, string password);
        bool PasswordCorrecto(string nick, string password);


        //productos
        IEnumerable<IProducto> ListadoProductos();
        IProducto BuscarProductoPorId(int v);
        void AgregarProductoACarrito(IProducto producto, ICarrito carrito);
        void AgregarProductoACarrito(IProducto producto, int cantidad, ICarrito carrito);
        void AltaProducto(IProducto producto);
        void ModificarProducto(IProducto producto);
        void BajaProducto(int id);

        // facturas
        IEnumerable<ILineaFactura> ListadoProductosCarrito(ICarrito carrito);
        IFactura FacturarCarrito(ICarrito carrito, string numero);
        void AltaFactura(IFactura factura);
        IEnumerable<IFactura> ListarFacturas();
        void AltaLineas(IFactura factura, int id);
        string GenerarNumero();
        int GetIdFactura(string numero);
        IFactura BuscarFacturaPorNumero(string numero);
        void BajaFactura(string numero);
    }
}
