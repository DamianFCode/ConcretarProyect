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
    public class UsuarioController : CommonController
    {
        private readonly ILogger<UsuarioController> _logger;
        private readonly IConfiguration _configuration;

        public UsuarioController(ILogger<UsuarioController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }
        public IActionResult Index()
        {
            ViewData["AppTitle"] = Parametro.GetValue("AppTitle").ToString();
            UsuarioService us = new UsuarioService(_logger);
            return View(us.GetUsuariosAll());
        }

        public IActionResult Create()
        {
            ViewData["AppTitle"] = Parametro.GetValue("AppTitle").ToString();
            UsuarioService us = new UsuarioService(_logger);
                ViewData["Roles"] = us.GetRolesDropDown();
                return View(new UsuarioViewModel());
        }
        [HttpPost]
        public IActionResult Create(UsuarioViewModel model)
        {
            UsuarioService us = new UsuarioService(_logger);
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewData["Roles"] = us.GetRolesDropDown(model.ArrayRoles.Split(',').ToList());
                    return View(model);
                }
                var user = new UsuarioViewModel()
                {
                    Apellido = model.Apellido,
                    Nombre = model.Nombre,
                    Email = model.Email,
                    ArrayRoles = model.ArrayRoles
                };

                var message = us.CreateUsuario(user);
                if (message.Equals("FailModel"))
                {
                    SetTempData("Los datos ingresados son incorrectos.", "error");
                    ViewData["AppTitle"] = Parametro.GetValue("AppTitle").ToString();
                    UsuarioService usS = new UsuarioService(_logger);
                    ViewData["Roles"] = usS.GetRolesDropDown();

                    return View(model);
                }
                else
                {
                    _logger.LogInformation("Usuario Creado Correctamente");
                    SetTempData(message);
                }
            }
            catch (Exception e)
            {
                SetTempData(e.Message, "error");
                _logger.LogErrorException(e, "Ocurrio un error al crear el usuario");
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Edit(UsuarioViewModel model)
        {
            UsuarioService us = new UsuarioService(_logger);
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewData["Roles"] = us.GetRolesDropDown(model.ArrayRoles.Split(',').ToList());
                    return View(model);
                }
                var usuario = new UsuarioViewModel()
                {
                    Apellido = model.Apellido,
                    Nombre = model.Nombre,
                    UsuarioId = model.UsuarioId,
                    ArrayRoles = model.ArrayRoles,
                    Email = model.Email
                };

                var message = us.EditUsuario(usuario);
                if (message.Equals("FailModel"))
                {
                    SetTempData("Los datos ingresados son incorrectos.", "error");
                    ViewData["AppTitle"] = Parametro.GetValue("AppTitle").ToString();
                    UsuarioService usS = new UsuarioService(_logger);
                    ViewData["Roles"] = usS.GetRolesDropDown();

                    return View(model);
                }
                else
                {
                    SetTempData(message);
                }
            }
            catch (Exception e)
            {
                SetTempData(e.Message, "error");
            }
            return RedirectToAction("Index");
        }


        public IActionResult Edit(int id)
        {
            ViewData["AppTitle"] = Parametro.GetValue("AppTitle").ToString();
            try
            {
                UsuarioService us = new UsuarioService(_logger);
                var usuario = us.GetUsuarioById(id);
                var model = new UsuarioViewModel()
                {
                    Apellido = usuario.Apellido,
                    Nombre = usuario.Nombre,
                    UsuarioId = usuario.UsuarioId,
                    Email = usuario.Email,
                    ArrayRoles = usuario.ArrayRoles,
                };

                ViewData["Roles"] = us.GetRolesDropDown(usuario.ArrayRoles.Split(',').ToList());
                return View(model);
            } catch(Exception ex)
            {
                _logger.LogError("", ex.Message);
                SetTempData("Error al intentar editar un usuario que no existe", "error");
                return RedirectToAction("Index", "Usuario");
            }
        
        }

        public IActionResult EmailExists(string email, string usuarioId)
        {
            UsuarioService us = new UsuarioService(_logger);
            var exist = us.UserExist(email, usuarioId);
            return Json(exist ? string.Format("Ya existe un usuario con el email {0}.", email) : "true");
        }

        public IActionResult Delete(int id)
        {
            UsuarioService us = new UsuarioService(_logger);
            try
            {
                var message = us.DeleteUser(id);
                SetTempData(message);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                SetTempData(e.Message, "error");
                return RedirectToAction("Index");
            }
        }
    }
}