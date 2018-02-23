using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaVirtual.Entidades;

namespace TiendaVirtual.AccesoDatos
{
    class DaoProductoSqlServer : IDaoProducto
    {
        private const string SQL_INSERT = "INSERT INTO Productos (Nombre, Precio) VALUES (@Nombre, @Precio)";
        private const string SQL_DELETE = "DELETE FROM Productos WHERE Id=@Id";
        private const string SQL_UPDATE = "UPDATE Productos SET Nombre=@Nombre,Precio=@Precio WHERE Id=@Id";
        private const string SQL_SELECT = "SELECT Id, Nombre, Precio FROM Productos";
        private const string SQL_SELECT_ID = "SELECT Id, Nombre, Precio FROM Productos WHERE Id=@Id";
        private const string SQL_SELECT_Nombre = "SELECT Id, Nombre, Precio FROM Productos WHERE Nombre=@Nombre";


        private string connectionString;

        public DaoProductoSqlServer(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void Alta(IProducto Producto)
        {
            try
            {
                using (IDbConnection con = new System.Data.SqlClient.SqlConnection(connectionString))
                {
                    //"Zona declarativa"
                    con.Open();

                    IDbCommand comInsert = con.CreateCommand();

                    comInsert.CommandText = SQL_INSERT;

                    IDbDataParameter parNombre = comInsert.CreateParameter();
                    parNombre.ParameterName = "Nombre";
                    parNombre.DbType = DbType.String;

                    IDbDataParameter parPrecio = comInsert.CreateParameter();
                    parPrecio.ParameterName = "Precio";
                    parPrecio.DbType = DbType.Decimal;

                    comInsert.Parameters.Add(parNombre);
                    comInsert.Parameters.Add(parPrecio);

                    //"Zona concreta"
                    parNombre.Value = Producto.Nombre;
                    parPrecio.Value = Producto.Precio;

                    int numRegistrosInsertados = comInsert.ExecuteNonQuery();

                    if (numRegistrosInsertados != 1)
                        throw new AccesoDatosException("Número de registros insertados: " +
                            numRegistrosInsertados);
                }
            }
            catch (Exception e)
            {
                throw new AccesoDatosException("No se ha podido realizar el alta", e);
            }
        }

        public void Baja(IProducto Producto)
        {
            Baja(Producto.Id);
        }

        public void Baja(int id)
        {
            try
            {
                using (IDbConnection con = new System.Data.SqlClient.SqlConnection(connectionString))
                {
                    //"Zona declarativa"
                    con.Open();

                    IDbCommand comDelete = con.CreateCommand();

                    comDelete.CommandText = SQL_DELETE;

                    IDbDataParameter parId = comDelete.CreateParameter();
                    parId.ParameterName = "Id";
                    parId.DbType = DbType.Int32;

                    comDelete.Parameters.Add(parId);

                    //"Zona concreta"
                    parId.Value = id;

                    int numRegistrosBorrados = comDelete.ExecuteNonQuery();

                    if (numRegistrosBorrados != 1)
                        throw new AccesoDatosException("Número de registros borrados: " +
                            numRegistrosBorrados);
                }
            }
            catch (Exception e)
            {
                throw new AccesoDatosException("No se ha podido realizar el borrado", e);
            }
        }

        public IProducto BuscarPorId(int id)
        {
            try
            {
                using (IDbConnection con = new System.Data.SqlClient.SqlConnection(connectionString))
                {
                    //"Zona declarativa"
                    con.Open();

                    IDbCommand comSelectId = con.CreateCommand();

                    comSelectId.CommandText = SQL_SELECT_ID;

                    IDbDataParameter parId = comSelectId.CreateParameter();
                    parId.ParameterName = "Id";
                    parId.DbType = DbType.Int32;

                    comSelectId.Parameters.Add(parId);

                    //"Zona concreta"

                    parId.Value = id;

                    IDataReader dr = comSelectId.ExecuteReader();

                    if (dr.Read())
                    {
                        IProducto Producto = new Producto();

                        Producto.Id = dr.GetInt32(0);
                        Producto.Nombre = dr.GetString(1);
                        Producto.Precio = dr.GetDecimal(2);

                        return Producto;
                    }

                    return null;
                }
            }
            catch (Exception e)
            {
                throw new AccesoDatosException("No se ha podido buscar ese Producto por ese id", e);
            }
        }

        public IProducto BuscarPorNombre(string nombre)
        {
            try
            {
                using (IDbConnection con = new System.Data.SqlClient.SqlConnection(connectionString))
                {
                    //"Zona declarativa"
                    con.Open();

                    IDbCommand comSelectId = con.CreateCommand();

                    comSelectId.CommandText = SQL_SELECT_Nombre;

                    IDbDataParameter parNombre = comSelectId.CreateParameter();
                    parNombre.ParameterName = "Nombre";
                    parNombre.DbType = DbType.String;

                    comSelectId.Parameters.Add(parNombre);

                    //"Zona concreta"

                    parNombre.Value = nombre;

                    IDataReader dr = comSelectId.ExecuteReader();

                    if (dr.Read())
                    {
                        IProducto Producto = new Producto();

                        Producto.Id = dr.GetInt32(0);
                        Producto.Nombre = dr.GetString(1);
                        Producto.Precio = dr.GetDecimal(2);

                        return Producto;
                    }

                    return null;
                }
            }
            catch (Exception e)
            {
                throw new AccesoDatosException("No se ha podido buscar ese Producto por ese id", e);
            }
        }

        public IEnumerable<IProducto> BuscarTodos()
        {
            List<IProducto> Productos = new List<IProducto>();

            try
            {
                using (IDbConnection con = new System.Data.SqlClient.SqlConnection(connectionString))
                {
                    //"Zona declarativa"
                    con.Open();

                    IDbCommand comSelect = con.CreateCommand();

                    comSelect.CommandText = SQL_SELECT;

                    //"Zona concreta"
                    IDataReader dr = comSelect.ExecuteReader();

                    IProducto Producto;

                    while (dr.Read())
                    {
                        Producto = new Producto();

                        Producto.Id = dr.GetInt32(0);
                        Producto.Nombre = dr.GetString(1);
                        Producto.Precio = dr.GetDecimal(2);

                        Productos.Add(Producto);
                    }

                    return Productos;
                }
            }
            catch (Exception e)
            {
                throw new AccesoDatosException("No se ha podido buscar todos los Productos", e);
            }
        }

        public void Modificacion(IProducto Producto)
        {
            try
            {
                using (IDbConnection con = new System.Data.SqlClient.SqlConnection(connectionString))
                {
                    //"Zona declarativa"
                    con.Open();

                    IDbCommand comUpdate = con.CreateCommand();

                    comUpdate.CommandText = SQL_UPDATE;

                    IDbDataParameter parId = comUpdate.CreateParameter();
                    parId.ParameterName = "Id";
                    parId.DbType = DbType.Int32;

                    IDbDataParameter parNombre = comUpdate.CreateParameter();
                    parNombre.ParameterName = "Nombre";
                    parNombre.DbType = DbType.String;

                    IDbDataParameter parPrecio = comUpdate.CreateParameter();
                    parPrecio.ParameterName = "Precio";
                    parPrecio.DbType = DbType.String;

                    comUpdate.Parameters.Add(parId);
                    comUpdate.Parameters.Add(parNombre);
                    comUpdate.Parameters.Add(parPrecio);

                    //"Zona concreta"
                    parId.Value = Producto.Id;
                    parNombre.Value = Producto.Nombre;
                    parPrecio.Value = Producto.Precio;

                    int numRegistrosModificados = comUpdate.ExecuteNonQuery();

                    if (numRegistrosModificados != 1)
                        throw new AccesoDatosException("Número de registros modificados: " +
                            numRegistrosModificados);
                }
            }
            catch (Exception e)
            {
                throw new AccesoDatosException("No se ha podido realizar la modificación", e);
            }
        }
    }
}

