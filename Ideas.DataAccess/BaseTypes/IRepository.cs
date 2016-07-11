using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ideas.DataAccess.UtilityTypes;
using System.Linq.Expressions;

namespace Ideas.DataAccess.BaseTypes
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity GetById(int id);

        /// <summary>
        /// Returns paged result based on query, sorting and additional navigation properties to be included.
        /// </summary>
        /// <param name="query"></param>
        /// <param name="orderBy"></param>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        PagedResult<TEntity> GetByQuery(Expression<Func<TEntity, bool>> query = null, 
                Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "");

        TEntity GetFirst(Expression<Func<TEntity, bool>> predicate);

        void Insert(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        void DeleteByID(int id);
    }
}
