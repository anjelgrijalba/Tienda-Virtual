namespace TiendaVirtual.Entidades
{
    public interface IUsuario
    {
        int Id { get; set; }
        string Nick { get; set; }
        string Password { get; set; }
    }
}
