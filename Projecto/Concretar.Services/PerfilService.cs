using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using Concretar.Entities;
using Concretar.Helper;
using Concretar.Helper.Exceptions;
using Concretar.Helper.Extensions;
using Concretar.Services.Models;
using Parametro = Concretar.Helper.Parametro;

namespace Concretar.Services
{
    public class PerfilService
    {
        private readonly ILogger _logger;
        readonly UnitOfWork _uow = new UnitOfWork();
        public PerfilService(ILogger logger)
        {
            _logger = logger;
        }
        public List<Rol> GetRoles()
        {
            try
            {
                return _uow.RolRepository.All().OrderBy(r => r.Nombre).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogErrorException(ex, "Ocurrió un error al listar los roles.");
                return new List<Rol>();
            }
        }

        public PerfilModel GetRolModel()
        {
            try
            {
                var permisos = _uow.PermisoRepository.AllIncluding(x => x.Vista).ToList(); 
                var model = new PerfilModel { ListaVistaPermiso = new List<PerfilModel.VistaPermiso>() };
                foreach (var permiso in permisos)
                {
                    model.ListaVistaPermiso.Add(new PerfilModel.VistaPermiso
                    {
                        Permiso = permiso,
                        Activo = false
                    });
                }
                return model;
            }
            catch (Exception ex)
            {
                _logger.LogErrorException(ex, "Ocurrió un error al devolver un modelo del tipo RolModel.");
                return new PerfilModel { ListaVistaPermiso = new List<PerfilModel.VistaPermiso>() };
            }
        }

        public PerfilModel GetRolById(int Id)
        {
            try
            {
                var rol = _uow.RolRepository.FilterIncluding(x => x.RolId == Id, y => y.RolPermisos).FirstOrDefault();
                var permisosAll = _uow.PermisoRepository.AllIncluding(x => x.Vista).ToList();
                PerfilModel model = new PerfilModel
                {
                    RolId = rol.RolId,
                    NombreRol = rol.Nombre,
                    IsPerfilAdmin = rol.Codigo == Enum.GetName(typeof(EstadosHelper.UsuarioDefault), EstadosHelper.UsuarioDefault.ADM) ? true : false,
                };
                model.ListaVistaPermiso = new List<PerfilModel.VistaPermiso>();
                foreach (var permiso in permisosAll)
                {
                    model.ListaVistaPermiso.Add(new PerfilModel.VistaPermiso
                    {
                        Permiso = permiso,
                        Activo = rol.RolPermisos.Any(x => x.PermisoId == permiso.PermisoId)
                    });
                }
                return model;
            }
            catch (Exception ex)
            {
                _logger.LogErrorException(ex, "Ocurrió un error al intentar devolver el rol por id. Id de rol <{0}>", Id);
                return null;
            }
        }

        public List<Permiso> GetPermisos()
        {
            return _uow.PermisoRepository.All().ToList();
        }
        public void CreateRol(PerfilModel model)
        {
            try
            {
                var existe = _uow.RolRepository.CountWhere(x => x.Nombre == model.NombreRol.Trim());
                if (existe > 0)
                {
                    _logger.LogWarning("Ya existe un perfil con el nombre <{0}>", model.NombreRol);
                    throw new ArgumentException("Ya existe un perfil con el mismo nombre.");
                }
                var permisoIdList = model.ListaVistaPermiso.Where(x => x.Activo).Select(x => x.Permiso.PermisoId);
                Rol rol = new Rol { Nombre = model.NombreRol.Trim() };
                var rolNew = _uow.RolRepository.Create(rol);
                foreach (var permisoId in permisoIdList)
                {
                    _uow.RolPermisoRepository.Create(new RolPermiso { RolId = rol.RolId, PermisoId = permisoId });
                }
                _uow.Save();
                _logger.LogInformation("Se creo correctamente el Rol con id <{0}> y con los siguientes id de permisos [{1}].", rolNew.RolId, permisoIdList.Select(x => x).ToString());
            }
            catch (Exception ex)
            {
                _uow.Dispose();
                _logger.LogErrorException(ex, "Ocurrió un error al crear el Rol <{0}>", model.NombreRol);
                throw new CreateRecordException("Ocurrió un error al crear el Rol", ex);
            }
        }

        public void EditRol(PerfilModel model)
        {
            try
            {
                var existe = _uow.RolRepository.CountWhere(x => x.Nombre == model.NombreRol.Trim() && x.RolId != model.RolId);
                if (existe > 0)
                {
                    _logger.LogWarning("Ya existe un perfil con el nombre <{0}>", model.NombreRol);
                    throw new ArgumentException("Ya existe un perfil con el mismo nombre");
                }

                var rol = _uow.RolRepository.Find(x => x.RolId == model.RolId);
                var permisoIdList = model.ListaVistaPermiso.Where(x => x.Activo).Select(x => x.Permiso.PermisoId);
                rol.Nombre = model.NombreRol.Trim();
                List<RolPermiso> RolPermisoList = new List<RolPermiso>();
                _uow.RolRepository.Update(rol);
                foreach (var permisosTipo in _uow.RolPermisoRepository.AllIncluding(x => x.Permiso, g => g.Permiso.Vista))
                {
                    _uow.RolPermisoRepository.Delete(x => x.RolId == rol.RolId);
                }
                foreach (var permisoId in permisoIdList)
                {
                    _uow.RolPermisoRepository.Create(new RolPermiso { RolId = rol.RolId, PermisoId = permisoId });
                }
                _uow.Save();
                _logger.LogInformation("Se editó correctamente el Rol con id <{0}> y con los siguientes id de permisos [{1}].", model.RolId, permisoIdList.Select(x => x).ToString());
            }
            catch (Exception ex)
            {
                _uow.Dispose();
                _logger.LogErrorException(ex, "Ocurrió un error al editar el Rol <{0}>. Id del rol <{1}>.", model.NombreRol, model.RolId);
                throw new UpdateRecordException("Ocurrió un error al editar el Rol", ex);
            }
        }

        
        public bool DeleteRol(int Id)
        {
            var rol = _uow.RolRepository.Find(x => x.RolId == Id);

            if (rol.Codigo != null && rol.Codigo.Equals(EstadosHelper.UsuarioDefault.ADM.ToString()))
            {
                return true;
            }
            
            try
            {
                _uow.RolRepository.Delete(x => x.RolId == Id);
                _uow.RolPermisoRepository.Delete(x => x.RolId == Id);
                _uow.Save();
                _logger.LogInformation("Se eliminó correctamente el Rol con id <{0}>", rol.RolId);
                return false;
            }
            catch (Exception ex)
            {
                _uow.Dispose();
                throw new DeleteRecordException("Ocurrió un error al eliminar el Rol", ex);
            }
        }

        public List<Permiso> GetPermisosByRoleId(int roleId)
        {
            try
            {
                var lista = _uow.RolPermisoRepository.Filter(a => a.RolId == roleId).Select(x => x.Permiso).ToList();
                return lista;
            }
            catch (Exception ex)
            {
                _logger.LogErrorException(ex, "No se pudo obtener la lista de permisos para el Rol con id <{0}>.", roleId);
                return new List<Permiso>();
            }
        }

        
    }
}
