using Rinku.Models;
using Rinku.Models.POCO;
using Rinku.Models.ViewModel;

namespace Rinku.Services.Interfaces
{
    /// <summary>
    /// Interface para los metodos del servicio de nomina
    /// </summary>
    public interface INominaService
    {
        public Task<List<Rol>> GetRols();
        public Task<decimal> InsertEmpleado(EmpleadoViewModel model);
        public Task<EmpleadoRol> GetEmpleado(string numero);
        public Task<decimal> InsertMovimiento(MovimientoViewModel model);
        public Task<NominaDetalle> GetNomina(string numero, int mes);
        public Task<int> GetLastId();
        public Task<decimal> InsertRol(string name);
    }
}
