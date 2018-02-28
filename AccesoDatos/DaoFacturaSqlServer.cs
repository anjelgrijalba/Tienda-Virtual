using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaVirtual.Entidades;



namespace TiendaVirtual.AccesoDatos
{
    public class DaoFacturaSqlServer : IDaoFactura
    {

        private const string SQL_INSERT = "INSERT INTO facturas (Numero, Fecha, UsuariosId ) VALUES (@Numero, @Fecha, @Uid)";
        private const string SQL_SELECT_LAST = "SELECT TOP 1 Numero FROM facturas ORDER BY ID DESC";
        private const string SELECT_FACTURAS = "select distinct facturaid from lineasfactura";
        private const string SQL_INSERT_LINEAS = "INSERT INTO lineasfactura (FacturaId, ProductoId, Cantidad ) VALUES (@NumeroFactura, @ProductoId, @Cantidad)";
        private const string SQL_SELECT_IDFACTURA = "SELECT Id FROM facturas WHERE Numero=@NumeroFactura";
        private const string SQL_SELECT_ID = "SELECT Id, Nick, Contra FROM usuarios WHERE Id=@Id";
        private const string SQL_SELECT_FACTURA = "SELECT f.Fecha, l.Cantidad, p.Id, p.Nombre, p.Precio, u.Nick " +
                                                    "FROM facturas f " +
                                                    "INNER JOIN lineasfactura l ON f.Id = l.FacturaId " +
                                                    "INNER JOIN productos p ON p.Id = l.ProductoId " +
                                                    "INNER JOIN usuarios u ON u.Id= f.UsuariosId " +
                                                    "WHERE f.Numero = @NumeroFactura";

        //private const string SELECT_LIN = "SELECT f.Numero, f.Fecha, f.UsuariosId, l.Cantidad, p.Id, p.Nombre, p.Precio, u.Nick, f.Id FROM facturas f, lineasfactura l, productos p, usuarios u WHERE f.Id = l.FacturaId AND p.Id = l.ProductoId AND u.Id= f.UsuariosId";
        //private const string SELECT_LIN = "SELECT f.Numero, f.Fecha, f.UsuariosId, l.Cantidad, p.Id, p.Nombre, p.Precio, u.Nick FROM facturas f INNER JOIN lineasfactura l ON f.Id = l.FacturaId INNER JOIN productos p ON p.Id = l.ProductoId INNER JOIN usuarios u ON u.Id= f.UsuariosId";

        private string connectionString;

        public DaoFacturaSqlServer(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public string GenerarNumero()
        {
            try
            {
                using (IDbConnection con = new System.Data.SqlClient.SqlConnection(connectionString))
                {
                    //"Zona declarativa"   para obtener el numero de la última factura
                    con.Open();

                    IDbCommand comSelectLast = con.CreateCommand();

                    comSelectLast.CommandText = SQL_SELECT_LAST;

                    string ultimoNumero = comSelectLast.ExecuteScalar().ToString();

                    string numero = (int.Parse(ultimoNumero) + 1).ToString("0000000");

                    return numero;
                }
            }
            catch (Exception e)
            {
                throw new AccesoDatosException("No se ha podido generar e numero", e);
            }
        }

        public void Alta(IFactura factura) { }

        public void Alta(DateTime fecha, int idU, string numero)
        {
            try
            {
                using (IDbConnection con = new System.Data.SqlClient.SqlConnection(connectionString))
                {
                    //"Zona declarativa"   para insertar la factura en la tabla facturas de la BD
                    con.Open();
                    IDbCommand comInsert = con.CreateCommand();

                    comInsert.CommandText = SQL_INSERT;

                    IDbDataParameter parUid = comInsert.CreateParameter();
                    parUid.ParameterName = "Uid";
                    parUid.DbType = DbType.Int16;

                    IDbDataParameter parNumero = comInsert.CreateParameter();
                    parNumero.ParameterName = "Numero";
                    parNumero.DbType = DbType.String;

                    IDbDataParameter parFecha = comInsert.CreateParameter();
                    parFecha.ParameterName = "Fecha";
                    parFecha.DbType = DbType.Date;

                    comInsert.Parameters.Add(parNumero);
                    comInsert.Parameters.Add(parFecha);
                    comInsert.Parameters.Add(parUid);

                    //"Zona concreta"
                    parUid.Value = idU;
                    parNumero.Value = numero;
                    parFecha.Value = fecha;


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

        public IEnumerable<IFactura> ListarTodas()
        {

            List<IFactura> facturas = new List<IFactura>();
            List<ILineaFactura> lineas = new List<ILineaFactura>();

            try
            {
                using (IDbConnection con = new System.Data.SqlClient.SqlConnection(connectionString))
                {
                    //"Zona declarativa"
                    con.Open();

                    IDbCommand comSelect = con.CreateCommand();
                    const string SELECT_LIN =
                        "SELECT f.Numero, f.Fecha, f.UsuariosId, l.Cantidad, p.Id, p.Nombre, p.Precio, u.Nick " +
                        "FROM facturas f " +
                        "INNER JOIN lineasfactura l ON f.Id = l.FacturaId " +
                        "INNER JOIN productos p ON p.Id = l.ProductoId " +
                        "INNER JOIN usuarios u ON u.Id= f.UsuariosId";

                    comSelect.CommandText = SELECT_LIN;
                    IDataReader dr = comSelect.ExecuteReader();

                    List<Object[]> listado = new List<Object[]>();

                    while (dr.Read())    //lee cada una de las lineas de todas las facturas seguidas
                    {
                        Object[] linea = new Object[]
                        {
                              dr.GetString(0),
                              dr.GetDateTime(1),
                              dr.GetInt32(2),
                              dr.GetInt32(3),
                              dr.GetInt32(4),
                              dr.GetString(5),
                              dr.GetDecimal(6),
                              dr.GetString(7),
                        };
                        listado.Add(linea);
                    }
                    int totalFacts = listado.Count();
                    for (int o = 0; o < totalFacts; o++)
                    {
                        if (listado[o] != null) //comprobacion exception
                        {
                            IFactura factura;
                            IUsuario usuario = new Usuario();
                            factura = new Factura(usuario);
                            factura.Numero = listado[o][0].ToString();
                            factura.Fecha = (DateTime)listado[o][1];
                            factura.Usuario.Nick = listado[o][7].ToString();

                            IProducto P = new Producto((int)listado[o][4], listado[o][5].ToString(), (decimal)listado[o][6]);
                            ILineaFactura L = new LineaFactura(P, (int)listado[o][3]);

                            lineas.Add(L);
                            if (o != totalFacts - 1)
                            {
                                //compruebo si hemos cambiado de factura
                                if (listado[o][0].ToString() != listado[o + 1][0].ToString())
                                {
                                    factura.ImportarLineas(lineas);
                                    facturas.Add(factura);
                                    lineas.Clear();
                                }
                            }
                            else  //si es la última factura
                            {
                                factura.ImportarLineas(lineas);
                                facturas.Add(factura);
                                lineas.Clear();
                            }
                        }
                    }
                }
                return facturas;
            }
            catch (Exception e)
            {
                throw new AccesoDatosException("No se han podido buscar todas las facturas", e);
            }

        }

        public int GetIdFactura(string numero)
        {
            try
            {
                using (IDbConnection con = new System.Data.SqlClient.SqlConnection(connectionString))
                {
                    //"Zona declarativa"
                    con.Open();

                    IDbCommand comSelectId = con.CreateCommand();

                    comSelectId.CommandText = SQL_SELECT_IDFACTURA;

                    IDbDataParameter parNumeroFactura = comSelectId.CreateParameter();
                    parNumeroFactura.ParameterName = "NumeroFactura";
                    parNumeroFactura.DbType = DbType.String;

                    comSelectId.Parameters.Add(parNumeroFactura);

                    //"Zona concreta"

                    parNumeroFactura.Value = numero;
                    int id = (int)comSelectId.ExecuteScalar();
                    return id;
                }
            }
            catch (Exception e)
            {
                throw new AccesoDatosException("No se ha podido encontrar el id de esa factura ", e);
            }
        }

        public void AltaLineas(IFactura factura, int id)
        {
            try
            {
                using (IDbConnection con = new System.Data.SqlClient.SqlConnection(connectionString))
                {
                    IDbTransaction transaccion;
                    //"Zona declarativa"
                    con.Open();
                    transaccion = con.BeginTransaction();
                    try
                    {
                        IDbCommand comSecomInsertLineas = con.CreateCommand();
                        comSecomInsertLineas.Transaction = transaccion;
                        comSecomInsertLineas.CommandText = SQL_INSERT_LINEAS;

                        IDbDataParameter parIdFactura = comSecomInsertLineas.CreateParameter();
                        parIdFactura.ParameterName = "NumeroFactura";
                        parIdFactura.DbType = DbType.String;

                        IDbDataParameter parProducto = comSecomInsertLineas.CreateParameter();
                        parProducto.ParameterName = "ProductoId";
                        parProducto.DbType = DbType.Int16;

                        IDbDataParameter parCantidad = comSecomInsertLineas.CreateParameter();
                        parCantidad.ParameterName = "Cantidad";
                        parCantidad.DbType = DbType.Int16;

                        comSecomInsertLineas.Parameters.Add(parProducto);
                        comSecomInsertLineas.Parameters.Add(parCantidad);
                        comSecomInsertLineas.Parameters.Add(parIdFactura);

                        foreach (ILineaFactura l in factura.LineasFactura)
                        {
                            //"Zona concreta"
                            parIdFactura.Value = id;
                            parProducto.Value = l.Producto.Id;
                            parCantidad.Value = l.Cantidad;

                            int numRegistrosInsertados = comSecomInsertLineas.ExecuteNonQuery();
                            if (numRegistrosInsertados != 1)
                                throw new AccesoDatosException("Número de registros insertados: " +
                                    numRegistrosInsertados);
                        }

                        transaccion.Commit();
                    }
                    catch (SqlException)
                    {
                        transaccion.Rollback();
                    }
                }
            }
            catch (Exception e)
            {
                throw new AccesoDatosException("No se ha podido realizar el alta", e);
            }
        }

        public IFactura BuscarPorNumero(string numero)
        {
            try
            {
                using (IDbConnection con = new System.Data.SqlClient.SqlConnection(connectionString))
                {
                    //"Zona declarativa"
                    con.Open();

                    IDbCommand comSelect = con.CreateCommand();

                    comSelect.CommandText = SQL_SELECT_FACTURA;

                    IDbDataParameter parIdFactura = comSelect.CreateParameter();
                    parIdFactura.ParameterName = "NumeroFactura";
                    parIdFactura.DbType = DbType.String;
                    comSelect.Parameters.Add(parIdFactura);
                    parIdFactura.Value = numero;

                    IDataReader dr = comSelect.ExecuteReader();

                    IUsuario usuario = new Usuario();
                    IFactura factura = new Factura(usuario);
                    IProducto producto = new Producto();
                    List<ILineaFactura> lineas = new List<ILineaFactura>();


                    while (dr.Read())
                    {
                        factura.Numero = numero;
                        factura.Fecha = dr.GetDateTime(0);
                        factura.Usuario.Nick = dr.GetString(5);
                        IProducto P = new Producto(dr.GetInt32(2), dr.GetString(3), dr.GetDecimal(4));
                        ILineaFactura L = new LineaFactura(P, dr.GetInt32(1));
                        lineas.Add(L);
                    }
                    factura.ImportarLineas(lineas);
                    return factura;
                }
            }
            catch (Exception e)
            {
                throw new AccesoDatosException("No se han podido encontrar la factura", e);
            }

        }
    }
}
