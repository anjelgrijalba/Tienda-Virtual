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
        //private const string SELECT_LIN = "SELECT f.Numero, f.Fecha, f.UsuariosId, l.Cantidad, p.Id, p.Nombre, p.Precio, u.Nick, f.Id FROM facturas f, lineasfactura l, productos p, usuarios u WHERE f.Id = l.FacturaId AND p.Id = l.ProductoId AND u.Id= f.UsuariosId";
        //private const string SELECT_LIN = "SELECT f.Numero, f.Fecha, f.UsuariosId, l.Cantidad, p.Id, p.Nombre, p.Precio, u.Nick FROM facturas f INNER JOIN lineasfactura l ON f.Id = l.FacturaId INNER JOIN productos p ON p.Id = l.ProductoId INNER JOIN usuarios u ON u.Id= f.UsuariosId";

        private string connectionString;

        public DaoFacturaSqlServer(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void Alta(IFactura factura)
        {
        }

        public void Alta(IFactura factura, IUsuario usuario)
        {
            try
            {

                using (IDbConnection con = new System.Data.SqlClient.SqlConnection(connectionString))
                {
                    //"Zona declarativa"
                    con.Open();

                    IDbCommand comSelectLast = con.CreateCommand();

                    comSelectLast.CommandText = SQL_SELECT_LAST;

                    //IDataReader dr = comSelectLast.ExecuteReader();
                    //if (dr.Read())
                    //{
                    //    ultimoNumero = dr.GetInt32(0);
                    //}

                    //int ultimoNumero = 20180000;

                    string ultimoNumero = comSelectLast.ExecuteScalar().ToString();

                    factura.Numero = (int.Parse(ultimoNumero) + 1).ToString("0000000");


                    //"Zona declarativa"
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
                    parUid.Value = usuario.Id;
                    parNumero.Value = factura.Numero;
                    parFecha.Value = factura.Fecha;


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
                            { dr.GetString(0),
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
                        int num = listado.Count();
                        for (int o = 0; o<num; o++)
                        {
                            if (listado[o] != null) //comprobacion exception
                            {
                                    IFactura factura;
                                    IUsuario usuario = new Usuario();
                                    factura = new Factura(usuario);
                                    factura.Numero = listado[o][0].ToString();
                                    factura.Fecha = (DateTime)listado[o][1];
                                    factura.Usuario.Nick = listado[o][7].ToString();
                                    //factura.Usuario.Id = dr.GetInt32(9);

                                    IProducto P = new Producto((int)listado[o][4], listado[o][5].ToString(), (decimal)listado[o][6]);
                                    ILineaFactura L = new LineaFactura(P, (int)listado[o][3]);

                                    lineas.Add(L);
                                if (o > 0)
                                {


                                    //compruebo si seguimos en la misma factura
                                    if (listado[o][0].ToString() == listado[o - 1][0].ToString())
                                    {

                                       
                                       
                                    }
                                    else //hemos cambiado de factura
                                    {
                                        factura.ImportarLineas(lineas);
                                        facturas.Add(factura);
                                    }

                                   
                                   
                                }
                                else
                                {

                                }
                              
                            }
                            }
                              
                           
                        }
















                        


                        return facturas;
                    
                }
                catch (Exception e)
                {
                    throw new AccesoDatosException("No se ha podido buscar todos los facturas", e);
                }
            }
        }
        public void AltaLineas(IFactura factura)
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

                        foreach (ILineaFactura l in factura.LineasFactura)
                        {
                            IDbDataParameter parIdFactura = comSecomInsertLineas.CreateParameter();
                            parIdFactura.ParameterName = "NumeroFactura";
                            parIdFactura.DbType = DbType.String;

                            IDbDataParameter parProducto = comSecomInsertLineas.CreateParameter();
                            parProducto.ParameterName = "IdProducto";
                            parProducto.DbType = DbType.Int16;

                            IDbDataParameter parCantidad = comSecomInsertLineas.CreateParameter();
                            parCantidad.ParameterName = "Cantidad";
                            parCantidad.DbType = DbType.Int16;

                            comSecomInsertLineas.Parameters.Add(parProducto);
                            comSecomInsertLineas.Parameters.Add(parCantidad);
                            comSecomInsertLineas.Parameters.Add(parIdFactura);

                            //"Zona concreta"
                            parIdFactura.Value = factura.Numero;
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
    }
}
