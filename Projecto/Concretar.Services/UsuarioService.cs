using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Concretar.Entities;
using Concretar.DTO;
using Concretar.Helper;
using Concretar.Helper.Extensions;
using Concretar.Helper.Models;
using Concretar.Services.Models;
using Parametro = Concretar.Helper.Parametro;
using Concretar.Helper.Exceptions;
using Concretar.Helper;

namespace Concretar.Services
{
    public class UsuarioService
    {
        private readonly SendNotificacion sender;
        private readonly ILogger _logger;
        readonly UnitOfWork _uow = new UnitOfWork();

        public UsuarioService(ILogger logger)
        {
            _logger = logger;
            sender = new SendNotificacion(_logger);
        }

        public List<Usuario> GetUsuariosAll()
        {
            return _uow.UsuarioRepository.AllIncluding(x => x.UsuarioRoles).OrderBy(x => x.Nombre).ToList();
        }
        public IEnumerable<SelectListItem> GetRolesDropDown(List<string> selected = null)
        {
            var roles = _uow.RolRepository.All().ToList();

            var listRoles = roles.Select(x => new SelectListItem()
            {
                Selected = selected != null ? selected.Any(s => s == x.RolId.ToString()) : false,
                Text = x.Nombre,
                Value = x.RolId.ToString()
            });

            return listRoles;
        }

        public string CreateUsuario(UsuarioViewModel model)
        {
            var rolesList = model.ArrayRoles != null ? model.ArrayRoles.Split(',').ToList() : new List<string>();
            if (ValidarUsuario(model))
            {
                try
                {
                    Random r = new Random();
                    int randNum = r.Next(1000000);
                    string sixDigitNumber = randNum.ToString("D6");
                    var encripted_password = Encrypter.Encryption(sixDigitNumber, Parametro.GetValue("3DESKey"));
                    _logger.LogInformation("Contraseña encriptada correctamente.");
                    var usuario = new Usuario()
                    {
                        Apellido = model.Apellido,
                        Nombre = model.Nombre,
                        Email = model.Email,
                        Contrasena = encripted_password
                    };

                    var result = _uow.UsuarioRepository.Create(usuario);
                    _uow.UsuarioRepository.Save();

                    foreach (var rol in rolesList)
                    {
                        var usuarioRol = new UsuarioRol()
                        {
                            RolId = int.Parse(rol),
                            UsuarioId = result.UsuarioId
                        };
                        _uow.UsuarioRolRepository.Create(usuarioRol);
                        _uow.UsuarioRolRepository.Save();
                    }
                    var tokenGuid = Guid.NewGuid().ToString();
                    var token = new UsuarioToken()
                    {
                        UsuarioId = result.UsuarioId,
                        FechaExpiracion = DateTime.Now.AddMinutes(Convert.ToDouble(Helper.Parametro.GetValue("TiempoExpiracionTokenMail"))),
                        Usado = false,
                        Token = tokenGuid
                    };
                    MailUsuarioModel mailModel = new MailUsuarioModel()
                    {
                        TokenUrl = tokenGuid,
                        Password = sixDigitNumber,
                        UsuarioApellido = result.Apellido,
                        UsuarioNombre = result.Nombre,
                        Email = result.Email,
                        Usuario = result.Email,
                        EsAdministrador = _uow.UsuarioRolRepository.AllIncluding(x => x.Rol).Any(x => x.UsuarioId == result.UsuarioId && x.Rol.Codigo == Helper.EstadosHelper.UsuarioDefault.ADM.ToString())
                    };

                    sender.SendMailNewUsuario(mailModel);
                    var tokenCreated = _uow.UsuarioTokenRepository.Create(token);
                    _uow.UsuarioRepository.Save();
                    _logger.LogInformation("Envio de email correctamente");
                    _logger.LogInformation("Usuario creado correctamente. Nombre de usuario: <{0}> - Roles asignados: <{1}>.",model.Nombre + " " + model.Apellido, rolesList);
                    return "Usuario creado correctamente.";
                }
                catch (Exception ex)
                {
                    _logger.LogErrorException(ex,
                        "Ocurrió un error al crear usuario. Nombre de usuario: <{0}> - Roles asignados: <{1}>.",
                        model.Nombre + " " + model.Apellido, rolesList.ToString());
                    throw new CreateRecordException("Ocurrió un error al crear el usuario", ex);
                }
            }
            else
            {
                _logger.LogError(
                    "[ERROR VALIDACION EN SERVER] - Ocurrió un error al crear usuario. Uno o mas datos son incorrectos.");
                return "FailModel";
            }
        }


        public UsuarioViewModel GetUsuarioById(int id)
        {
            var user = _uow.UsuarioRepository.Find(x => x.UsuarioId == id);
            var roles = _uow.UsuarioRolRepository.Filter(x => x.UsuarioId == id).Select(x => x.RolId);
            var rolesArray = String.Empty;
            foreach (var rol in roles)
            {
                rolesArray = rolesArray + rol.ToString() + ",";
            }

            var sitiosArray = String.Empty;

            return new UsuarioViewModel()
            {
                Apellido = user.Apellido,
                Nombre = user.Nombre,
                UsuarioId = user.UsuarioId,
                Email = user.Email,
                ArrayRoles = rolesArray
            };
        }

        public bool UserExist(string email, string usuarioId)
        {
            if (usuarioId != null)
            {
                var uId = int.Parse(usuarioId);
                return _uow.UsuarioRepository.All().Any(x => (x.Email == email || x.Email == email.ToUpper() || x.Email == email.ToLower()) && !(x.UsuarioId == uId));
            }
            return _uow.UsuarioRepository.All().Any(x => x.Email == email || x.Email == email.ToUpper() || x.Email == email.ToLower());
        }

        public string DeleteUser(int id)
        {
            try
            {
                var usuario = _uow.UsuarioRepository.Find(x => x.UsuarioId == id);
                var rolesList = _uow.UsuarioRolRepository.Filter(x => x.UsuarioId == id);

                foreach (var rol in rolesList)
                {
                    _uow.UsuarioRolRepository.Delete(rol);
                }

                _uow.UsuarioRepository.Delete(usuario);
                _uow.Save();

                _logger.LogInformation("Usuario con id <{0}>, eliminado correctamente.", id);
                return "Usuario eliminado correctamente";
            }
            catch (Exception ex)
            {
                _uow.Dispose();
                _logger.LogErrorException(ex, "Ocurrió un problema al intentar eliminar el Usuario con ID <{0}>.", id);
                throw new DeleteRecordException("Ocurrió un error al eliminar el usuario", ex);
            }
        }

        public string EditUsuario(UsuarioViewModel model)
        {
            if (ValidarUsuario(model))
            {
                try
                {
                    var usuario = _uow.UsuarioRepository.Find(x => x.UsuarioId == model.UsuarioId);
                    usuario.Apellido = model.Apellido;
                    usuario.Email = model.Email;
                    usuario.Nombre = model.Nombre;

                    var rolesList = _uow.UsuarioRolRepository
                        .FilterIncluding(x => x.UsuarioId == model.UsuarioId, y => y.Rol, z => z.Usuario).ToList();

                    foreach (var rol in rolesList)
                    {
                        _uow.UsuarioRolRepository.Delete(rol);
                    }

                    _uow.UsuarioRepository.Update(usuario);
                    var rolesListNew = model.ArrayRoles != null
                        ? model.ArrayRoles.Split(',').ToList()
                        : new List<string>();

                    foreach (var rol in rolesListNew)
                    {
                        var usuarioRol = new UsuarioRol()
                        {
                            RolId = int.Parse(rol),
                            UsuarioId = model.UsuarioId
                        };
                        _uow.UsuarioRolRepository.Create(usuarioRol);
                    }

                    _uow.Save();
                    _logger.LogInformation("Se editó correctamente el Usuario <{0} con el ID <{1}>",
                        model.Nombre + " " + model.Apellido, model.UsuarioId);
                    return "Usuario editado correctamente";
                }
                catch (Exception ex)
                {
                    _uow.Dispose();
                    _logger.LogErrorException(ex, "Ocurrió un problema al editar el Usuario <{0}> con el ID <{1}>",
                        model.Nombre + " " + model.Apellido, model.UsuarioId);
                    throw new UpdateRecordException("Ocurrió un error al editar el usuario", ex);
                }
            }
            else
            {
                _logger.LogError(
                    "[ERROR VALIDACION EN SERVER] - Ocurrió un error al editar un usuario. Uno o mas datos son incorrectos.");
                return "FailModel";
            }
        }

        public bool ValidarUsuario(UsuarioViewModel model)
        {
            var nombre = (model.Nombre);
            var apellido = (model.Apellido);
            var email = (model.Email).Trim();
            var roles = (model.ArrayRoles);

            if (String.IsNullOrEmpty(nombre))
            {
                return false;
            }

            if (String.IsNullOrEmpty(apellido))
            {
                return false;
            }

            if (!ValidarEmail(email))
            {
                return false;
            }

            if (String.IsNullOrEmpty(roles))
            {
                return false;
            }

            return true;
        }

        private bool ValidarEmail(String email)
        {
            String expresion;
            expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
            if (Regex.IsMatch(email, expresion))
            {
                if (Regex.Replace(email, expresion, String.Empty).Length == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        public bool ExistUser(LoginDTO user)
        {
            return _uow.UsuarioRepository.All().Any(x => x.Email == user.Email && x.Contrasena == user.Contrasena && x.Habilitado);
        }
        public UsuarioDTO GetUsuario(LoginDTO login)
        {
            var usuario = _uow.UsuarioRepository.Find(x => x.Email == login.Email && x.Contrasena == login.Contrasena && x.Habilitado);
            return new UsuarioDTO()
            {
                Nombre = usuario.Nombre,
                Apellido = usuario.Apellido,
                Usuario = usuario.Email,
                Password = usuario.Contrasena,
                Email = usuario.Email,
            };
        }

        public IEnumerable<SelectListItem> GetUsuariosDropDown(string selectedId = null)
        {
            var usuarios = _uow.UsuarioRepository.All().OrderBy(x => x.Nombre).ToList();

            var listUsuarios = usuarios.Select(x => new SelectListItem()
            {
                Selected = (x.UsuarioId.ToString() == selectedId),
                Text = x.Nombre + " " + x.Apellido,
                Value = x.UsuarioId.ToString()
            }).OrderBy(x => x.Text);

            return listUsuarios;
        }
    }
}
