using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Concretar.Entities.Repository.Interface;

namespace Concretar.Entities
{
	public interface IUnitOfWork
	{
		IRepository<Auditoria> AuditoriaRepository {get;}
		IRepository<Parametro> ParametroRepository {get;}
		IRepository<Permiso> PermisoRepository {get;}
		IRepository<Rol> RolRepository {get;}
		IRepository<RolPermiso> RolPermisoRepository {get;}
		IRepository<Usuario> UsuarioRepository {get;}
		IRepository<UsuarioRol> UsuarioRolRepository {get;}
		IRepository<Vista> VistaRepository {get;}
		IRepository<AsuntoFormContacto> AsuntoFormContactoRepository {get;}
		IRepository<FormContacto> FormContactoRepository {get;}
		IRepository<Producto> ProductoRepository {get;}
		IRepository<ArchivoProducto> ArchivoProductoRepository {get;}
		IRepository<FormSolicitudProducto> FormSolicitudProductoRepository {get;}
		IRepository<FormGestionesUsuario> FormGestionesUsuarioRepository {get;}
		IRepository<UsuarioToken> UsuarioTokenRepository {get;}
				int Save();
	}
}