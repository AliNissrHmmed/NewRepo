using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Linq;
using Microsoft.AspNetCore.Http.HttpResults;


namespace ERP
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        internal DbSet<T> dbSet;

        public Repository(ApplicationDbContext db)
        {
            _db = db;
            dbSet = _db.Set<T>();
        }

        public async Task CreateAsync(T entity)
        {
            await dbSet.AddAsync(entity);
            await SaveAsync();
        }




        public async Task<T?> GetByIdAsync(Guid id)
        {
           return  await dbSet.FirstOrDefaultAsync(e => EF.Property<Guid>(e, "Id") == id);


        }


        public async Task<T?> GetAsync(Expression<Func<T, bool>>? filter = null, bool tracked = true)
        {
            IQueryable<T> query = dbSet;

            if (!tracked)
            {
                query = query.AsNoTracking();
            }

            query = query.Where(e => EF.Property<DateTime?>(e, "DeletedAt") == null);

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.FirstOrDefaultAsync();
        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null)
        {
            IQueryable<T> query = dbSet;
            query = query.Where(e => EF.Property<DateTime?>(e, "DeletedAt") == null);

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.ToListAsync();
        }

        public async Task RemoveAsync(T entity)
        {
            dbSet.Remove(entity);
            await SaveAsync();
        }

        public async Task SoftRemoveAsync(T entity)
        {
            var property = typeof(T).GetProperty("DeletedAt");
            if (property != null && property.PropertyType == typeof(DateTime?))
            {
                property.SetValue(entity, DateTime.UtcNow); // Set DeletedAt to current time (UTC)
                dbSet.Update(entity);
                await SaveAsync();
            }
            else
            {
                throw new InvalidOperationException("This entity does not have a DeletedAt property.");
            }
        }

        public async Task<List<T>> GetRemovedAsync(Expression<Func<T, bool>>? filter = null)
        {
            IQueryable<T> query = dbSet;

            query = query.Where(e => EF.Property<DateTime?>(e, "DeletedAt") != null);

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.ToListAsync();
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            dbSet.Update(entity);
            await SaveAsync();
        }

        public async Task<List<T>> GetPaginatedAsync(int? pageNumber = null, int? pageSize = null, Expression<Func<T, bool>>? filter = null)
        {
            int currentPage = pageNumber ?? 1;
            int currentPageSize = pageSize ?? 25;


            IQueryable<T> query = dbSet;
            query = query.Where(e => EF.Property<DateTime?>(e, "DeletedAt") == null);


            if (filter != null)
            {
                query = query.Where(filter);
            }

            query = query.Skip((currentPage - 1) * currentPageSize)
                         .Take(currentPageSize);

            return await query.ToListAsync();
        }

        public async Task<int> GetCountAsync(Expression<Func<T, bool>>? filter = null)
        {
            IQueryable<T> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.CountAsync();
        }

        public string GenerateCode()
        {
            return Guid.NewGuid().ToString();
        }


    }

}
