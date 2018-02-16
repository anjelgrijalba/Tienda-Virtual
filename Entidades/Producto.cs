using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaVirtual.Entidades
{
    public class Producto : IProducto, IEquatable<Producto>
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }


        public Producto()
        {
            
        }

        public Producto(int id, string nombre, decimal precio)
        {
            Id = id;
            Nombre = nombre ?? throw new ArgumentNullException(nameof(nombre));
            Precio = precio;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Producto);
        }

        public bool Equals(Producto other)
        {
            return other != null &&
                   Id == other.Id &&
                   Nombre == other.Nombre &&
                   Precio == other.Precio;
        }

        public override int GetHashCode()
        {
            var hashCode = -675047433;
            hashCode = hashCode * -1521134295 + Id.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Nombre);
            hashCode = hashCode * -1521134295 + Precio.GetHashCode();
            return hashCode;
        }

        public static bool operator ==(Producto producto1, Producto producto2)
        {
            return EqualityComparer<Producto>.Default.Equals(producto1, producto2);
        }

        public static bool operator !=(Producto producto1, Producto producto2)
        {
            return !(producto1 == producto2);
        }

        public override string ToString()
        {
            return $"ID:\t{Id}\nNOMBRE:\t{Nombre}\nPRECIO:\t{Precio:c}\n";
        }
    }
}
