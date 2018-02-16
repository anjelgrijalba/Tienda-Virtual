using System;
using System.Text;
using TiendaVirtual.Entidades;
using TiendaVirtual.LogicaNegocio;

namespace TiendaVirtual.PresentacionConsola
{
    class Program
    {
        private static ILogicaNegocio ln = new LogicaNegocio.LogicaNegocio();
        static void Main(string[] args)
        {
            string e = Encoding.Default.GetString(new byte[] { 128 });

            Console.OutputEncoding = Encoding.Default;

            Console.WriteLine(e);

            MostrarUsuarios();

            //ln.AltaUsuario(PedirUsuario());

            //MostrarUsuarios();

            int idUsuario;

            Console.WriteLine(ln.ValidarUsuario("Javier", "adfakljflk")); //0
            Console.WriteLine(idUsuario = ln.ValidarUsuario("Javier", "Lete")); //1

            IUsuario u = ln.ValidarUsuarioYDevolverUsuario("Javier", "adfakljflk");

            Console.WriteLine(u != null ? u.ToString() : "El usuario no es válido");
            Console.WriteLine(ln.ValidarUsuarioYDevolverUsuario("Javier", "Lete")); //1

            IUsuario usuario = ln.BuscarUsuarioPorId(idUsuario);

            foreach (IProducto p in ln.ListadoProductos())
                Console.WriteLine("{0}, {1}, {2:0.00} {3}",
                    p.Id, p.Nombre, p.Precio, e); //1, Placa Base, 123€

            IProducto producto = ln.BuscarProductoPorId(2);

            Console.WriteLine(producto); //Ficha completa

            ICarrito carrito = new Carrito(usuario);

            ln.AgregarProductoACarrito(producto, carrito);
            ln.AgregarProductoACarrito(producto, carrito);
            ln.AgregarProductoACarrito(new Producto(3, "Pantalla", 100.0m), carrito);

            ////VER CONTENIDO DE CARRITO

            Console.WriteLine(carrito.Usuario);

            foreach (ILineaFactura linea in ln.ListadoProductosCarrito(carrito))
                Console.WriteLine(linea);

            Console.WriteLine(carrito.ImporteSinIva);
            Console.WriteLine(carrito.Iva);
            Console.WriteLine(carrito.Total);

            //(decimal ImporteSinIva, decimal Iva, decimal Total) 
            var totales = ((Compra)carrito).CalcularTotales();
            Console.WriteLine($"{totales.ImporteSinIva}, {totales.Iva}, {totales.Total}");

            //for (int i = 0; i < 15; i++)
            //{
            IFactura factura = ln.FacturarCarrito(carrito);

            carrito = null;

            Console.WriteLine(factura);
            //}
        }



        static void MainUsuarios(string[] args)
        {
            MostrarUsuarios();

            ln.AltaUsuario(PedirUsuario());

            MostrarUsuarios();

            ln.ModificarUsuario(new Usuario(2, "Yepa", "Yepez"));

            MostrarUsuarios();

            ln.BajaUsuario(new Usuario(3, "Juan", "García"));

            MostrarUsuarios();

            Console.WriteLine(ln.BuscarUsuarioPorId(1));
        }

        private static IUsuario PedirUsuario()
        {
            return new Usuario(int.Parse(Pedir("Id")), Pedir("Nick"), Pedir("Password"));
        }

        private static string Pedir(string campo)
        {
            Console.Write("Introduce el " + campo + ": ");
            return Console.ReadLine();
        }

        private static void MostrarUsuarios()
        {
            foreach (IUsuario usuario in ln.BuscarTodosUsuarios())
                Console.WriteLine(usuario);
            Console.WriteLine();
        }
    }
}
