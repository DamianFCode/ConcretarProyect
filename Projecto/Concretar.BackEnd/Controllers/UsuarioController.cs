using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Concretar.Helper;
using Concretar.Helper.Extensions;
using Concretar.Services;
using Concretar.Services.Models;
using System.Security.Claims;
using System.IO;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Hosting;

namespace Concretar.Backend.Controllers
{
    public class UsuarioController : CommonController
    {
        private readonly ILogger<UsuarioController> _logger;
        private readonly IConfiguration _configuration;
        private IHostingEnvironment hostingEnv;

        public UsuarioController(ILogger<UsuarioController> logger, IConfiguration configuration, IHostingEnvironment env)
        {
            _logger = logger;
            _configuration = configuration;
            hostingEnv = env;
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
        public IActionResult Edit(UsuarioViewModel model, IFormFile ImagenArchivo)
        {

            //file.SaveAs(path);
            long size = 0;
            var path = hostingEnv.WebRootPath + "\\images";
            if (ImagenArchivo != null)
            {
                var baseFilename = ContentDispositionHeaderValue
                                .Parse(ImagenArchivo.ContentDisposition)
                                .FileName
                                .Trim('"');
                var filename = string.Format("{0}\\{1}", (path).Replace("//", "\\"), baseFilename);
                size += ImagenArchivo.Length;
                using (FileStream fs = System.IO.File.Create(filename))
                {
                    ImagenArchivo.CopyTo(fs);
                    fs.Flush();
                }
                model.PathImagenPerfil = baseFilename;
            }
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

                var message = us.EditUsuario(model);
                if (message.Equals("FailModel"))
                {
                    SetTempData("Los datos ingresados son incorrectos.", "error");
                    ViewData["AppTitle"] = Parametro.GetValue("AppTitle").ToString();
                    UsuarioService usS = new UsuarioService(_logger);
                    ViewData["Roles"] = usS.GetRolesDropDown();

                    return View(model);
                }
                //else
                //{
                //    SetTempData(message);
                //}
            }
            catch (Exception e)
            {
                SetTempData(e.Message, "error");
            }
            return RedirectToAction("LogOut", "Account");
        }


        public IActionResult Edit(int id)
        {
            ViewData["AppTitle"] = Parametro.GetValue("AppTitle").ToString();
            try
            {
                UsuarioService us = new UsuarioService(_logger);
                var usuario = us.GetUsuarioById(id);
                var path = Parametro.GetValue("BaseUrlBackend") + "images/";
                var model = new UsuarioViewModel()
                {
                    Apellido = usuario.Apellido,
                    Nombre = usuario.Nombre,
                    UsuarioId = usuario.UsuarioId,
                    Email = usuario.Email,
                    ArrayRoles = usuario.ArrayRoles,
                    PathImagenPerfil = string.Format("{0}{1}", path, usuario.PathImagenPerfil)
                };

                ViewData["Roles"] = us.GetRolesDropDown(usuario.ArrayRoles.Split(',').ToList());
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError("", ex.Message);
                SetTempData("Error al intentar editar un usuario que no existe", "error");
                return RedirectToAction("Index", "Usuario");
            }

        }
        public IActionResult EditRedirect(string email = null, int? UsuarioId = null)
        {
            try
            {
                UsuarioService usuarioService = new UsuarioService(_logger);
                email = User.Claims.FirstOrDefault(x => x.Type == "Usuario").Value;
                UsuarioId = usuarioService.GetUsuarioByEmail(email);
                _logger.LogInformation("Usuario obtenido para el Id: <{0}>", UsuarioId);
                return RedirectToAction("Edit", "Usuario", new { @id = UsuarioId });
            }
            catch
            {
                _logger.LogError("No se pudo obtener el Usuario");
                return BadRequest("Ocurrio un error al obtener el Usuario");

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