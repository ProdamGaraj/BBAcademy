using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class RepositoryBase<T> : IRepository<T>
    where T : class
{
    private readonly BilimContext _context;

    public RepositoryBase(BilimContext context)
    {
        _context = context;
    }

    private DbSet<T> GetSet()
    {
        return _context.Set<T>();
    }

    public IQueryable<T> GetAll()
    {
        return GetSet();
    }

    public async Task Add(IEnumerable<T> entities)
    {
        var dbSet = GetSet();
        
        dbSet.AddRange(entities);

        await _context.SaveChangesAsync();
    }

    public void Add(T entity)
    {
        var dbSet = GetSet();
        
        dbSet.Add(entity);
    }

    public async Task Update(IEnumerable<T> entities)
    {
        var dbSet = GetSet();
        
        dbSet.UpdateRange(entities);

        await _context.SaveChangesAsync();
    }

    public async Task Update(T entity)
    {
        var dbSet = GetSet();
        
        dbSet.Update(entity);

        await _context.SaveChangesAsync();
    }

    public async Task Delete(IEnumerable<T> entities)
    {
        var dbSet = GetSet();
        
        dbSet.RemoveRange(entities);

        await _context.SaveChangesAsync();
    }

    public async Task Delete(T entity)
    {
        var dbSet = GetSet();
        
        dbSet.Remove(entity);

        await _context.SaveChangesAsync();
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}