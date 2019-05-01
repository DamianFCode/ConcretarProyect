using Microsoft.Extensions.Logging;
using Concretar.DTO;
using Concretar.Entities;
using Concretar.Helper;
using Concretar.Helper.Exceptions;
using Concretar.Helper.Extensions;
using Concretar.Helper.Models;
using System;
using System.Linq;

namespace Concretar.Services
{
    public class PasswordService
    {
        readonly SendNotificacion sender;
        private readonly ILogger _logger;
        readonly UnitOfWork _uow = new UnitOfWork();

        public PasswordService(ILogger logger)
        {
            _logger = logger;
            sender = new SendNotificacion(_logger);
        }
        public bool TokenExpiration(string token)
        {
            var result = _uow.UsuarioTokenRepository.Find(x => x.Token == token && x.FechaExpiracion >= DateTime.Now && x.Usado == false);
            if (result != null)
            {
                result.Usado = true;
                _uow.UsuarioTokenRepository.Update(result);
                _uow.UsuarioTokenRepository.Save();
                _logger.LogInformation("[Backend-Usuario] - Token con id <{0}>, expirado correctamente para cambio de contraseña.", result.UsuarioId);
                return true;
            }
            else
            {
                _logger.LogInformation("[Backend-Usuario] - Token GUID <{0}> Vencido", token);
                _logger.LogError("[Backend-Usuario] - Token GUID <{0}>, no se pudo expirar.", token);
                return false;
            }
        }
        public bool CheckPasswordActualizado(string Email)
        {
            var result = _uow.UsuarioRepository.Find(x => x.Email == Email && x.ContrasenaActualizada == true);
            if (result != null)
            {
                return true;
            }
            else
            {
                _logger.LogError("[Backend-Usuario] - Contraseña temporal del usuario <{0}> aún no ha sido actualizada.", Email);
                return false;
            }
        }
        public LoginDTO UpdatePasswordTemp(string password, string userName)
        {
            try
            {
                var encripted_password = Encrypter.Encryption(password, Helper.Parametro.GetValue("3DESKey"));
                var usuario = _uow.UsuarioRepository.Find(x => x.Email == userName);
                usuario.TSModificado = DateTime.Now;
                usuario.Contrasena = encripted_password;
                usuario.ContrasenaActualizada = true;

                var token = _uow.UsuarioTokenRepository.Filter(x => x.UsuarioId == usuario.UsuarioId && x.Usado == false && x.FechaExpiracion >= DateTime.Now).FirstOrDefault().Token;
                TokenExpiration(token);

                _uow.UsuarioRepository.Update(usuario);
                _uow.UsuarioRepository.Save();
                _logger.LogInformation("[Backend-Usuario] - Contraseña de UsuarioComercio ID<{0}>, actualizada correctamente.", usuario.UsuarioId);

                LoginDTO result = new LoginDTO
                {
                    Contrasena = password,
                    Email = usuario.Email,
                    Recordarme = false
                };
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogErrorException(ex, "[Backend-Usuario] - Ocurrió un problema al intentar actualizar password temporal para usuario: " + userName);
                throw;
            }
        }
        public bool ChangePassword(ChangePasswordDTO model)
        {
            try
            {
                var encripted_newpassword = Encrypter.Encryption(model.NewPassword, Helper.Parametro.GetValue("3DESKey"));
                var encripted_actualpassword = Encrypter.Encryption(model.ActualPassword, Helper.Parametro.GetValue("3DESKey"));

                var usuario = _uow.UsuarioRepository.Find(x => x.Email == model.Email && x.Contrasena == encripted_actualpassword);
                usuario.TSModificado = DateTime.Now;
                usuario.Contrasena = encripted_newpassword;
                _uow.UsuarioRepository.Update(usuario);
                _uow.UsuarioRepository.Save();
                _logger.LogInformation("[Backend-Usuario] - Contraseña de Usuario <{0}>, actualizada correctamente.", model.Email);
                return true;

            }
            catch (Exception ex)
            {
                _logger.LogErrorException(ex, "[Backend-Usuario] - Ocurrió un problema al intentar actualizar clave de usuario.");
                return false;
            }
        }
        public bool ChangeForgottenPassword(ForgotPasswordDTO model)
        {
            var user = _uow.UsuarioRepository.Find(x => x.Email == model.Email);
            if (user != null)
            {
                //Envío mail pass temporal y link para cambio...
                try
                {
                    var tokenGuid = Guid.NewGuid().ToString();
                    var token = new UsuarioToken()
                    {
                        UsuarioId = user.UsuarioId,
                        FechaExpiracion = DateTime.Now.AddMinutes(Convert.ToDouble(Helper.Parametro.GetValue("TiempoExpiracionTokenMail"))),
                        Usado = false,
                        Token = tokenGuid
                    };

                    var mailModel = new MailUsuarioModel()
                    {
                        TokenUrl = tokenGuid,
                        UsuarioApellido = user.Apellido,
                        UsuarioNombre = user.Nombre,
                        Email = user.Email
                    };

                    //Envio mail de contraseña olvidada...
                    sender.SendMailForgotPassword(mailModel);

                    _uow.UsuarioTokenRepository.Create(token);
                    _uow.UsuarioTokenRepository.Save();
                    _logger.LogInformation("Envio de email correctamente");
                    return true;
                }
                catch (Exception ex)
                {
                    _logger.LogErrorException(ex, "[Token-Mail] - Ocurrió un error al intentar enviar EMAIL para restablecer contraseña olvidada.");
                    throw;
                }
            }
            else
            {
                return false;
            }
        }
        public bool CheckTokenDateValid(string token)
        {
            var result = _uow.UsuarioTokenRepository.Find(x => x.Token == token && x.FechaExpiracion >= DateTime.Now);
            if (result != null)
            {
                _logger.LogInformation("[Backend-Usuario] - Token GUID <{0}> de cambio de contraseña esta vigente.", token);
                return true;
            }
            else
            {
                _logger.LogError("[Backend-Usuario] - Token GUID <{0}> de cambio de contraseña se encuentra vencido.", token);
                return false;
            }
        }
        public bool CheckTokenUsedValid(string token)
        {
            var result = _uow.UsuarioTokenRepository.Find(x => x.Token == token && x.Usado == false);
            if (result != null)
            {
                _logger.LogInformation("[Backend-Usuario] - Token GUID <{0}> de cambio de contraseña no esta en uso.", token);
                return true;
            }
            else
            {
                _logger.LogError("[Backend-Usuario] - Token GUID <{0}> de cambio de contraseña ya ha sido usado.", token);
                return false;
            }
        }
        
        public LoginDTO ChangePasswordByMail(string password, string token)
        {
            try
            {
                var encripted_password = Encrypter.Encryption(password, Helper.Parametro.GetValue("3DESKey"));

                var userId = _uow.UsuarioTokenRepository.Find(x => x.Token == token).UsuarioId;
                var usuario = _uow.UsuarioRepository.Find(x => x.UsuarioId == userId);
                usuario.TSModificado = DateTime.Now;
                usuario.Contrasena = encripted_password;
                usuario.ContrasenaActualizada = true;
                _uow.UsuarioRepository.Update(usuario);
                _uow.UsuarioRepository.Save();
                TokenExpiration(token);
                _logger.LogInformation("[Backend-Usuario] - Contraseña de UsuarioComercio ID<{0}>, actualizada correctamente.", userId);
                LoginDTO result = new LoginDTO
                {
                    Contrasena = password,
                    Email = usuario.Email,
                    Recordarme = false
                };
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogErrorException(ex, "[Backend-Usuario] - Ocurrió un problema al intentar actualizar con token: " + token);
                throw new UpdateRecordException("Ocurrió un problema al intentar actualizar la contraseña", ex);
            }
        }

        public bool ValidarToken(string email)
        {
            try
            {
                var usuario = _uow.UsuarioRepository.Find(x => x.Email == email).UsuarioId;
                var result =
                    _uow.UsuarioTokenRepository.Find(x => x.UsuarioId == usuario && x.FechaExpiracion >= DateTime.Now);
                if (result != null)
                {
                    _logger.LogInformation("[Backend-Usuario] - Token GUID <{0}> se encuentra vigente.", result.Token);
                    return true;
                }
                else
                {
                    _logger.LogError("[Backend-Usuario] - Token GUID <{0}> esta Vencido.");
                    return false;
                }
            }
            catch (Exception e)
            {
                _logger.LogErrorException(e, "[Backend-Usuario] - Ocurrio un problema al obtener el Token: ");
                return false;
            }
        }
    }
}
