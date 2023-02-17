using System.Data.SqlClient;
using System.Data;
using Rinku.Data.DAO.Contracts;

namespace Rinku.Data.DAO
{
    public class SQLServer : ISQLServer
    {
        private readonly string connectionString = Helpers.Helpers.GetConfiguration().GetSection("ConnectionStrings")["DefaultConnection"];
        private readonly int timeout = 3600;

        //Metodo para Abrir la Connecion de la BD
        public async Task<bool> OpenAsync(SqlConnection connection)
        {
            try
            {
                if (connection.State == ConnectionState.Closed)
                {
                    await connection.OpenAsync();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        //Metodo para Cerrar la BD
        public async Task CloseAsync(SqlConnection connection)
        {
            if (connection.State == ConnectionState.Open)
            {
                await connection.CloseAsync();
            }
        }

        /// <summary>
        /// Regresa un Dataset con los datos solicitados en el Store Procedure, utilizando un DataAdapter
        /// </summary>
        /// <param name="spName">Nombre del Sp a ejecutar ó script sql</param>
        /// <param name="parameters">Lista de parametros</param>
        /// <param name="typeComman">Tipo de commandos</param>
        /// <returns>dsData con el Resultado</returns>
        public async Task<DataSet> ExecuteDataSetAsync(string spName, List<SqlParameter> parameters, CommandType typeComman)
        {
            DataSet dsData = new DataSet();
            using SqlConnection sqlConn = new SqlConnection(connectionString);
            try
            {
                if (await OpenAsync(sqlConn))
                {
                    using SqlCommand sqlCommand = new SqlCommand(spName, sqlConn)
                    {
                        CommandType = typeComman,
                        CommandTimeout = timeout
                    };
                    if (parameters != null)
                    {
                        //sqlCommand.Parameters.AddRange(parameters.ToArray());
                        foreach (SqlParameter item in parameters)
                        {
                            if (item.Value == null)
                                item.Value = DBNull.Value;
                            sqlCommand.Parameters.Add(item);
                        }
                    }
                    using SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                    sqlDataAdapter.Fill(dsData);
                    await CloseAsync(sqlConn);
                }
                else
                {
                    throw new Exception("No ha sido posible conectarse a la Base de datos.");
                }
                return dsData;
            }
            catch (Exception ex)
            {
                dsData = null;
                dsData.Dispose();
                throw ex;
            }
            finally { await CloseAsync(sqlConn); }
        }

        /// <summary>
        /// Regresa un DataTable con los datos solicitados en el Store Procedure, utilizando un DataAdapter
        /// </summary>
        /// <param name="spName">Nombre del Sp a ejecutar ó script sql</param>
        /// <param name="parameters">Lista de parametros</param>
        /// <param name="typeComman">Tipo de commandos</param>
        /// <returns>dtData con el Resultado</returns>
        public async Task<DataTable> ExecuteDataTableAsync(string spName, List<SqlParameter> parameters, CommandType typeComman)
        {
            DataTable dtData = new DataTable();
            using SqlConnection sqlConn = new SqlConnection(connectionString);
            try
            {
                if (await OpenAsync(sqlConn))
                {
                    using SqlCommand sqlCommand = new SqlCommand()
                    {
                        CommandText = spName,
                        CommandType = typeComman,
                        CommandTimeout = timeout,
                        Connection = sqlConn
                    };
                    if (parameters != null)
                    {
                        //sqlCommand.Parameters.AddRange(parameters.ToArray());
                        foreach (SqlParameter item in parameters)
                        {
                            if (item.Value == null)
                                item.Value = DBNull.Value;
                            sqlCommand.Parameters.Add(item);
                        }
                    }
                    using SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                    sqlDataAdapter.Fill(dtData);
                    await CloseAsync(sqlConn);
                }
                else
                {
                    throw new Exception("No ha sido posible conectarse a la Base de datos.");
                }
                return dtData;
            }
            catch (Exception ex)
            {
                dtData = null;
                dtData.Dispose();
                throw ex;
            }
            finally { await CloseAsync(sqlConn); }
        }

        /// <summary>
        /// Regresa el Numero de registros afectados al ejecutar una de las instrucciones UPDATE, INSERT o DELETE, utilizando sqlCommand
        /// </summary>
        /// <param name="spName">Nombre del Sp a ejecutar ó script sql</param>       
        /// <param name="typeComman">Tipo de commandos</param>
        /// <returns>dtData con el Resultado</returns>
        public async Task<int> ExecuteNonQueryAsync(string spName, CommandType typeComman)
        {
            //Variables de retorno           
            int affectedRecords = 0;
            using SqlConnection sqlConn = new SqlConnection(connectionString);
            try
            {
                if (await OpenAsync(sqlConn))
                {
                    using SqlCommand sqlCommand = new SqlCommand(spName, sqlConn)
                    {
                        CommandType = typeComman,
                        CommandTimeout = timeout
                    };
                    affectedRecords = await sqlCommand.ExecuteNonQueryAsync();
                    await CloseAsync(sqlConn);
                }
                else
                {
                    throw new Exception("No ha sido posible conectarse a la Base de datos.");
                }
                return affectedRecords;
            }
            catch (Exception ex)
            {
                affectedRecords = 0;
                throw ex;
            }
            finally { await CloseAsync(sqlConn); }
        }

        /// <summary>
        /// Regresa el Numero de registros afectados al ejecutar una de las instrucciones UPDATE, INSERT o DELETE, utilizando sqlCommand
        /// </summary>
        /// <param name="spName">Nombre del Sp a ejecutar ó script sql</param>       
        /// <param name="typeComman">Tipo de commandos</param>
        /// <returns>dtData con el Resultado</returns>
        public async Task<int> ExecuteSPNoReturnAsync(string spName, List<SqlParameter> parameters, CommandType typeComman)
        {
            //Variables de retorno           
            int affectedRecords = 0;
            using SqlConnection sqlConn = new SqlConnection(connectionString);
            try
            {
                if (await OpenAsync(sqlConn))
                {
                    using SqlCommand sqlCommand = new SqlCommand(spName, sqlConn)
                    {
                        CommandType = typeComman,
                        CommandTimeout = timeout
                    };
                    if (parameters != null)
                    {
                        sqlCommand.Parameters.AddRange(parameters.ToArray());
                        foreach (SqlParameter item in parameters)
                        {
                            if (item.Value == null)
                            {
                                item.Value = DBNull.Value;
                                sqlCommand.Parameters.Add(item);
                            }
                        }
                    }
                    affectedRecords = await sqlCommand.ExecuteNonQueryAsync();
                    await CloseAsync(sqlConn);
                }
                else
                {
                    throw new Exception("No ha sido posible conectarse a la Base de datos.");
                }
                return affectedRecords;
            }
            catch (Exception ex)
            {
                affectedRecords = 0;
                throw ex;
            }
            finally { await CloseAsync(sqlConn); }
        }

        /// <summary>
        /// Ejecuta la consulta y devuelve la primera columna de la primera fila del conjunto de resultados devuelto por la consulta.
        /// </summary>
        /// <param name="spName">Nombre del Sp a ejecutar ó script sql</param>
        /// <param name="parameters">Lista de parametros</param>
        /// <param name="typeComman">Tipo de commandos</param>
        /// <returns>dtData con el Resultado</returns>
        public async Task<object> ExecuteScalarAsync(string spName, List<SqlParameter> parameters, CommandType typeComman)
        {
            //Variables de retorno
            object result;
            using SqlConnection sqlConn = new SqlConnection(connectionString);
            try
            {

                if (await OpenAsync(sqlConn))
                {
                    using SqlCommand sqlCommand = new SqlCommand(spName, sqlConn)
                    {
                        CommandType = typeComman,
                        CommandTimeout = timeout
                    };
                    if (parameters != null)
                    {
                        //sqlCommand.Parameters.AddRange(parameters.ToArray());
                        foreach (SqlParameter item in parameters)
                        {
                            if (item.Value == null)
                                item.Value = DBNull.Value;
                            sqlCommand.Parameters.Add(item);
                        }
                    }
                    result = await sqlCommand.ExecuteScalarAsync();
                    await CloseAsync(sqlConn);
                }
                else
                {
                    throw new Exception("No ha sido posible conectarse a la Base de datos.");
                }
                return result;
            }
            catch (Exception ex)
            {
                result = null;
                throw ex;
            }
            finally { await CloseAsync(sqlConn); }
        }


        /// <summary>
        /// Proporciona una forma de leer una secuencia de filas sólo hacia delante en una base de datos de SQL Server
        /// Requiere una conneccion Abierta a la Base Datos
        /// </summary>
        /// <param name="spName">Nombre del Sp a ejecutar ó script sql</param>
        /// <param name="parameters">Lista de parametros</param>
        /// <param name="typeComman">Tipo de commandos</param>
        /// <returns>dtData con el Resultado</returns>
        public async Task<SqlDataReader> ExecuteReaderAsync(string spName, List<SqlParameter> parameters, CommandType typeComman)
        {
            //Variables de retorno                     
            SqlDataReader dataReader;
            using SqlConnection sqlConn = new SqlConnection(connectionString);
            try
            {
                if (await OpenAsync(sqlConn))
                {
                    using SqlCommand sqlCommand = new SqlCommand(spName, sqlConn)
                    {
                        CommandType = typeComman,
                        CommandTimeout = timeout
                    };
                    if (parameters != null)
                    {
                        foreach (SqlParameter item in parameters)
                        {
                            if (item.Value == null)
                                item.Value = DBNull.Value;
                            sqlCommand.Parameters.Add(item);
                        }
                    }
                    dataReader = await sqlCommand.ExecuteReaderAsync();

                    await CloseAsync(sqlConn);
                }
                else
                {
                    throw new Exception("No ha sido posible conectarse a la Base de datos.");
                }
                return dataReader;
            }
            catch (Exception ex)
            {
                dataReader = null;
                await dataReader.DisposeAsync();
                throw ex;
            }
            finally { await CloseAsync(sqlConn); }
        }
    }
}
