using System.Collections.Generic;
using TiendaVirtual.Entidades;
using System.Diagnostics;
using TiendaVirtual.AccesoDatos;
using System;


namespace TiendaVirtual.LogicaNegocio
{
    public class LogicaNegocio : ILogicaNegocio
    {
        private string tipo, cadenaConexion, usuario, password;

        public LogicaNegocio(string tipo = "coleccion", string cadenaConexion = null, string usuario = null, string password = null)
        {
            this.tipo = tipo;
            this.cadenaConexion = cadenaConexion;
            this.usuario = usuario;
            this.password = password;

            DaoFactory daoFactory = new DaoFactory(tipo, cadenaConexion, usuario, password);

            daoUsuario = daoFactory.GetDaoUsuario();
            daoProducto = daoFactory.GetDaoProducto();
            daoFactura = daoFactory.GetDaoFactura();
        }

        private IDaoUsuario daoUsuario = new DaoUsuarioColecciones();
        private IDaoProducto daoProducto = new DaoProductoColecciones();
        private IDaoFactura daoFactura = new DaoFacturaColecciones();


        public void AgregarProductoACarrito(IProducto producto, ICarrito carrito)
        {
            AgregarProductoACarrito(producto, 1, carrito);
        }

        public void AgregarProductoACarrito(IProducto producto, int cantidad, ICarrito carrito)
        {
            carrito.Add(producto, cantidad);
        }

        public void AltaProducto(IProducto producto)
        {
            daoProducto.Alta(producto);
        }

        public void AltaUsuario(IUsuario usuario)
        {
            Debug.Print("Alta de " + usuario);

            daoUsuario.Alta(usuario);
        }

        public void BajaProducto(int id)
        {
            daoProducto.Baja(id);
        }

        public void BajaUsuario(IUsuario usuario)
        {
            daoUsuario.Baja(usuario);
        }

        public void BajaUsuario(int id)
        {
            daoUsuario.Baja(id);
        }

        public IProducto BuscarProductoPorId(int id)
        {
            return daoProducto.BuscarPorId(id);
        }

        public IEnumerable<IUsuario> BuscarTodosUsuarios()
        {
            Debug.Print("Consulta de todos los usuarios");

            return daoUsuario.BuscarTodos();
        }

        public IUsuario BuscarUsuarioPorId(int id)
        {
            return daoUsuario.BuscarPorId(id);
        }

        public IUsuario BuscarUsuarioPorNick(string nick)
        {
            return daoUsuario.BuscarPorNick(nick);
        }

        public IFactura FacturarCarrito(ICarrito carrito, string numero)
        {
            IFactura f = new Factura(carrito.Usuario);

            f.ImportarLineas(carrito.LineasFactura);

            f.Fecha = DateTime.Today;

            f.Numero = numero;

            return f;
        }

        public void AltaFactura(DateTime fecha, int idU, string numero)
        {
            daoFactura.Alta(fecha, idU, numero);
        }

        public void AltaFactura(IFactura factura)
        {
            daoFactura.Alta(factura);
        }
        
        public IEnumerable<IProducto> ListadoProductos()
        {
            return daoProducto.BuscarTodos();
        }

        public IEnumerable<ILineaFactura> ListadoProductosCarrito(ICarrito carrito)
        {
            return carrito.LineasFactura;
        }

        public void ModificarProducto(IProducto producto)
        {
            daoProducto.Modificacion(producto);
        }

        public void ModificarUsuario(IUsuario usuario)
        {
            daoUsuario.Modificacion(usuario);
        }

        public int ValidarUsuario(string nick, string password)
        {
            IUsuario usuarioValido = ValidarUsuarioYDevolverUsuario(nick, password);

            return usuarioValido != null ? usuarioValido.Id : 0;
        }

        public IUsuario ValidarUsuarioYDevolverUsuario(string nick, string password)
        {
            IUsuario usuario = daoUsuario.BuscarPorNick(nick);

            return usuario != null && password == usuario.Password ? usuario : null;
        }

        public bool ExisteUsuario(string nick, string password)
        {
            IUsuario usuarioValido = ValidarUsuarioYDevolverUsuario(nick, password);

            return usuarioValido != null ? true : false;
        }

        public bool ExisteNick(string nick)
        {
            IUsuario nickValido = BuscarUsuarioPorNick(nick);

            return nickValido != null ? true : false;
        }

        public bool PasswordCorrecto(string nick, string password)
        {
            IUsuario usuario = daoUsuario.BuscarPorNick(nick);

            return usuario != null && password == usuario.Password ? true : false;
        }

        public IEnumerable<IFactura> ListarFacturas()
        {
            return daoFactura.ListarTodas();
        }

        public string  GenerarNumero()
        {
            return daoFactura.GenerarNumero();
        }

        public int GetIdFactura(string numero)
        {
            return daoFactura.GetIdFactura(numero);
        }

        public void AltaLineas(IFactura factura, int id)
        {
            daoFactura.AltaLineas(factura, id);
        }
        public IFactura BuscarFacturaPorNumero(string numero)
        {
            return daoFactura.BuscarPorNumero(numero);
        }
    }
}
