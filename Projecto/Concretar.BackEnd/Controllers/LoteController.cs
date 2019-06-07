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
    public class LoteController : CommonController
    {
        private readonly ILogger<LoteController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IOptions<AppSettings> _appSettings;

        public LoteController(ILogger<LoteController> logger, IConfiguration configuration, IOptions<AppSettings> options)
        {
            _logger = logger;
            _appSettings = options;
            _configuration = configuration;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            try
            {
                LoteService loteService = new LoteService(_logger);
                ViewData["AppTitle"] = Parametro.GetValue("AppTitle").ToString();
                _logger.LogInformation("Lotes  obtenido correctamente");
                return View();
            }
            catch (Exception e)
            {
                _logger.LogError("Ocurrio un error al obtener el listado de Lotes. Error {0}", e);
                SetTempData("Ocurrio un error al obtener el listado de Lotes", "error");
                return RedirectToAction("Index", "Home");
            }
        }
        public async Task<IActionResult> GridLote(string nombre = null, string ubicacion = null, string dimension = null, string precio = null, int? page = null, int? rows = null)
        {
            try
            {
                LoteService loteService = new LoteService(_logger);
                var model = new GridLoteModel();
                var rowsPerPages = _appSettings.Value.Paging.RowsPerPage;
                await Task.Run(() => model = loteService.GetAll(rowsPerPages, nombre, ubicacion, dimension, precio, page, rows));
                return PartialView("_GridIndex", model);
            }
            catch (Exception e)
            {
                _logger.LogError("Ocurrio un error al obtener el listado de Lotes-ajax Error {0}", e);
                return BadRequest("Ocurrio un error al obtener el listado de Lotes-ajax");
            }
        }
        public IActionResult Create()
        {
            ProyectoService proyectoService = new ProyectoService(_logger);
            ViewData["AppTitle"] = Parametro.GetValue("AppTitle").ToString();
            ViewData["Proyecto"] = proyectoService.GetProyectoDropDown();
            return View();
        }
        [HttpPost]
        public IActionResult Create(LoteViewModel loteModel)
        {
            try
            {
                LoteService loteService = new LoteService(_logger);
                loteService.Create(loteModel);
                SetTempData("Lote Creado.");
                _logger.LogInformation("Lote Creado correctamente");
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                _logger.LogError("Ocurrio un error al crear el lote. Error {0}", e);
                SetTempData("Ocurrio un error al crear el lote", "error");
                return RedirectToAction("Index", "Lote");
            }
        }
        public IActionResult Edit(int id)
        {
            try
            {
                LoteService loteService = new LoteService(_logger);
                ViewData["AppTitle"] = Parametro.GetValue("AppTitle").ToString();
                var model = loteService.GetById(id);
                ProyectoService proyectoService = new ProyectoService(_logger);
                ViewData["Proyecto"] = proyectoService.GetProyectoDropDown();
                _logger.LogInformation("Lote obtenido para el Id: <{0}>", id);
                return View(model);
            }
            catch (Exception e)
            {
                _logger.LogError("No se pudo obtener el Lote para el Id: <{0}>. {1}", id, e);
                SetTempData("Ocurrio un error al obtener el Lote", "error");
                return RedirectToAction("Index", "Lote");
            }
        }
        [HttpPost]
        public IActionResult Edit(LoteViewModel loteModel)
        {
            try
            {
                LoteService loteService = new LoteService(_logger);
                loteService.Edit(loteModel);
                SetTempData("Lote Editado.");
                _logger.LogInformation("Lote Editado Correctamente");
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                _logger.LogError("No se pudo editar el Lote. Error <{0}>", e);
                SetTempData("Ocurrio un error al editar el Lote", "error");
                return RedirectToAction("Index", "Lote");
            }
        }
        public IActionResult Delete(int id)
        {
            try
            {
                LoteService loteService = new LoteService(_logger);
                loteService.Delete(id);
                SetTempData("Lote Eliminado.");
                _logger.LogInformation("Lote eliminado correctamente");
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                _logger.LogError("No se pudo eliminar el lote para el Id: <{0}>. Error {1}", id, e);
                SetTempData("Ocurrio un error en el lote", "error");
                return RedirectToAction("Index", "Lote");
            }
        }
    }
}
