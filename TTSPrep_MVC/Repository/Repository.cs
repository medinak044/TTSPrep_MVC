using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TTSPrep_MVC.Data;
using TTSPrep_MVC.Repository.IRepository;

namespace TTSPrep_MVC.Repository;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly AppDbContext _context;
    internal DbSet<T> _dbSet;

    public Repository(AppDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public virtual async Task<bool> AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
        return true;
    }

    public virtual async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate)
    {
        return await _dbSet.AnyAsync(predicate);
    }

    public virtual IEnumerable<T> GetSome(Expression<Func<T, bool>> predicate)
    {
        return _dbSet.Where(predicate);
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public virtual async Task<T> GetByIdAsync(string id)
    {
        return await _dbSet.FindAsync(id);
    }

    public virtual async Task<bool> RemoveAsync(T entity)
    {
        _dbSet.Remove(entity);
        return true;
    }

    public virtual async Task<bool> RemoveRangeAsync(IEnumerable<T> entity)
    {
        _dbSet.RemoveRange(entity);
        return true;
    }

    public virtual async Task<bool> UpdateAsync(T entity)
    {
        _context.Update(entity);
        return true; // Remember to call Save() after this
    }
}
