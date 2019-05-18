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
		IRepository<Parametro> ParametroRepository {get;}
		IRepository<Permiso> PermisoRepository {get;}
		IRepository<Rol> RolRepository {get;}
		IRepository<RolPermiso> RolPermisoRepository {get;}
		IRepository<Usuario> UsuarioRepository {get;}
		IRepository<UsuarioRol> UsuarioRolRepository {get;}
		IRepository<Vista> VistaRepository {get;}
		IRepository<UsuarioToken> UsuarioTokenRepository {get;}
        IRepository<Cliente> ClienteRepository { get; }
        IRepository<Lote> LoteRepository { get; }
        IRepository<Proyecto> Proyecto { get; }
        int Save();
	}
}