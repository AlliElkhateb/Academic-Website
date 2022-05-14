using System.Linq.Expressions;

namespace WebAppRepositoryWithUOW.EF.Repository
{
    public interface IRepository<T> where T : class
    {
        // get all objects
        IEnumerable<T> GetAll();

        // get all objects with include
        IEnumerable<T> GetAll(params Expression<Func<T, object>>[] navigationProperties);

        // get all objects with specific condition
        IEnumerable<T> GetAll(Expression<Func<T, bool>> selector);

        // get all objects with specific condition with include
        IEnumerable<T> GetAll(Expression<Func<T, bool>> selector, params Expression<Func<T, object>>[] navigationProperties);

        //get object with specific condition
        T GetObj(Expression<Func<T, bool>> selector);

        //get object with specific condition with include
        T GetObj(Expression<Func<T, bool>> selector, params Expression<Func<T, object>>[] navigationProperties);

        //add new object
        void Create(T entity);

        //update object records
        void Update(T entity);

        //Delete object
        void Delete(T entity);
    }
}
