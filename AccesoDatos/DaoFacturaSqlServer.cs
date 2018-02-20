using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaVirtual.Entidades;

namespace TiendaVirtual.AccesoDatos
{
    public class DaoFacturaSqlServer :IDaoFactura
    {
       
            private const string SQL_INSERT = "INSERT INTO facturas (Numero, Fecha, UsuariosId ) VALUES (@Numero, @Fecha, @Uid)";
           
            private string connectionString;

            public DaoFacturaSqlServer(string connectionString)
            {
                this.connectionString = connectionString;
            }

            public void Alta(IFactura factura, int IdUsuario)
            {
                try
                {
                    using (IDbConnection con = new System.Data.SqlClient.SqlConnection(connectionString))
                    {
                        //"Zona declarativa"
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

                    //"Zona concreta"
                        parUid.Value = IdUsuario;
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
        }
}
