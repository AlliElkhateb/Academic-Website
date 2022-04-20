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
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }


        // get all objects with include
        public async Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] navigationProperties)
        {
            IQueryable<T> query = _context.Set<T>();
            foreach (var navigationProperty in navigationProperties)
            {
                query.Include(navigationProperty);
            }
            return await query.ToListAsync();
        }


        // get all objects with specific condition
        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> selector)
        {
            return await _context.Set<T>().Where(selector).ToListAsync();
        }


        // get all objects with specific condition with include
        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> selector, params Expression<Func<T, object>>[] navigationProperties)
        {
            IQueryable<T> query = _context.Set<T>().Where(selector);
            foreach (var navigationProperty in navigationProperties)
            {
                query.Include(navigationProperty);
            }
            return await query.ToListAsync();
        }


        //get object with specific condition
        public async Task<T> GetObjectAsync(Expression<Func<T, bool>> selector)
        {
            return await _context.Set<T>().SingleOrDefaultAsync(selector);
        }


        //get object with specific condition with include
        public async Task<T> GetObjectAsync(Expression<Func<T, bool>> selector, params Expression<Func<T, object>>[] navigationProperties)
        {
            IQueryable<T> query = _context.Set<T>();
            foreach (var navigationProperty in navigationProperties)
            {
                query.Include(navigationProperty);
            }
            return await query.SingleOrDefaultAsync(selector);
        }


        //add new object
        public async Task CreateAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
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
