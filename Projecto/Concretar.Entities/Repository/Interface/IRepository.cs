using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Concretar.Entities.Repository.Interface
{
    public interface IRepository<TEntity> : IDisposable where TEntity : class, IEntity
    {
        DbSet<TEntity> GetDbSet();
        /// <summary>
        /// Gets all objects from database
        /// </summary>
        IQueryable<TEntity> All();


        /// <summary>
        /// Gets all objects from database including...
        /// </summary>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        IQueryable<TEntity> AllIncluding(params Expression<Func<TEntity, object>>[] includeProperties);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        IQueryable<TEntity> FilterIncluding(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties);

        /// <summary>
        /// Gets objects from database by filter.
        /// </summary>
        /// <param name="predicate">Specified a filter</param>
        IQueryable<TEntity> Filter(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Gets objects from database with filting and paging.
        /// </summary>
        /// <typeparam name="Key"></typeparam>
        /// <param name="filter">Specified a filter</param>
        /// <param name="total">Returns the total records count of the filter.</param>
        /// <param name="index">Specified the page index.</param>
        /// <param name="size">Specified the page size</param>
        IQueryable<TEntity> Filter<Key>(Expression<Func<TEntity, bool>> filter,
            out int total, int index = 0, int size = 50);

        /// <summary>
        /// Gets the object(s) is exists in database by specified filter.
        /// </summary>
        /// <param name="predicate">Specified the filter expression</param>
        bool Contains(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Find object by keys.
        /// </summary>
        /// <param name="keys">Specified the search keys.</param>
        //TEntity Find(params object[] keys);

        /// <summary>
        /// Find object by specified expression.
        /// </summary>
        /// <param name="predicate"></param>
        TEntity Find(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Create a new object to database.
        /// </summary>
        /// <param name="t">Specified a new object to create.</param>
        TEntity Create(TEntity t);

        /// <summary>
        /// Delete the object from database.
        /// </summary>
        /// <param name="t">Specified a existing object to delete.</param>        
        void Delete(TEntity t, bool? forzarDelete = null);


        /// <summary>
        /// Delete objects from database by specified filter expression.
        /// </summary>
        /// <param name="predicate"></param>
        void Delete(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Update object changes and save to database.
        /// </summary>
        /// <param name="t">Specified the object to save.</param>
        void Update(TEntity t);

        /// <summary>
        /// Get the total objects count.
        /// </summary>
        int Count { get; }

        /// <summary>
        /// Count objects from database by specified filter expression.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        int CountWhere(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Count objects from database by specified filter expression.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        TEntity Last();

        /// <summary>
        /// Save the changes
        /// </summary>
        /// <returns>Return result of save</returns>
        int Save();

        /// <summary>
        /// Delete and create new list of entities
        /// </summary>
        /// <param name="predicateToDelete"></param>
        /// <param name="itemListToCreate"></param>
        void DeleteAndCreate(Expression<Func<TEntity, bool>> predicateToDelete, List<TEntity> itemListToCreate);
    }
}
