using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Concretar.Backend.Models;
using Concretar.Helper;
using Concretar.Services;
using System;

namespace Concretar.Backend.Controllers
{
    [Route("[controller]/[action]")]
    public class HomeController : CommonController
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
        {
            ViewData["AppTitle"] = Parametro.GetValue("AppTitle").ToString();
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [HttpGet]
        public ActionResult ListaCumpleanios()
        {
            ClienteService clienteService = new ClienteService(_logger);
            var data = clienteService.GetByBirthday();
            return Ok(Json(data));
        }
        
        [HttpGet]
        public ActionResult ListaReunionesMes()
        {
            ReunionService reunionService = new ReunionService(_logger);
            var data = reunionService.GetByMonth();
            return Ok(Json(data));
        }

    }
}
