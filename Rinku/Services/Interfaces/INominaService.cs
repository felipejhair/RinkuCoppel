using Rinku.Models;
using Rinku.Models.POCO;
using Rinku.Models.ViewModel;

namespace Rinku.Services.Interfaces
{
    public interface INominaService
    {
        public Task<List<Rol>> GetRols();
        public Task<int> InsertEmpleado(EmpleadoViewModel model);
        public Task<EmpleadoRol> GetEmpleado(string numero);
    }
}
