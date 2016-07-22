using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Ideas.DataAccess.UtilityTypes;
using Ideas.DataAccess.BaseTypes;

namespace Ideas.DataAccess.Model
{
    class Repository<TEntity> : IRepository<TEntity> where TEntity: class
    {
        protected DbSet<TEntity> dbSet;
        protected DbContext dbContext;

        public Repository(DbContext context)
        {
            this.dbContext = context;
            this.dbSet = this.dbContext.Set<TEntity>();
        }

        public TEntity GetById(int id)
        {
            return dbSet.Find(id);
        }

        public IEnumerable<TEntity> GetByQuery(Expression<Func<TEntity, bool>> query = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
        {
            IQueryable<TEntity> queryResult = dbSet;

            //If there is a query, execute it against the dbset
            if (query != null)
            {
                queryResult = queryResult.Where(query);
            }

            //get the include requests for the navigation properties and add them to the query result
            foreach (var property in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                queryResult = queryResult.Include(property);
            }
            
            if (orderBy != null)
            {
                return orderBy(queryResult);
            }

            return queryResult;
        }

        public TEntity GetFirst(Expression<Func<TEntity, bool>> predicate)
        {
            return dbSet.First(predicate);
        }

        public void Insert(TEntity entity)
        {
            dbSet.Add(entity);
        }

        public void Update(TEntity entity)
        {
            dbSet.Attach(entity);
            dbContext.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(TEntity entity)
        {
            if (dbContext.Entry(entity).State == EntityState.Detached)
                dbSet.Attach(entity);

            dbSet.Remove(entity);
        }

        public void DeleteByID(int id)
        {
            TEntity entity = dbSet.Find(id);
            dbSet.Remove(entity);
        }
    }
}
