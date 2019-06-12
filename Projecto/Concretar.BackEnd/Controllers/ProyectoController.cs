using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Concretar.Helper;
using Concretar.Helper.Extensions;
using Concretar.Services;
using Concretar.Services.Models;
using Concretar.Backend.Models;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace Concretar.Backend.Controllers
{
    public class ProyectoController : CommonController
    {
        private readonly ILogger<ProyectoController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IOptions<AppSettings> _appSettings;
        public ProyectoController(ILogger<ProyectoController> logger, IConfiguration configuration, IOptions<AppSettings> options)
        {
            _logger = logger;
            _appSettings = options;
            _configuration = configuration;
        }
        public IActionResult Index()
        {
            try
            {
                ProyectoService proyectoService = new ProyectoService(_logger);
                ViewData["AppTitle"] = Parametro.GetValue("AppTitle").ToString();
                _logger.LogInformation("Listado de proyectos obtenido correctamente");
                return View();
            }
            catch (Exception e)
            {
                _logger.LogError("Ocurrio un error al obtener el listado de Proyectos. Error {0}", e);
                SetTempData("Ocurrio un error al obtener el listado de proyectos", "error");
                return RedirectToAction("Index", "Home");
            }
        }
        public async Task<IActionResult> GridProyecto(string nombre = null, int? page = null, int? rows = null)
        {
            try
            {
                ProyectoService proyectoService = new ProyectoService(_logger);
                var model = new GridProyectoModel();
                var rowsPerPages = _appSettings.Value.Paging.RowsPerPage;
                await Task.Run(() => model = proyectoService.GetAll(rowsPerPages, nombre, page, rows));
                return PartialView("_GridIndex", model);
            }
            catch (Exception e)
            {
                _logger.LogError("Ocurrio un error al obtener el listado de Proyectos-ajax. Error {0}", e);
                return BadRequest("Ocurrio un error al obtener el listado de proyectos-ajax");
            }
        }

        public IActionResult Create()
        {
            ViewData["AppTitle"] = Parametro.GetValue("AppTitle").ToString();
            return View();
        }
        [HttpPost]
        public IActionResult Create(ProyectoViewModel proyectoModel)
        {
            try
            {
                ProyectoService proyectoService = new ProyectoService(_logger);
                proyectoService.Create(proyectoModel);
                SetTempData("Proyecto Creado.");
                _logger.LogInformation("Proyecto Creado correctamente");
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                _logger.LogError("Ocurrio un error al crear el Proyecto. Error {0}", e);
                SetTempData("Ocurrio un error al crear el Proyecto", "error");
                return RedirectToAction("Index", "Proyecto");
            }
        }
        public IActionResult Edit(int id)
        {
            try
            {
                ProyectoService proyectoService = new ProyectoService(_logger);
                ViewData["AppTitle"] = Parametro.GetValue("AppTitle").ToString();
                var model = proyectoService.GetById(id);
                _logger.LogInformation("Proyecto obtenido para el Id: <{0}>", id);
                return View(model);
            }
            catch (Exception e)
            {
                _logger.LogError("No se pudo obtener el Proyecto para el Id: <{0}>. Error {1}", id, e);
                SetTempData("Ocurrio un error al obtener el Proyecto", "error");
                return RedirectToAction("Index", "Proyecto");
            }
        }
        [HttpPost]
        public IActionResult Edit(ProyectoViewModel proyectoModel)
        {
            try
            {
                ProyectoService proyectoService = new ProyectoService(_logger);
                proyectoService.Edit(proyectoModel);
                SetTempData("Proyecto Editado.");
                _logger.LogInformation("Proyecto Editado Correctamente");
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                _logger.LogError("No se pudo editar el proyecto. Error <{0}>", e);
                SetTempData("Ocurrio un error al editar el proyecto", "error");
                return RedirectToAction("Index", "Proyecto");
            }
        }
        public IActionResult Delete(int id)
        {
            try
            {
                ProyectoService proyectoService = new ProyectoService(_logger);
                proyectoService.Delete(id);
                SetTempData("Proyecto Eliminado.");
                _logger.LogInformation("Proyecto eliminado correctamente");
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                _logger.LogError("No se pudo eliminar el proyecto para el Id: <{0}>. Error {1}", id, e);
                SetTempData("Ocurrio un error al eliminar el proyecto", "error");
                return RedirectToAction("Index", "Proyecto");
            }
        }
    }
}
