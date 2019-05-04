using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Concretar.Entities;
using Concretar.Entities.Repository;
using Concretar.Entities.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Concretar.Entities
{
	public class UnitOfWork: IUnitOfWork, IDisposable
	{
		private readonly ConcretarContext context;
		
		public UnitOfWork()
    {
        var builder = new DbContextOptionsBuilder<ConcretarContext>();

            builder.UseSqlServer(DbConfig.Conexion.GetConnectionString("DefaultConnection"));

            // Stop client query evaluation
            builder.ConfigureWarnings(w => w.Throw(RelationalEventId.QueryClientEvaluationWarning));
            context = new ConcretarContext(builder.Options);
    }
					
						private Repository<Parametro> parametroRepository;
		public IRepository<Parametro> ParametroRepository
		{
			get
			{
				if (this.parametroRepository == null)
				{
					this.parametroRepository = new Repository<Parametro>(context);
				}
				return parametroRepository;
			}
		}
						private Repository<Permiso> permisoRepository;
		public IRepository<Permiso> PermisoRepository
		{
			get
			{
				if (this.permisoRepository == null)
				{
					this.permisoRepository = new Repository<Permiso>(context);
				}
				return permisoRepository;
			}
		}
						private Repository<Rol> rolRepository;
		public IRepository<Rol> RolRepository
		{
			get
			{
				if (this.rolRepository == null)
				{
					this.rolRepository = new Repository<Rol>(context);
				}
				return rolRepository;
			}
		}
						private Repository<RolPermiso> rolPermisoRepository;
		public IRepository<RolPermiso> RolPermisoRepository
		{
			get
			{
				if (this.rolPermisoRepository == null)
				{
					this.rolPermisoRepository = new Repository<RolPermiso>(context);
				}
				return rolPermisoRepository;
			}
		}
						private Repository<Usuario> usuarioRepository;
		public IRepository<Usuario> UsuarioRepository
		{
			get
			{
				if (this.usuarioRepository == null)
				{
					this.usuarioRepository = new Repository<Usuario>(context);
				}
				return usuarioRepository;
			}
		}
						private Repository<UsuarioRol> usuarioRolRepository;
		public IRepository<UsuarioRol> UsuarioRolRepository
		{
			get
			{
				if (this.usuarioRolRepository == null)
				{
					this.usuarioRolRepository = new Repository<UsuarioRol>(context);
				}
				return usuarioRolRepository;
			}
		}
						private Repository<Vista> vistaRepository;
		public IRepository<Vista> VistaRepository
		{
			get
			{
				if (this.vistaRepository == null)
				{
					this.vistaRepository = new Repository<Vista>(context);
				}
				return vistaRepository;
			}
		}
						
						private Repository<UsuarioToken> usuarioTokenRepository;
		public IRepository<UsuarioToken> UsuarioTokenRepository
		{
			get
			{
				if (this.usuarioTokenRepository == null)
				{
					this.usuarioTokenRepository = new Repository<UsuarioToken>(context);
				}
				return usuarioTokenRepository;
			}
		}
        private Repository<Lote> loteRepository;
        public IRepository<Lote> LoteRepository
        {
            get
            {
                if (this.loteRepository == null)
                {
                    this.loteRepository = new Repository<Lote>(context);
                }
                return loteRepository;
            }
        }



        public int Save()
		{
			return context.SaveChanges();
		}

		private bool disposed = false;

		protected virtual void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				if (disposing)
				{
					context.Dispose();
				}
			}
			this.disposed = true;
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

	}
}
