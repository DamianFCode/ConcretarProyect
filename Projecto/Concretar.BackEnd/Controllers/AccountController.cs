using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Concretar.DTO;
using Concretar.Entities;
using Concretar.Helper.Extensions;
using Concretar.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Encrypter = Concretar.Services.Encrypter;
using Parametro = Concretar.Helper.Parametro;
using Microsoft.AspNetCore.Http;

namespace Concretar.Backend.Controllers
{
    public class AccountController : CommonController
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IConfiguration _configuration;
        protected UsuarioService _uservice;
        protected PasswordService _pass;

        public AccountController(ILogger<AccountController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            _uservice = new UsuarioService(_logger);
            _pass = new PasswordService(_logger);
        }

        [TempData]
        public string ErrorMessage { get; set; }

        [TempData]
        public string SuccessMessage { get; set; }
        private IActionResult RedirectReturnUrl(string returnUrl)
        {
            if (string.IsNullOrEmpty(returnUrl))
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return Redirect(returnUrl);
            }
        }

        private HttpStatusCode validateResult(bool result)
        {
            if (result)
            {
                return HttpStatusCode.OK;
            }
            else
            {
                return HttpStatusCode.BadRequest;
            }
        }
        [HttpGet]
        [AllowAnonymous]
        [Route("/")]
        public async Task<IActionResult> Login(string returnUrl = null)
        {
            if (User.Identity.IsAuthenticated)
            {
                var urlBackend = Parametro.GetValue("BaseUrlBackend");
                HttpContext.Session.SetString("urlBackend", urlBackend);
                return RedirectReturnUrl(returnUrl);
            }
            else
            {
                var model = new LoginDTO();
                ViewData["ReturnUrl"] = returnUrl;
                return View(model);
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [Route("/")]
        public async Task<IActionResult> Login(LoginDTO model, Usuario user, string returnUrl = null)
        {

            if (HttpContext.Request.Cookies[CookieAuthenticationDefaults.AuthenticationScheme] != null)
            {
                var siteCookies = HttpContext.Request.Cookies.Where(c => c.Key.StartsWith(CookieAuthenticationDefaults.AuthenticationScheme));
                foreach (var cookie in siteCookies)
                {
                    Response.Cookies.Delete(cookie.Key);
                }
            }
            var pass_noencrypted = model.Contrasena;
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                try
                {
                    var encripted_password = Encrypter.Encryption(model.Contrasena, _configuration.GetSection("3DESKey").Value);
                    model.Contrasena = encripted_password;
                    var passValidate = CheckPasswordTemp(model.Email);
                    var userExist = _uservice.ExistUser(model);
                    if (userExist)
                    {
                        _logger.LogInformation("El usuario Existe.");
                        if (passValidate != HttpStatusCode.OK)
                        {
                            _logger.LogInformation("El usuario logueado con email {0} debe cambiar la contraseña. Se muestra formulario de cambio de contraseña.", model.Email);
                            ViewBag.Usuario = model.Email;
                            return ValidarTokenAndPassTemp(model.Email);
                        }

                        var usuario = Authenticate(model);
                        if (usuario != null)
                        {
                            var claimsCookie = new List<Claim>
                            {
                                new Claim("Name", usuario.Nombre),
                                new Claim("FullName", usuario.Apellido),
                                new Claim("Email", usuario.Email),
                                new Claim("Usuario", usuario.Usuario),
                                new Claim("FotoPerfil", usuario.PathImagenPerfil),
                            };
                            var claimsIdentity = new ClaimsIdentity(
                                claimsCookie, CookieAuthenticationDefaults.AuthenticationScheme);

                            var authProperties = new AuthenticationProperties
                            {
                                IsPersistent = model.Recordarme ? true : false,
                            };

                            await HttpContext.SignInAsync(
                                CookieAuthenticationDefaults.AuthenticationScheme,
                                new ClaimsPrincipal(claimsIdentity),
                                authProperties);
                            _logger.LogInformation("Usuario Logeado.");
                            return RedirectReturnUrl(returnUrl);
                        }
                        else
                        {
                            TempData["ErrorMessage"] = "Usuario o contraseña inválidos";
                        }
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "No existe un usuario con el correo ingresado.";
                    }
                }
                catch (Exception e)
                {
                    _logger.LogErrorException(e, "OCurrió un error al loguear al usuario.");
                    TempData["ErrorMessage"] = "Error al realizar el login.";
                    model.Contrasena = pass_noencrypted;
                }
            }

            model.Contrasena = null;
            return View(model);
        }
        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return View("ForgotPassword", new ForgotPasswordDTO());
        }
        [AllowAnonymous]
        [HttpPost]
        public IActionResult ForgotPassword(ForgotPasswordDTO model)
        {
            try
            {
                var result = ChangeForgottenPassword(model);
                if (result != HttpStatusCode.OK)
                {
                    if (result == HttpStatusCode.NotFound)
                    {
                        _logger.LogInformation("No existe una cuenta de usuario asociada al e-mail {0}", model.Email);
                        SetTempData("No existe una cuenta de usuario asociada al e-mail ingresado. ", "warning");
                        return View("ForgotPassword", model);
                    }
                    else
                    {
                        _logger.LogError("Ocurrio un error al recuperar contraseña para el e-mail {0}", model.Email);
                        SetTempData("No fue posible procesar su solicitud. Intente de nuevo más tarde.", "danger");
                        return View("ForgotPassword", model);
                    }
                }
                else
                {
                    _logger.LogInformation("El Usuario Existe, Token Creado");
                    return View("ForgotPasswordConfirmation", model);
                }
            }
            catch (Exception ex)
            {
                _logger.LogErrorException(ex, "Ocurrió un error al recuperar contraseña para el email {0}.", model.Email);
                SetTempData("No fue posible procesar su solicitud. Intente de nuevo más tarde.", "danger");
                return View("ForgotPassword", model);
            }
        }

        [AllowAnonymous]
        public IActionResult ChangePassByMail(string token)
        {
            _logger.LogInformation("Cambio de contraseña a través del mail para el token {0}", token);
            if (token != null)
            {
                var result = CheckTokenDate(token);
                if (result != HttpStatusCode.OK)
                {
                    _logger.LogInformation("Token de cambio de contraseña a través del mail para el token {0} usado.", token);
                    ViewBag.Motivo = "Token Usado";
                    ViewBag.Content = "No es posible realizar el cambio de contraseña, el token para ésta funcionalidad ya fue utilizado o perdió su validez.";
                    ViewBag.MailSoporte = Helper.Parametro.GetValue("MailSoporte");
                    return View("ErrorToken");
                }

                var tokenUsed = CheckTokenUsed(token);
                if (tokenUsed != HttpStatusCode.OK)
                {
                    _logger.LogError("Ocurrió un error al actualizar la contraseña para el token {0}", token);
                    ViewBag.Motivo = "Error al actualizar datos";
                    ViewBag.Content = "No fue posible realizar el cambio de contraseña.";
                    ViewBag.MailSoporte = Helper.Parametro.GetValue("MailSoporte");
                    return View("ErrorToken");
                }
                _logger.LogInformation("El Token no esta usado y esta vigente");
                return View("ChangePasswordMail");
            }
            else
            {
                _logger.LogInformation("Token de cambio de contraseña a través del mail para el token {0} inexistente.", token);
                ViewBag.Motivo = "Token Vencido";
                ViewBag.Motivo = "Token Inexistente";
                ViewBag.Content = "No es posible realizar el cambio de contraseña, el token no existe o está inutilizable.";
                ViewBag.MailSoporte = Helper.Parametro.GetValue("MailSoporte");
                return View("ErrorToken");
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult ChangePassByMail(ChangePasswordMailDTO model)
        {
            try
            {
                var content = String.Empty;
                var result = String.Empty;
                var tokenUsed = CheckTokenUsed(model.Token);
                if (tokenUsed != HttpStatusCode.OK)
                {
                    _logger.LogInformation("Token de cambio de contraseña a través del mail para el token {0} usado.", model.Token);
                    ViewBag.Motivo = "Token Usado";
                    ViewBag.Content = "No es posible realizar el cambio de contraseña, el token para ésta funcionalidad ya fue utilizado o perdió su validez.";
                    ViewBag.MailSoporte = Helper.Parametro.GetValue("MailSoporte");
                    return View("ErrorToken");
                }

                try
                {
                    result = _pass.ChangePasswordByMail(model.Password, model.Token).ToString();
                }
                catch (Exception e)
                {
                    _logger.LogErrorException(e, "No fue posible cambiar la contraseña. Exception: " + e.Message);
                    ViewBag.Motivo = "Error al actualizar datos";
                    ViewBag.Content = "No fue posible realizar el cambio de contraseña.";
                    ViewBag.MailSoporte = Helper.Parametro.GetValue("MailSoporte");
                    return View("ErrorToken");
                }
                if (result == null)
                {
                    _logger.LogError("Ocurrió un error al actualizar la contraseña para el token {0}", model.Token);
                    ViewBag.Motivo = "Error al actualizar datos";
                    ViewBag.Content = "No fue posible realizar el cambio de contraseña.";
                    ViewBag.MailSoporte = Helper.Parametro.GetValue("MailSoporte");
                    return View("ErrorToken");
                }

                _logger.LogInformation("Cambio de contraseña para el token {0} realizado correctamente.", model.Token);
                ViewBag.Referer = _configuration.GetSection("UrlBackendComercio").Value;
                ViewBag.Motivo = "Cambio de contraseña exitoso";
                ViewBag.Content = "La contraseña ha sido actualizada satisfactoriamente.";
                return View("ChangePasswordSuccess");
            }
            catch (Exception e)
            {
                _logger.LogErrorException(e, "El cambio de contraseña para el token {0} no pudo ser realizado.");
                ViewBag.Motivo = "Error al actualizar datos";
                ViewBag.Content = "No fue posible realizar el cambio de contraseña.";
                ViewBag.MailSoporte = Helper.Parametro.GetValue("MailSoporte");
                return View("ErrorToken");
            }

        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> UpdatePassTemp(UpdatePasswordTempDTO model)
        {
            _logger.LogInformation("Actualización de contraseña temporal para el email {0}. ", model.UserName);
            try
            {
                var result = String.Empty;
                try
                {
                    result = _pass.UpdatePasswordTemp(model.Password, model.UserName).ToString();
                }
                catch (Exception e)
                {
                    _logger.LogErrorException(e, "Ocurrió un error al actualizar la contraseña temporal para el email {0}", model.UserName);
                    ViewBag.Motivo = "Error al actualizar datos";
                    ViewBag.Content = "No fue posible realizar el cambio de contraseña.";
                    ViewBag.MailSoporte = Helper.Parametro.GetValue("MailSoporte");
                    return View("ErrorToken");
                }
                if (result == null)
                {
                    _logger.LogError("Ocurrió un error al actualizar los datos del usuario {0}.", model.UserName);
                    ViewBag.Motivo = "Error al actualizar datos";
                    ViewBag.Content = "No fue posible realizar el cambio de contraseña.";
                    ViewBag.MailSoporte = Helper.Parametro.GetValue("MailSoporte");
                    return View("ErrorToken");
                }

                ViewBag.Referer = _configuration.GetSection("UrlBackendComercio").Value;
                ViewBag.Motivo = "Cambio de contraseña exitoso";
                ViewBag.Content = "La contraseña ha sido actualizada satisfactoriamente.";
                _logger.LogInformation("Cambio de contraseña temporar existoso para el usuario {0}", model.UserName);
                return View("UpdatePasswordTempSuccess");
            }
            catch (Exception e)
            {
                _logger.LogErrorException(e, "Ocurrió un error al actualizar la contraseña temporal para el email {0}", model.UserName);
                ViewBag.Motivo = "Error al actualizar datos";
                ViewBag.Content = "No fue posible realizar el cambio de contraseña.";
                ViewBag.MailSoporte = Helper.Parametro.GetValue("MailSoporte");
                return View("ErrorToken");
            }
        }
        [AllowAnonymous]
        public ActionResult ChangePass()
        {
            ViewData["AppTitle"] = Parametro.GetValue("AppTitle");
            return View("ChangePassword");
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult ChangePass(ChangePasswordDTO model)
        {
            try
            {
                model.Email = User.Claims.FirstOrDefault(x => x.Type == "Usuario").Value;
                _logger.LogInformation("Email del usuario logeado");
                var result = ChangePassword(model);
                if (result != HttpStatusCode.OK)
                {
                    if (model.ActualPassword != null)
                    {
                        _logger.LogError("Ocurrió un error al actualizar la contraseña actual del token {0}", model.Token);
                        SetTempData("No fue posible actualizar sus credenciales.", "error");
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ViewBag.Motivo = "Error al actualizar datos";
                        ViewBag.Content = "No fue posible realizar el cambio de contraseña.";
                        ViewBag.MailSoporte = Helper.Parametro.GetValue("MailSoporte");
                        return View("ErrorToken");
                    }
                }
                if (model.ActualPassword != null)
                {
                    _logger.LogInformation("Contraseña modificada correctamente para el usuario con el token {0}. ", model.Token);
                    SetTempData("Contraseña modificada exitosamente.", "success");
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    _logger.LogInformation("Contraseña actualizada correctamente para el usuario con el token {0}. ", model.Token);
                    ViewBag.Motivo = "Cambio de contraseña exitoso";
                    ViewBag.Content = "La contraseña ha sido actualizada satisfactoriamente.";
                    return View("ChangePasswordSuccess");
                }
            }
            catch (Exception e)
            {
                _logger.LogErrorException(e, "Ocurrió un error al realizar el cambio de contraseña.");
                ViewBag.Motivo = "Error al actualizar datos";
                ViewBag.Content = "No fue posible realizar el cambio de contraseña.";
                ViewBag.MailSoporte = Helper.Parametro.GetValue("MailSoporte");
                return View("ErrorToken");
            }
        }
        private UsuarioDTO Authenticate(LoginDTO model)
        {
            UsuarioDTO usuario = null;
            if (_uservice.ExistUser(model))
            {
                usuario = _uservice.GetUsuario(model);
                _logger.LogInformation("Se encontro el Usuario." + model.Email);
            }
            return usuario;
        }
        [AllowAnonymous]
        [Route("/Logout")]
        public async Task<IActionResult> LogOut()
        {
            _logger.LogInformation("El usuario cerró sesión.");
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
        [AllowAnonymous, HttpPost]
        public HttpStatusCode ChangeForgottenPassword([FromBody]ForgotPasswordDTO model)
        {
            try
            {
                var result = _pass.ChangeForgottenPassword(model);
                if (result)
                {
                    return HttpStatusCode.OK;
                }
                else
                {
                    return HttpStatusCode.NotFound;
                }
            }
            catch (Exception ex)
            {
                return HttpStatusCode.BadRequest;
            }
        }
        public HttpStatusCode CheckTokenDate([FromRoute]string token)
        {
            var result = _pass.CheckTokenDateValid(token);
            return validateResult(result);
        }
        public HttpStatusCode CheckTokenUsed([FromRoute]string token)
        {
            var result = _pass.CheckTokenUsedValid(token);
            return validateResult(result);
        }
        public HttpStatusCode CheckPasswordTemp([FromRoute]string Email)
        {
            var result = _pass.CheckPasswordActualizado(Email);
            return validateResult(result);
        }
        public HttpStatusCode ChangePassword([FromBody]ChangePasswordDTO model)
        {
            var result = _pass.ChangePassword(model);
            return validateResult(result);
        }
        public IActionResult ValidarTokenAndPassTemp(string email)
        {
            var modelUpdatePass = new UpdatePasswordTempDTO();
            modelUpdatePass.UserName = email;
            var result = _pass.ValidarToken(email);
            if (result)
            {
                _logger.LogInformation("El token se encuentra vigente");
                return View("UpdatePasswordTemp", modelUpdatePass);
            }
            else
            {
                _logger.LogInformation("El token Vencido");
                ViewBag.Motivo = "El Token Se Encuentra Vencido";
                ViewBag.Content = "No se puede realizar el cambio de contraseña porque el Token se encuentra vencido";
                ViewBag.MailSoporte = Helper.Parametro.GetValue("MailSoporte");
                return View("ErrorToken");
            }
        }
    }
}