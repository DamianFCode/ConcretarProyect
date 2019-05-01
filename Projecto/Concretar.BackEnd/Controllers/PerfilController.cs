using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Concretar.Helper;
using Concretar.Services;
using Concretar.Services.Models;

namespace Concretar.Backend.Controllers
{
    public class PerfilController : CommonController
    {
        private readonly ILogger<PerfilController> _logger;
        public PerfilController(ILogger<PerfilController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
        {
            ViewData["AppTitle"] = Parametro.GetValue("AppTitle").ToString();
            PerfilService p = new PerfilService(_logger);
            var list = p.GetRoles();
            return View(list);
        }
        public IActionResult Create()
        {
            ViewData["AppTitle"] = Parametro.GetValue("AppTitle").ToString();
            PerfilService p = new PerfilService(_logger);
            return View(p.GetRolModel());
        }
        [HttpPost]
        public IActionResult Create(PerfilModel model)
        {
            PerfilService p = new PerfilService(_logger);
            if (!String.IsNullOrEmpty(model.NombreRol))
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        p.CreateRol(model);
                        SetTempData("Perfil creado!!");
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return View("Create", model);
                    }
                }
                catch (Exception e)
                {
                    SetTempData(e.Message, "error");
                    return View("Create", model);
                }
            }
            else
            {
                _logger.LogInformation("[ERROR VALIDACION SERVER] Ocurrió un error con los datos ingresados.");
                SetTempData("Cracion de Perfil. Los datos inválidos.", "error");
                return View("Create", model);
            }
        }
        public IActionResult Edit(int Id)
        {
            ViewData["AppTitle"] = Parametro.GetValue("AppTitle").ToString();
            PerfilService p = new PerfilService(_logger);
            var model = p.GetRolById(Id);
            if (model != null)
            {
                return View(model);
            }
            else
            {
                _logger.LogError("Ocurrió un error al intentar editar un perfil que no existe.");
                SetTempData("Error al intentar editar un perfil que no existe.", "error");
                return RedirectToAction("Index", "Perfil");
            }
        }

        [HttpPost]
        public IActionResult Edit(PerfilModel model)
        {
            PerfilService p = new PerfilService(_logger);
            try
            {
                if (ModelState.IsValid)
                {
                    p.EditRol(model);
                    SetTempData("Perfil editado correctamente !");
                    return RedirectToAction("Index");
                }
                else
                {
                    return View("Edit", model);
                }
            }
            catch (Exception e)
            {
                SetTempData(e.Message, "error");
                return View("Edit", model);
            }

        }
        public IActionResult Delete(int Id)
        {
            PerfilService p = new PerfilService(_logger);
            try
            {
                var status = p.DeleteRol(Id);

                if (status)
                {
                    SetTempData("No se puede eliminar rol Administrador.", "error");
                    return RedirectToAction("Index");
                }

                SetTempData("Perfil eliminado!!");
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                SetTempData(e.Message, "error");
                return View("Index");
            }
        }
    }
}