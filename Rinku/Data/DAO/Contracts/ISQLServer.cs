using System.Data;
using System.Data.SqlClient;

namespace Rinku.Data.DAO.Contracts
{
    public interface ISQLServer
    {
        Task<DataSet> ExecuteDataSetAsync(string spName, List<SqlParameter> parameters, CommandType typeComman);
        Task<DataTable> ExecuteDataTableAsync(string spName, List<SqlParameter> parameters, CommandType typeComman);
        Task<int> ExecuteNonQueryAsync(string spName, CommandType typeComman);
        Task<object> ExecuteScalarAsync(string spName, List<SqlParameter> parameters, CommandType typeComman);
        Task<SqlDataReader> ExecuteReaderAsync(string spName, List<SqlParameter> parameters, CommandType typeComman);
        Task<int> ExecuteSPNoReturnAsync(string spName, List<SqlParameter> parameters, CommandType typeComman);
    }
}
