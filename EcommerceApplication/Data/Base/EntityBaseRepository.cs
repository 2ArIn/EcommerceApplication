using EcommerceApplication.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EcommerceApplication.Data.Base
{
    public class EntityBaseRepository<T> : IEntityBaseRepository<T> where T : class, IEntityBase, new()
    {
        private readonly AppDbContext _context;
        public EntityBaseRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task Add(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var entity =await _context.Set<T>().FirstOrDefaultAsync(n => n.Id == id);
            _context.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _context.Set<T>().ToListAsync();
        }

        //public async Task<IEnumerable<T>> GetAll(params Expression<Func<T, object>>[] includes)
        //{
        //    IQueryable<T> query = _context.Set<T>();
        //    //foreach(var includeProperty in includeProperties)
        //    //{
        //    //    //query = query.Include(includeProperty);
        //    //    query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

        //    //}
        //    foreach (var include in includes)
        //    {
        //        query = query.Include(include);
        //    }
        //    return await query.ToListAsync();
        //}
        public async Task<IEnumerable<T>> GetAll(params string[] includeProperties)
        {
            IQueryable<T> query = _context.Set<T>();

            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            return await query.ToListAsync();
        }

        public async Task<T> GetById(int id)
        {
            return await _context.Set<T>().Where(a => a.Id == id).FirstOrDefaultAsync();
        }



        public async Task UpdateAsync(int id, T entity)
        {
            _context.Update(entity);
            _context.SaveChangesAsync();
        }
    }
}
