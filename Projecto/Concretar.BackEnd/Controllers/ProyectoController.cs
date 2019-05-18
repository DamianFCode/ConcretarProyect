using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Concretar.Helper;
using Concretar.Helper.Extensions;
using Concretar.Services;
using Concretar.Services.Models;

namespace Concretar.Backend.Controllers
{
    public class ProyectoController : CommonController
    {
        private readonly ILogger<ProyectoController> _logger;
        private readonly IConfiguration _configuration;
        public ProyectoController(ILogger<ProyectoController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }
        public IActionResult Index()
        {
            try
            {
                ProyectoService proyectoService = new ProyectoService(_logger);
                var model = proyectoService.GetAll();
                _logger.LogInformation("Listado de proyectos obtenido correctamente");
                return View(model);
            }
            catch (Exception e)
            {
                _logger.LogError("Ocurrio un error al obtener el listado de Proyectos. Error {0}", e);
                return BadRequest("Ocurrio un error al obtener el listado de proyectos");
            }
        }
        public IActionResult Create()
        {
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
                return BadRequest("Ocurrio un error al crear el Proyecto");
            }
        }
        public IActionResult Edit(int id)
        {
            try
            {
                ProyectoService proyectoService = new ProyectoService(_logger);
                var model = proyectoService.GetById(id);
                _logger.LogInformation("Proyecto obtenido para el Id: <{0}>", id);
                return View(model);
            }
            catch (Exception e)
            {
                _logger.LogError("No se pudo obtener el Proyecto para el Id: <{0}>", id);
                return BadRequest("Ocurrio un error al obtener el Proyecto");
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
                _logger.LogError("No se pudo editar el cliente. Error <{0}>", e);
                return BadRequest("Ocurrio un error al editar el cliente");
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
                return BadRequest("Ocurrio un error en el proyecto");
            }
        }



    }
}
