using HeyDEAN_API.Data;
using HeyDEAN_API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HeyDEAN_API.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly AppDbContext _context;
        protected readonly DbSet<T> _set;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
            _set = _context.Set<T>();
        }

        public async Task<T> AddAsync(T entity)
        {
            var e = (await _set.AddAsync(entity)).Entity;
            await _context.SaveChangesAsync();
            return e;
        }

        public async Task DeleteAsync(T entity)
        {
            _set.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _set.ToListAsync();
        }

        public async Task<T?> GetAsync(int id)
        {
            return await _set.FindAsync(id);
        }

        public async Task UpdateAsync(T entity)
        {
            _set.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}