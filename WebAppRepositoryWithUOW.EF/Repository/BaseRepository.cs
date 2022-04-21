using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebAppRepositoryWithUOW.Core.IRepository;

namespace WebAppRepositoryWithUOW.EF.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly AppDbContext _context;

        public BaseRepository(AppDbContext context)
        {
            _context = context;
        }


        // get all objects
        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }


        // get all objects with include
        public IEnumerable<T> GetAll(params Expression<Func<T, object>>[] navigationProperties)
        {
            IQueryable<T> query = _context.Set<T>();
            foreach (var navigationProperty in navigationProperties)
            {
                query.Include(navigationProperty);
            }
            return query.ToList();
        }


        // get all objects with specific condition
        public IEnumerable<T> GetAll(Expression<Func<T, bool>> selector)
        {
            return _context.Set<T>().Where(selector).ToList();
        }


        // get all objects with specific condition with include
        public IEnumerable<T> GetAll(Expression<Func<T, bool>> selector, params Expression<Func<T, object>>[] navigationProperties)
        {
            IQueryable<T> query = _context.Set<T>().Where(selector);
            foreach (var navigationProperty in navigationProperties)
            {
                query.Include(navigationProperty);
            }
            return query.ToList();
        }


        //get object with specific condition
        public T Find(Expression<Func<T, bool>> selector)
        {
            return _context.Set<T>().SingleOrDefault(selector);
        }


        //get object with specific condition with include
        public T Find(Expression<Func<T, bool>> selector, params Expression<Func<T, object>>[] navigationProperties)
        {
            IQueryable<T> query = _context.Set<T>();
            foreach (var navigationProperty in navigationProperties)
            {
                query.Include(navigationProperty);
            }
            return query.SingleOrDefault(selector);
        }


        //add new object
        public void Create(T entity)
        {
            _context.Set<T>().Add(entity);
        }


        //update object records
        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }


        //Delete object
        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }
    }
}
