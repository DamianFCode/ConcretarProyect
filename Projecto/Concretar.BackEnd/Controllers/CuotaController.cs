using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Concretar.Backend.Models;
using Concretar.Helper;
using Concretar.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Concretar.Backend.Controllers
{
    public class CuotaController : CommonController
    {
        private readonly ILogger<CuotaController> _logger;
        private readonly IOptions<AppSettings> _appSettings;
        private readonly IConfiguration _configuration;
        public CuotaController(ILogger<CuotaController> logger, IConfiguration configuration, IOptions<AppSettings> options)
        {
            _logger = logger;
            _appSettings = options;
            _configuration = configuration;
        }

        public IActionResult Index(int ventaId)
        {
            ViewData["AppTitle"] = Parametro.GetValue("AppTitle").ToString();
            VentaService ventaService = new VentaService(_logger);
            var model = ventaService.GetById(ventaId);
            return View(model);
        }
        public IActionResult IndexGrid (int ventaId)
        {
            try
            {
                CuotaService cuotaService = new CuotaService(_logger);
                var model = cuotaService.GetCuotaByVenta(ventaId);
                return PartialView("_IndexGrid", model);
            }
            catch (Exception e)
            {
                SetTempData("Ocurrio un error al obtener las cuotas de la venta", "error");
                _logger.LogError("Ocurrio un error al obtener las cuotas de la venta. Error {0}", e);
                return RedirectToAction("Index", "Venta");
            }
        }

        public IActionResult GetCuota (int cuotaId, int ventaId)
        {
            CuotaService cuotaService = new CuotaService(_logger);
            var model = cuotaService.GetCuota(cuotaId, ventaId);
            return PartialView("_DetalleCuota", model);
        }

        [HttpPost]
        public IActionResult Pagarcuota (int cuotaId, int ventaId)
        {
            try
            {
                CuotaService cuotaService = new CuotaService(_logger);
                cuotaService.PagarCuota(cuotaId, ventaId);
                SetTempData("Cuota Pagada");
                return Ok(JsonConvert.SerializeObject(new { id = cuotaId }));
            }
            catch (Exception e)
            {
                _logger.LogError("Ocurrio un error al pagar la cuota con id <{0}>. Error {1}", cuotaId, e);
                return BadRequest("Ocurrio un error al pagar la cuota");
            }
        }
    }
}