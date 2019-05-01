using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using Concretar.Entities.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Concretar.Entities.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
    {
        protected Concretar.Entities.ConcretarContext Context = null;

        public Repository(Concretar.Entities.ConcretarContext context)
        {
            Context = context;
        }

        public DbSet<TEntity> DbSet
        {
            get
            {
                return (DbSet<TEntity>)Context.Set<TEntity>();
            }
        }

        public DbSet<TEntity> GetDbSet()
        {
            return (DbSet<TEntity>)Context.Set<TEntity>();
        }

        public virtual IQueryable<TEntity> All()
        {
            var name = typeof(TEntity);
            return DbSet.AsQueryable();
        }

        public virtual IQueryable<TEntity> AllIncluding(params Expression<Func<TEntity, object>>[] includeProperties)
        {

            IQueryable<TEntity> queryable = All();
            foreach (Expression<Func<TEntity, object>> includeProperty in includeProperties)
            {

                queryable = queryable.Include<TEntity, object>(includeProperty);
            }

            return queryable;
        }

        public virtual IQueryable<TEntity> FilterIncluding(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> queryable = Filter(predicate);
            foreach (Expression<Func<TEntity, object>> includeProperty in includeProperties)
            {

                queryable = queryable.Include<TEntity, object>(includeProperty);
            }

            return queryable;
        }

        public virtual IQueryable<TEntity> Filter(Expression<Func<TEntity, bool>> predicate)
        {
            var name = typeof(TEntity);
            return DbSet.Where(predicate).AsQueryable<TEntity>();
        }

        public virtual IQueryable<TEntity> Filter<Key>(Expression<Func<TEntity, bool>> filter, out int total, int index = 0, int size = 50)
        {
            int skipCount = index * size;
            var name = typeof(TEntity);
            var _resetSet = filter != null ? DbSet.Where(filter) : DbSet;
            _resetSet = skipCount == 0 ? _resetSet.Take(size) : _resetSet.AsEnumerable().Skip(skipCount).Take(size).AsQueryable();
            total = _resetSet.Count();
            return _resetSet;
        }

        public bool Contains(Expression<Func<TEntity, bool>> predicate)
        {
            var name = typeof(TEntity);
            return DbSet.Count(predicate) > 0;
        }

        public virtual TEntity Find(Expression<Func<TEntity, bool>> predicate)
        {
            var name = typeof(TEntity);
            return DbSet.FirstOrDefault(predicate);
        }

        public virtual TEntity Create(TEntity t)
        {
            try
            {
                if (UtilRepo.HasMember(t, "Habilitado"))
                {

                    if (UtilRepo.HasMember(t, "TSCreate"))
                    {
                        Context.Entry(t).Member("TSCreate").CurrentValue = DateTime.Now;
                        Context.Entry(t).Member("Habilitado").CurrentValue = true;
                    }
                    else
                    {
                        Context.Entry(t).Member("Habilitado").CurrentValue = true;
                    }
                   
                }
                var newEntry = DbSet.Add(t);
                return newEntry.Entity;
            }
            catch
            {
                return null;
            }


        }

        public virtual void Delete(TEntity t, bool? forzarDelete = null)
        {
            if (Context.Entry(t).State == EntityState.Detached)
            {
                DbSet.Attach(t);
            }
            if (forzarDelete != null)
            {
                if (forzarDelete.Value)
                {
                    DbSet.Remove(t);
                }
            }
            else
            {
                if (UtilRepo.HasMember(t, "Habilitado"))
                {
                    this.Disabled(t);
                }
                else
                {
                    DbSet.Remove(t);
                }
            }
        }

        public virtual void Delete(object id)
        {
            TEntity entity = DbSet.Find(id);
            DbSet.Remove(entity);
        }

        public virtual void Delete(Expression<Func<TEntity, bool>> predicate)
        {
            var objects = Filter(predicate);
            foreach (var obj in objects)
            {
                DbSet.Remove(obj);
            }
            
        }

        public virtual void Update(TEntity t)
        {
            if (UtilRepo.HasMember(t, "TSModificado"))
            {
                DbSet.Attach(t);
                Context.Entry(t).Member("TSModificado").CurrentValue = DateTime.Now;
                Context.Entry(t).State = EntityState.Modified;
            }
            else
            {
                DbSet.Attach(t);
                Context.Entry(t).State = EntityState.Modified;
            }     
        }

        public virtual void DeleteAndCreate(Expression<Func<TEntity, bool>> predicateToDelete, List<TEntity> itemListToCreate)
        {
            Delete(predicateToDelete);
            DateTime now = DateTime.Now;
            foreach (var item in itemListToCreate)
            {
                var newEntry = DbSet.Add(item);
                var entityName = typeof(TEntity).Name;
            }
        }

        //Esta función desabilita un registro dado
        public virtual void Disabled(TEntity TEntity)
        {
            if (UtilRepo.HasMember(TEntity, "TSEliminado"))
            {
                DbSet.Attach(TEntity);
                Context.Entry(TEntity).Member("Habilitado").CurrentValue = false;
                Context.Entry(TEntity).Member("TSEliminado").CurrentValue = DateTime.Now;
                Context.Entry(TEntity).State = EntityState.Modified;
            }
            else
            {
                DbSet.Attach(TEntity);
                Context.Entry(TEntity).Member("Habilitado").CurrentValue = false;
                Context.Entry(TEntity).State = EntityState.Modified;
            }
        }

        public virtual int Count
        {
            get
            {
                var name = typeof(TEntity);
                if (DbSet.Count() > 0)
                {
                    return DbSet.Count();
                }
                else
                {
                    return 0;
                }
            }
        }

        public virtual int CountWhere(Expression<Func<TEntity, bool>> predicate)
        {
            var name = typeof(TEntity);
            return DbSet.Count(predicate);
        }

        public virtual TEntity Last()
        {
            return All().ToList().Last();
        }

        public int Save()
        {
            return Context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    Context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        DbSet<TEntity> IRepository<TEntity>.GetDbSet()
        {
            throw new NotImplementedException();
        }
    }

    public static class UtilRepo
    {

        public static bool HasMember(this object objectToCheck, string memberName)
        {
            var type = objectToCheck.GetType();
            var a = type.GetMember(memberName);
            var Habilitado = false;
            if (a.Count() > 0)
            {
                Habilitado = true;
            }
            return Habilitado;
        }

        public static bool GetMember(Type TEntity, string memberName)
        {
            var a = TEntity.GetMember(memberName);
            var Habilitado = false;
            if (a.Count() > 0)
            {
                Habilitado = true;
            }
            return Habilitado;
        }
    }
}
