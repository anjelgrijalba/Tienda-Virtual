namespace PresentacionAspNetMvc.Models
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using TiendaVirtual.Entidades;

    public class ProductoDbContext : DbContext
    {
       
        //public ProductoDbContext()
        //    : base("name=ProductoDbContext")
        //{
        //}

       

        public virtual DbSet<Producto> productos { get; set; }

        //public virtual DbSet<Usuario> usuarios { get; set; }

       




    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}