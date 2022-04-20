using System.Linq.Expressions;

namespace WebAppRepositoryWithUOW.Core.IRepository
{
    public interface IBaseRepository<T> where T : class
    {
        // get all objects
        Task<IEnumerable<T>> GetAllAsync();

        // get all objects with include
        Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] navigationProperties);

        // get all objects with specific condition
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> selector);

        // get all objects with specific condition with include
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> selector, params Expression<Func<T, object>>[] navigationProperties);

        //get object with specific condition
        Task<T> GetObjectAsync(Expression<Func<T, bool>> selector);

        //get object with specific condition with include
        Task<T> GetObjectAsync(Expression<Func<T, bool>> selector, params Expression<Func<T, object>>[] navigationProperties);

        //add new object
        Task CreateAsync(T entity);

        //update object records
        void Update(T entity);

        //Delete object
        void Delete(T entity);
    }
}
