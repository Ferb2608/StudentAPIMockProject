using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace RepositoryLayer
{
    public class BaseRepository<T> where T : class
    {
        internal SchoolContext dbContext;
        internal DbSet<T> dbSet;

        public BaseRepository(SchoolContext dbContext)
        {
            this.dbContext = dbContext;
            dbSet = this.dbContext.Set<T>();
        }
        //public async Task<IEnumerable<T>> Get(Expression<Func<T, bool>> filter = null,
        //                          Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        //                          int pageNumber = 1,
        //                          int pageSize = 10, params Expression<Func<T, object>>[] includes)
        //{
        //    IQueryable<T> query = dbSet;

        //    //Searching/Filtering
        //    if (filter != null)
        //        query = query.Where(filter);

        //    //Sorting
        //    if (orderBy != null)
        //        query = orderBy(query);

        //    //Paging
        //    query = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);

        //    foreach (var include in includes)
        //    {
        //        query = query.Include(include);
        //    }

        //    return await query.ToListAsync();
        //}
        public async Task<IEnumerable<T>> Get(
                                              Expression<Func<T, bool>> filter = null,
                                              Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                              int pageNumber = 1,
                                              int pageSize = 10,
                                              Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
                                              bool disableTracking = true)
        {
            IQueryable<T> query = dbSet;

            query = disableTracking ? query.AsNoTracking() : query.AsTracking();

            if (include != null) query = include(query);

            if (filter != null) query = query.Where(filter);

            if (orderBy != null) _ = orderBy(query);

            query = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);

            return await query.ToListAsync();
        }
        public virtual async Task<T> Get(int id, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            IQueryable<T> query = dbSet;

            if (include != null) query = include(query);

            var entity = await query.FirstOrDefaultAsync(e => EF.Property<int>(e, "Id") == id);
            
            return entity;
        }
        public virtual async Task<T> Post(T entity)
        {
            await dbSet.AddAsync(entity);
            await dbContext.SaveChangesAsync();
            return entity;
        }

        public virtual void Put(T entity)
        {
            dbSet.Update(entity);
            dbContext.SaveChanges();
        }

        public virtual async Task Delete(int id)
        {
            dbSet.Remove(await Get(id));
            dbContext.SaveChanges();
        }
        public async Task<IEnumerable<T>> GetByProperty(Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> query = dbSet;
            query = query.Where(predicate);
            return await query.ToListAsync();
        }
    }
}
