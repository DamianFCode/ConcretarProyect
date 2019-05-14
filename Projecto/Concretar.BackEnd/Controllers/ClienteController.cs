using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Concretar.Helper;
using Concretar.Helper.Extensions;
using Concretar.Services;
using Concretar.Services.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Concretar.Backend.Controllers
{
    public class ClienteController : CommonController
    {
        private readonly ILogger<ClienteController> _logger;
        private readonly IConfiguration _configuration;
        public ClienteController(ILogger<ClienteController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            try
            {
                ClienteService clienteService = new ClienteService(_logger);
                var model = clienteService.GetAll();
                _logger.LogInformation("Listado de clientes obtenido correctamente");
                return View(model);
            }
            catch (Exception e)
            {
                _logger.LogError("Ocurrio un error al obtener el listado de Clientes. Error {0}", e);
                return BadRequest("Ocurrio un error al obtener el listado de clientes");
            }
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(ClienteViewModel clienteModel)
        {
            try
            {
                ClienteService clienteService = new ClienteService(_logger);
                clienteService.Create(clienteModel);
                SetTempData("Cliente Creado.");
                _logger.LogInformation("Cliente Creado correctamente");
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                _logger.LogError("Ocurrio un error al crear el cliente. Error {0}", e);
                return BadRequest("Ocurrio un error al crear la marca");
            }
        }
        public IActionResult Edit(int id)
        {
            try
            {
                ClienteService clienteService = new ClienteService(_logger);
                var model = clienteService.GetById(id);
                _logger.LogInformation("Cliente obtenido para el Id: <{0}>", id);
                return View(model);
            }
            catch (Exception e)
            {
                _logger.LogError("No se pudo obtener el cliente para el Id: <{0}>", id);
                return BadRequest("Ocurrio un error al obtener el cliente");
            }
        }
        [HttpPost]
        public IActionResult Edit(ClienteViewModel modelCliente)
        {
            try
            {
                ClienteService clienteService = new ClienteService(_logger);
                clienteService.Edit(modelCliente);
                SetTempData("Cliente Editada.");
                _logger.LogInformation("Cliente Editado Correctamente");
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
                ClienteService clienteService = new ClienteService(_logger);
                clienteService.Delete(id);
                SetTempData("Cliente Eliminado.");
                _logger.LogInformation("Cliente eliminado correctamente");
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                _logger.LogError("No se pudo eliminar el cliente para el Id: <{0}>. Error {1}", id, e);
                return BadRequest("Ocurrio un error el cliente la marca");
            }
        }


    }
}
