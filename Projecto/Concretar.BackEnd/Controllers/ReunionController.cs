using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Concretar.Backend.Models;
using Concretar.Helper;
using Concretar.Services;
using Concretar.Services.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Concretar.Backend.Controllers
{
    public class ReunionController : CommonController
    {
        private readonly ILogger<ReunionController> _logger;
        private readonly IOptions<AppSettings> _appSettings;
        private readonly IConfiguration _configuration;
        public ReunionController(ILogger<ReunionController> logger, IConfiguration configuration, IOptions<AppSettings> options)
        {
            _logger = logger;
            _appSettings = options;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            ViewData["AppTitle"] = Parametro.GetValue("AppTitle").ToString();
            return View();
        }

        public async Task<IActionResult> GridReunion(string FechaCreacionDesde = null, string FechaCreacionHasta = null, int? page = null, int? rows = null)
        {
            try
            {
                ReunionService reunionService = new ReunionService(_logger);
                var model = new GridReunionModel();
                var rowsPerPages = _appSettings.Value.Paging.RowsPerPage;
                await Task.Run(() => model = reunionService.GetAll(rowsPerPages, FechaCreacionDesde, FechaCreacionHasta, page, rows));
                _logger.LogInformation("Listado de reuniones obtenido correctamente");
                return PartialView("_GridIndex", model);
            }
            catch (Exception e)
            {
                _logger.LogError("Ocurrio un error al obtener el listado de reuniones-ajax. Error {0}", e);
                SetTempData("Ocurrio un error al listar las reuniones-ajax", "error");
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            try
            {
                ViewData["AppTitle"] = Parametro.GetValue("AppTitle").ToString();
                ClienteService clienteService = new ClienteService(_logger);
                UsuarioService usuarioService = new UsuarioService(_logger);
                ViewData["Clientes"] = clienteService.GetClientesDropDown();
                ViewData["Usuarios"] = usuarioService.GetUsuariosDropDown();
                return View();
            }
            catch (Exception e)
            {
                _logger.LogError("{0}", e);
                return RedirectToAction("Index", "Reunion");
            }
        }

        [HttpPost]
        public IActionResult Create(ReunionViewModel model)
        {
            try
            {
                ReunionService reunionService = new ReunionService(_logger);
                reunionService.Create(model);
                SetTempData("Reunion creada correctamente", "success");
                return RedirectToAction("Index", "Reunion");
            }
            catch (Exception e)
            {
                _logger.LogError("{0}", e);
                SetTempData("Ocurrio un error al crear la reunion", "error");
                return RedirectToAction("Index", "Reunion");
            }
        }

        [HttpGet]
        public IActionResult Edit (int id)
        {
            try
            {
                ViewData["AppTitle"] = Parametro.GetValue("AppTitle").ToString();
                ReunionService reunionService = new ReunionService(_logger);
                ClienteService clienteService = new ClienteService(_logger);
                UsuarioService usuarioService = new UsuarioService(_logger);
                var model = reunionService.GetById(id);
                ViewData["Clientes"] = clienteService.GetClientesDropDown();
                ViewData["Usuarios"] = usuarioService.GetUsuariosDropDown();
                return View(model);
            }
            catch (Exception e)
            {
                _logger.LogError("{0}", e);
                SetTempData("Ocurrio un error al obtener la reunion", "error");
                return RedirectToAction("Index", "Reunion");
            }
        }

        [HttpPost]
        public IActionResult Edit (ReunionViewModel model)
        {
            try
            {
                ReunionService reunionService = new ReunionService(_logger);
                reunionService.Edit(model);
                SetTempData("Reunion editada correctamente", "success");
                return RedirectToAction("Index", "Reunion");
            }
            catch(Exception e)
            {
                _logger.LogError("{0}", e);
                SetTempData("Ocurrio un error al editar la reunion", "error");
                return RedirectToAction("Index", "Reunion");
            }
        }

        public IActionResult Delete (int id)
        {
            try
            {
                ReunionService reunionService = new ReunionService(_logger);
                reunionService.Delete(id);
                SetTempData("Reunion eliminada correctamente", "success");
                return RedirectToAction("Index", "Reunion");
            }
            catch(Exception e)
            {
                _logger.LogError("{0}", e);
                SetTempData("Ocurrio un error al eliminar la reunion", "error");
                return RedirectToAction("Index", "Reunion");
            }
        }
    }
}