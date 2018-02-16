namespace TiendaVirtual.Entidades
{
    public class Carrito: Compra, ICarrito
    {
        //public Carrito() { }
        public Carrito(IUsuario usuario) : base(usuario) { }
        
    }
}
