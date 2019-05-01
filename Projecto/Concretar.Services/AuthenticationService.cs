using Concretar.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Concretar.Services
{
    public class AuthenticationService
    {
        readonly UnitOfWork _uow = new UnitOfWork();
        public Usuario GetUsuario(string Email)
        {
            var user = _uow.UsuarioRepository.FilterIncluding(x => x.Email == Email, y => y.UsuarioRoles).FirstOrDefault();
            if (user != null && user.UsuarioRoles.Count > 0)
            {
                return user;
            }
            return null;
        }
        public bool UserExists(string Email)
        {
            var user = _uow.UsuarioRepository.FilterIncluding(x => x.Email == Email, y => y.UsuarioRoles).FirstOrDefault();
            if (user != null && user.UsuarioRoles.Count > 0)
            {
                return true;
            }
            return false;
        }
        public Permiso GetPermiso(string Action, string Controller)
        {
            var permisos = _uow.PermisoRepository.FilterIncluding(x => x.Funcionalidad == Action, y => y.Vista).Where(x => x.Vista.Descripcion == Controller);
            if (permisos.Count() > 0)
            {
                return permisos.FirstOrDefault();
            }
            return null;
        }
        public bool HavePermission(List<int> RolIdList, int PermisoId)
        {
            var permisos = _uow.RolPermisoRepository.Filter(x => RolIdList.Contains(x.RolId) && x.PermisoId == PermisoId);
            if (permisos.Count() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool VerifyAccess(string Email, string Action, string Controller)
        {
            var user = GetUsuario(Email);
            if (user != null)
            {
                var permiso = GetPermiso(Action, Controller);
                if (permiso != null && !HavePermission(user.UsuarioRoles.Select(x => x.RolId).ToList(), permiso.PermisoId))
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
            return true;
        }
    }
}
