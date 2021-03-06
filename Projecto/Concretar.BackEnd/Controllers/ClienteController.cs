﻿using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Concretar.Helper;
using Concretar.Helper.Extensions;
using Concretar.Services;
using Concretar.Services.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Options;
using Concretar.Backend.Models;

namespace Concretar.Backend.Controllers
{
    public class ClienteController : CommonController
    {
        private readonly ILogger<ClienteController> _logger;
        private readonly IOptions<AppSettings> _appSettings;
        private readonly IConfiguration _configuration;
        public ClienteController(ILogger<ClienteController> logger, IConfiguration configuration, IOptions<AppSettings> options)
        {
            _logger = logger;
            _appSettings = options;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            try
            {
                ClienteService clienteService = new ClienteService(_logger);
                _logger.LogInformation("Listado de clientes obtenido correctamente");
                return View();
            }
            catch (Exception e)
            {
                _logger.LogError("Ocurrio un error al obtener el listado de Clientes. Error {0}", e);
                return BadRequest("Ocurrio un error al obtener el listado de clientes");
            }
        }
        [HttpGet]
        public async Task<IActionResult> GridClientes(string nombre = null, string apellido = null, string dni = null, int? page = null, int? rows = null)
        {
            ClienteService clienteService = new ClienteService(_logger);
            var model = new GridClienteModel();
            var rowsPerPages = _appSettings.Value.Paging.RowsPerPage;
            await Task.Run(() => model = clienteService.GetAll(rowsPerPages, nombre, apellido, dni, page, rows));
            return PartialView("_GridIndex", model);
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
