using Microsoft.AspNetCore.Mvc;
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
                var roles = await _nominaService.GetRols();
                ViewBag.Roles = roles;
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

            return View();
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

        public async Task<IActionResult> AddMovement()
        {
            return View();
        }

        public async Task<IActionResult> ViewReport()
        {
            return View();
        }

        public async Task<IActionResult> AddRol()
        {
            return View();
        }
    }
}
