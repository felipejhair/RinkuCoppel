using Rinku.Data.DAO.Contracts;
using Rinku.Models;
using Rinku.Models.POCO;
using Rinku.Models.ViewModel;
using Rinku.Services.Interfaces;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Text.Json;

namespace Rinku.Services
{
    public class NominaService : INominaService
    {
        private readonly ISQLServer _sqlServer;

        public NominaService(ISQLServer sqlServer)
        {
            _sqlServer = sqlServer;
        }

        public async Task<List<Rol>> GetRols()
        {
            List<Rol> rols = new();
            DataTable dtRols = new();

            List<SqlParameter> parameters = new List<SqlParameter>();

            parameters.Clear();

            dtRols = await _sqlServer.ExecuteDataTableAsync("[dbo].[SeleccionarRol]", parameters, CommandType.StoredProcedure);

            if (dtRols != null)
            {
                if (dtRols.Rows.Count != 0)
                {
                    string jsonRols = Helpers.Helpers.DtToJson(dtRols);
                    rols = JsonSerializer.Deserialize<List<Rol>>(jsonRols);
                }
            }

            return rols;
        }

        public async Task<EmpleadoRol> GetEmpleado(string numero)
        {
            EmpleadoRol empleado = new();
            DataTable dtEmpleado = new();

            List<SqlParameter> parameters = new();
            
            parameters.Clear();
            parameters.Add(new SqlParameter("@_Numero", numero));

            dtEmpleado = await _sqlServer.ExecuteDataTableAsync("[dbo].[SelectEmpleado]", parameters, CommandType.StoredProcedure);

            if (dtEmpleado != null)
            {
                if (dtEmpleado.Rows.Count != 0)
                {
                    string jsonEmpleado = Helpers.Helpers.DtToJson(dtEmpleado, true);
                    empleado = JsonSerializer.Deserialize <EmpleadoRol>(jsonEmpleado);
                }
            }

            return empleado;
        }

        public async Task<int> InsertEmpleado(EmpleadoViewModel model)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();

            parameters.Clear();
            parameters.Add(new SqlParameter("@_Nombre", model.Name));
            parameters.Add(new SqlParameter("@_Numero", model.Numero));
            parameters.Add(new SqlParameter("@_RolId", model.RolId));

            var result = await _sqlServer.ExecuteScalarAsync("[dbo].[AddEmpleado]", parameters, CommandType.StoredProcedure);

            return 1;
        }
    }
}
