using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Concretar.Backend.Models;
using Concretar.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

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
            CuotaService cuotaService = new CuotaService(_logger);
            var model = cuotaService.GetCuotaByVenta(ventaId);
            return View(model);
        }

        public IActionResult Pagarcuota (int cuotaId, int ventaId)
        {
            CuotaService cuotaService = new CuotaService(_logger);
            cuotaService.PagarCuota(cuotaId, ventaId);
            SetTempData("Cuota Pagada");
            return RedirectToAction("Index", "Cuota", new { ventaId = ventaId });
        }
    }
}