using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
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
        public async Task<IEnumerable<T>> Get(Expression<Func<T, bool>> filter = null,
                                  Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                  int pageNumber = 1,
                                  int pageSize = 10, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = dbSet;

            //Searching/Filtering
            if (filter != null)
                query = query.Where(filter);

            //Sorting
            if (orderBy != null)
                query = orderBy(query);

            //Paging
            query = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.ToListAsync();
        }

        public virtual async Task<T> Get(int id, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = dbSet;

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

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
