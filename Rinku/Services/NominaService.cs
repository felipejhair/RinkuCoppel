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

        public async Task<NominaDetalle> GetNomina(string numero, int mes)
        {
            NominaDetalle nomina = new();
            DataTable dtNomina = new();

            List<SqlParameter> parameters = new();

            parameters.Clear();
            parameters.Add(new SqlParameter("@_Mes", mes));
            parameters.Add(new SqlParameter("@_NumeroEmpleado", numero));

            dtNomina = await _sqlServer.ExecuteDataTableAsync("[dbo].[SelectNominaMes]", parameters, CommandType.StoredProcedure);

            if (dtNomina != null)
            {
                if (dtNomina.Rows.Count != 0)
                {
                    string jsonNomina = Helpers.Helpers.DtToJson(dtNomina, true);
                    nomina = JsonSerializer.Deserialize<NominaDetalle>(jsonNomina);
                }
            }

            return nomina;
        }

        public async Task<decimal> InsertEmpleado(EmpleadoViewModel model)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();

            parameters.Clear();
            parameters.Add(new SqlParameter("@_Nombre", model.Name));
            parameters.Add(new SqlParameter("@_Numero", model.Numero));
            parameters.Add(new SqlParameter("@_RolId", model.RolId));

            var result = await _sqlServer.ExecuteScalarAsync("[dbo].[AddEmpleado]", parameters, CommandType.StoredProcedure);

            return Convert.ToDecimal(result);
        }

        public async Task<decimal> InsertMovimiento (MovimientoViewModel model)
        {
            List<SqlParameter> parameters = new();

            parameters.Clear();
            parameters.Add(new SqlParameter("@_Numero", model.Numero));
            parameters.Add(new SqlParameter("@_NumeroEmpleado", model.NombreEmpleado));
            parameters.Add(new SqlParameter("@_NombreRol", model.NombreRol));
            parameters.Add(new SqlParameter("@_Mes", model.Mes));
            parameters.Add(new SqlParameter("@_Entregas", model.Entregas));

            var result = await _sqlServer.ExecuteScalarAsync("[dbo].[InsertMovimiento]", parameters, CommandType.StoredProcedure);

            return Convert.ToDecimal(result);
        }

        public async Task<int> GetLastId()
        {
            List<SqlParameter> parameters = new();

            parameters.Clear();

            var result = await _sqlServer.ExecuteScalarAsync("[dbo].[SelectLastIdEmpleados]", parameters, CommandType.StoredProcedure);

            return Convert.ToInt32(result);
        }

        public async Task<decimal> InsertRol(string name)
        {
            List<SqlParameter> parameters = new();

            parameters.Clear();
            parameters.Add(new SqlParameter("@_Nombre", name));

            var result = await _sqlServer.ExecuteScalarAsync("[dbo].[InsertarRol]", parameters, CommandType.StoredProcedure);

            return Convert.ToInt32(result);
        }
    }
}
