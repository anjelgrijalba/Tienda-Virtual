using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaVirtual.Entidades;

namespace TiendaVirtual.AccesoDatos
{
    public class DaoProductoColecciones : IDaoProducto
    {
        IDictionary<int, IProducto> productos = new Dictionary<int, IProducto>();

        //para MVC
       // IEnumerable <int, IProducto> productoss = new IEnumerable<int,Producto>();

        public DaoProductoColecciones()
        {
            for (int i = 1; i <= 20; i++)
                Alta(new Producto(i, "Producto" + i, i * 10));
            //Alta(new Producto(1, "Teclado", 11.0m));
            //Alta(new Producto(2, "Ratón", 12.0m));
            //Alta(new Producto(3, "Memoria USB 64GB", 10.0m));
            //Alta(new Producto(4, "Auriculares", 5.0m));
            //Alta(new Producto(5, "Impresora", 10.0m));
            //Alta(new Producto(6, "Pantalla", 5.0m));
        }
        
        public void Alta(IProducto producto)
        {
            productos.Add(producto.Id, producto);
        }

        public void Baja(IProducto producto)
        {
            Baja(producto.Id);
        }

        public void Baja(int id)
        {
            productos.Remove(id);
        }

        public IProducto BuscarPorId(int id)
        {
            return productos[id];
        }

       
        public IEnumerable<IProducto> BuscarTodos()
        {
            return new ReadOnlyCollection<IProducto>(productos.Values.ToArray<IProducto>());
        }


        public void Modificacion(IProducto producto)
        {
            productos[producto.Id] = producto;
        }
    }

   
}
