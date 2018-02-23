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
        private const string SELECT_FACTURAS = "SELECT * FROM FACTURAS";
        private const string SQL_INSERT_LINEAS = "INSERT INTO lineasfactura (FacturaId, ProductoId, Cantidad ) VALUES (@NumeroFactura, @ProductoId, @Cantidad)";
        private const string SELECT_LINEAS = "SELECT f.Numero, f.Fecha, f.UsuariosId, l.Cantidad, p.Id, p.Nombre, p.Precio, u.Nick FROM facturas f, lineasfactura l, productos p, usuarios u WHERE f.Id = l.FacturaId AND p.Id = l.ProductoId AND u.Id= f.UsuariosId";

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
                        comSelect.CommandText = SELECT_LINEAS;
                        IDataReader drLin = comSelect.ExecuteReader();
                        //comSelect.CommandText = SELECT_FACTURAS;
                        //IDataReader drFact = comSelect.ExecuteReader();
                        
                        while (drLin.Read())    //lee cada una de las facturas
                        {
                           

                            IFactura factura;
                            IUsuario usuario = new Usuario();
                            factura = new Factura(usuario);

                           
                            factura.Numero = drLin.GetString(0);
                            factura.Fecha = drLin.GetDateTime(1);
                            factura.Usuario.Nick = drLin.GetString(7);
                            

                            IProducto P = new Producto(drLin.GetInt32(4), drLin.GetString(5), drLin.GetDecimal(6));
                            ILineaFactura L = new LineaFactura(P, drLin.GetInt32(3));
                            
                            

                            lineas.Add(L);
                            factura.ImportarLineas(lineas);
                            facturas.Add(factura);
                        }
                        

                       
                        return facturas;
                    }
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
