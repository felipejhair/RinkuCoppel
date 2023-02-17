using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Rinku.Helpers;
using Rinku.Models.POCO;
using Rinku.Models.ViewModel;
using Rinku.Services.Interfaces;

namespace Rinku.Controllers
{
    /// <summary>
    /// Contoler encargado de toda la funcionalidad del proyecto
    /// </summary>
    public class NominaController : Controller
    {
        private readonly INominaService _nominaService;
        public NominaController(INominaService nominaService)
        {
            _nominaService = nominaService;
        }

        /// <summary>
        /// Funcion para vista de añadir empleado
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Funcion para añadir empleado, contiene la logica de negocio y llamada a servicios
        /// </summary>
        /// <param name="model">modelo de la vista, contiene el formulario</param>
        /// <returns></returns>
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

        /// <summary>
        /// Funcion para traer datos del empleado
        /// </summary>
        /// <param name="numero">numero de empleado</param>
        /// <returns></returns>
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

        /// <summary>
        /// Funcion para vista de añadir movimiento
        /// </summary>
        /// <returns></returns>
        public IActionResult AddMovement()
        {
            return View();
        }

        /// <summary>
        /// Se encarga de dar de alta el movimiento en la base de datos, llama a los servicos
        /// </summary>
        /// <param name="model">modelo de la vista de añadir movimiento</param>
        /// <returns></returns>
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

        /// <summary>
        /// Trae los datos de la nomina (Selecciona Horas trabajadas, pago total por entregas, 
        /// pago total por bonos, retenciones, vales y sueldo total)
        /// </summary>
        /// <param name="numero">numero de empleado</param>
        /// <param name="mes">mes del cual se sacara la nomina</param>
        /// <returns></returns>
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

        /// <summary>
        /// Funcion para la vista de ver el reporte
        /// </summary>
        /// <returns></returns>
        public IActionResult ViewReport()
        {
            return View();
        }

        /// <summary>
        /// Funcion de la vista para añadir rol
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> AddRol()
        {
            return View();
        }

        /// <summary>
        /// Post para añadir el rol, llama a la capa de servicio
        /// </summary>
        /// <param name="name">Nombre del rol</param>
        /// <returns></returns>
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
