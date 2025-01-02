using Swashbuckle.AspNetCore.Swagger;
using System.Linq.Expressions;

public interface IRepository<T> where T : class

{



    Task<T> GetByIdAsync(Guid id);
    Task UpdateAsync(T entity);
    Task SaveAsync();


    

    Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null);
    //IQueryable<T> GetAllAsync(Expression<Func<T, bool>>? filter = null);
    Task<T> GetAsync(Expression<Func<T, bool>> filter = null, bool tracked = true);
    Task CreateAsync(T entiry);
    Task RemoveAsync(T entiry);
    Task SoftRemoveAsync(T entity);
    Task<List<T>> GetRemovedAsync(Expression<Func<T, bool>>? filter = null);
    
    
    Task<int> GetCountAsync(Expression<Func<T, bool>>? filter = null);
    Task<List<T>> GetPaginatedAsync(int? pageNumber = null, int? pageSize = null, Expression<Func<T, bool>>? filter = null);

}   