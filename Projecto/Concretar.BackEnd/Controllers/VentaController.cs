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
        private readonly ILogger<VentaController> _logger;
        private readonly IOptions<AppSettings> _appSettings;
        private readonly IConfiguration _configuration;
        public VentaController(ILogger<VentaController> logger, IConfiguration configuration, IOptions<AppSettings> options)
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

        public async Task<IActionResult> GridIndex(string DNI = null, string Lote = null, string Proyecto = null, int? page = null, int? rows = null)
        {
            try
            {
                VentaService ventaService = new VentaService(_logger);
                var rowsPerPages = _appSettings.Value.Paging.RowsPerPage;
                var model = new GridVentaModel();
                await Task.Run(() => model = ventaService.GetAll(rowsPerPages, DNI, Lote, Proyecto, page, rows));
                return PartialView("_GridVenta", model);
            }
            catch (Exception e)
            {
                _logger.LogError("Ocurrio un error al listar los clientes-ajax. Error {0}", e);
                return BadRequest();
            }
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
        public async Task<IActionResult> GridProyecto(string nombre = null, string ubicacion = null, string dimension = null, string precio = null, int? page = null, int? rows = null)
        {
            try
            {
                ProyectoService proyectoService = new ProyectoService(_logger);
                var model = new GridProyectoModel();
                var rowsPerPages = _appSettings.Value.Paging.RowsPerPage;
                await Task.Run(() => model = proyectoService.GetAll(rowsPerPages, nombre, ubicacion, dimension, precio, page, rows));
                return PartialView("_GridProyecto", model);
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
        public IActionResult Create(VentaViewModel ventaModel)
        {
            try
            {
                VentaService ventaService = new VentaService(_logger);
                ventaService.Create(ventaModel);
                SetTempData("Venta Creada.");
                _logger.LogInformation("Venta Creada correctamente");
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                _logger.LogError("Ocurrio un error al crear la venta. Error {0}", e);
                SetTempData("Ocurrio un error al crear la venta", "error");
                return RedirectToAction("Index", "Venta");
            }
        }

        public IActionResult Proyecto(int ClienteId)
        {
            try
            {
                ClienteService clienteService = new ClienteService(_logger);
                var model = clienteService.GetById(ClienteId);
                ViewData["AppTitle"] = Parametro.GetValue("AppTitle").ToString();
                return View(model);
            }
            catch (Exception e)
            {
                _logger.LogError("No se pudo completar la venta");
                SetTempData("No se pudo completar la venta", "error");
                return RedirectToAction("Index", "Venta");
            }

        }
        public IActionResult Lote(int ClienteId)
        {
            try
            {
                ClienteService clienteService = new ClienteService(_logger);
                var model = clienteService.GetById(ClienteId);
                ViewData["AppTitle"] = Parametro.GetValue("AppTitle").ToString();
                return View(model);
            }
            catch (Exception e)
            {
                _logger.LogError("No se pudo completar la venta");
                SetTempData("No se pudo completar la venta", "error");
                return RedirectToAction("Index", "Venta");
            }

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
        public IActionResult SearchProyecto(int id)
        {
            try
            {
                ProyectoService proyectoService = new ProyectoService(_logger);
                ViewData["AppTitle"] = Parametro.GetValue("AppTitle").ToString();
                var model = proyectoService.GetById(id);
                _logger.LogInformation("Lote obtenido para el Id: <{0}>", id);
                return Json(model);
            }
            catch (Exception e)
            {
                _logger.LogError("No se pudo obtener el Lote para el Id: <{0}>. {1}", id, e);
                SetTempData("Ocurrio un error al obtener el Lote", "error");
                return RedirectToAction("Index", "Venta");
            }
        }
        public IActionResult FechaVencimiento(string fechavencimiento, int cantidadcuotas)
        {
            var model = new List<string>();
            try
            {
                for (int i =1; i <= cantidadcuotas; i++)
                {
                    model.Add(Convert.ToDateTime(fechavencimiento).AddMonths(i).ToString("dd/MM/yyyy"));                    
                }
                return Json(model);
            }
            catch (Exception e)
            {
                return BadRequest("Falló la previsualización de la venta");
            }
        }
    }
}
