using Microsoft.AspNetCore.Mvc;
using Rinku.Helpers;
using Rinku.Models.POCO;
using Rinku.Models.ViewModel;
using Rinku.Services.Interfaces;

namespace Rinku.Controllers
{
    public class NominaController : Controller
    {
        private readonly INominaService _nominaService;
        public NominaController(INominaService nominaService)
        {
            _nominaService = nominaService;
        }

        public async Task<IActionResult> AddEmploye()
        {
            try
            {
                var id = await _nominaService.GetLastId();
                var roles = await _nominaService.GetRols();
                ViewBag.Roles = roles;
                ViewBag.Number = $"000{id+1}";
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return View();

        }

        [HttpPost]
        public async Task<IActionResult> AddEmploye(EmpleadoViewModel model)
        {
            try
            {
                var roles = await _nominaService.GetRols();
                ViewBag.Roles = roles;
                var insertEmp = await _nominaService.InsertEmpleado(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return View(new EmpleadoViewModel()).WithAlert("success", "Se agrego el empleado con exito");
        }

        [HttpGet]
        public async Task<EmpleadoRol> GetEmpleado(string numero)
        {
            try
            {
                var empleado = await _nominaService.GetEmpleado(numero);
                return empleado;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public IActionResult AddMovement()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddMovement(MovimientoViewModel model)
        {
            try
            {
                var insertMov = await _nominaService.InsertMovimiento(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return View(new MovimientoViewModel()).WithAlert("success", "Se agrego el movimiento con exito");
        }

        [HttpGet]
        public async Task<NominaDetalle> GetNomina(string numero, int mes)
        {
            try
            {
                var nomina = await _nominaService.GetNomina(numero, mes);
                return nomina;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IActionResult> ViewReport()
        {
            return View();
        }

        public async Task<IActionResult> AddRol()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddRol(string name)
        {
            try
            {
                var addRol = await _nominaService.InsertRol(name);
            }
            catch(Exception ex)
            {
                throw ex;
            }

            return View(new RolViewModel()).WithAlert("success", "Se agrego el rol con exito");
        }
    }
}
