using Concretar.Backend.Models;
using Concretar.Helper;
using Concretar.Services;
using Concretar.Services.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Concretar.Backend.Controllers
{
    public class VentaController : CommonController
    {
        private readonly ILogger<ReunionController> _logger;
        private readonly IOptions<AppSettings> _appSettings;
        private readonly IConfiguration _configuration;
        public VentaController(ILogger<ReunionController> logger, IConfiguration configuration, IOptions<AppSettings> options)
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
        public async Task<IActionResult> GridClientes(string nombre = null, string apellido = null, string dni = null, int? page = null, int? rows = null)
        {
            try
            {
                ClienteService clienteService = new ClienteService(_logger);
                var model = new GridClienteModel();
                var rowsPerPages = _appSettings.Value.Paging.RowsPerPage;
                await Task.Run(() => model = clienteService.GetAll(rowsPerPages, nombre, apellido, dni, page, rows));
                return PartialView("_GridIndex", model);
            }
            catch (Exception e)
            {
                _logger.LogError("Ocurrio un error al listar los clientes-ajax en la venta. Error {0}", e);
                return BadRequest("Ocurrio un error al listar los clientes-ajax en la venta");
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
                return PartialView("_GridLote", model);
            }
            catch (Exception e)
            {
                _logger.LogError("Ocurrio un error al obtener el listado de Lotes-ajax Error {0}", e);
                return BadRequest("Ocurrio un error al obtener el listado de Lotes-ajax");
            }
        }


        public IActionResult Create()
        {
            ViewData["AppTitle"] = Parametro.GetValue("AppTitle").ToString();
            return View();
        }
        public IActionResult Proyecto(int ClienteId)
        {
            ViewData["AppTitle"] = Parametro.GetValue("AppTitle").ToString();
            return View();
        }
        public IActionResult Lote(int ClienteId)
        {
            ViewData["AppTitle"] = Parametro.GetValue("AppTitle").ToString();
            return View();
        }


        public IActionResult Cliente(int id)
        {
            try
            {
                ClienteService clienteService = new ClienteService(_logger);
                ViewData["AppTitle"] = Parametro.GetValue("AppTitle").ToString();
                var model = clienteService.GetById(id);
                _logger.LogInformation("Cliente obtenido para el Id(venta): <{0}>", id);
                return Json(model);
            }
            catch (Exception e)
            {
                _logger.LogError("No se pudo obtener el cliente para el Id(venta): <{0}>", id);
                SetTempData("Ocurrio un error al obtener el cliente(venta)", "error");
                return RedirectToAction("Index", "Venta");
            }
        }
        public IActionResult SearchLote(int id)
        {
            try
            {
                LoteService loteService = new LoteService(_logger);
                ViewData["AppTitle"] = Parametro.GetValue("AppTitle").ToString();
                var model = loteService.GetById(id);
                _logger.LogInformation("Lote obtenido para el Id: <{0}>", id);
                return Json(model);
            }
            catch (Exception e)
            {
                _logger.LogError("No se pudo obtener el Lote para el Id: <{0}>. {1}", id, e);
                SetTempData("Ocurrio un error al obtener el Lote", "error");
                return RedirectToAction("Index", "Lote");
            }
        }
    }
}
