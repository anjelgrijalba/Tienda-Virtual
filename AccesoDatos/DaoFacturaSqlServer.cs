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
        private const string SQL_SELECT = "SELECT f.Numero, f.Fecha,f. UsuariosId, l.productoId, l.Cantidad FROM facturas f, lineasfactura l WHERE  f.id = l.FacturaId";
        private const string SQL_INSERT_LINEAS = "INSERT INTO lineasfactura (FacturaId, ProductoId, Cantidad ) VALUES (@NumeroFactura, @ProductoId, @Cantidad)";

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

                        IFactura factura;

                        while (dr.Read())
                        {

                            IUsuario usuario = new Usuario();
                            factura = new Factura(usuario);

                            factura.Id = dr.GetInt32(0);
                            factura.Numero = dr.GetString(1);
                            factura.Fecha = dr.GetDateTime(2);
                            factura.Usuario.Id = dr.GetInt32(3);


                           
                            


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
