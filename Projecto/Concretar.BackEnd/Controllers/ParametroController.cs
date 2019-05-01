using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Concretar.Services.Models;
using Concretar.Services;
using Microsoft.Extensions.Logging;
using Concretar.Backend.Controllers;

namespace Concretar.BackEnd.Controllers
{
    public class ParametroController : CommonController
    {      
        private readonly ILogger<ParametroController> _logger;
        protected ParametroService ps;

        public ParametroController(ILogger<ParametroController> logger)
        {
            _logger = logger;
            ps = new ParametroService(_logger);
        }

        public IActionResult Index()
        {
            ViewData["AppTitle"] = Helper.Parametro.GetValue("AppTitle").ToString();
            var model = ps.GetAll();
            return View(model);
        }
        public IActionResult Edit(int id)
        {
            ViewData["AppTitle"] = Helper.Parametro.GetValue("AppTitle").ToString();

            try
            {
                var model = ps.GetById(id);
                return View(model);
            }
            catch
            {
                SetTempData("Error al editar. El Parámetro no existe", "error");
                return RedirectToAction("Index", "Parametro");
            }
            
        }

        [HttpPost]
        public IActionResult Edit(ParametroModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View("Edit", model);
                }

                ps.Edit(model);
                SetTempData("Parámetro editado correctamente");
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                SetTempData(ex.Message, "error");
                return View("Edit", model);
            }
        }
    }
}