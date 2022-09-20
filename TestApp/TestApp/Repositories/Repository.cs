using Microsoft.EntityFrameworkCore;
using TestApp.Database;

namespace TestApp.Repositories;

public class Repository<TEntity> : IRepository<TEntity>
    where TEntity: class
{
    private readonly TestAppContext _context;
    private readonly DbSet<TEntity> _dbSet;

    public Repository(TestAppContext context)
    {
        _context = context;
        _dbSet = context.Set<TEntity>();
    }

    public IQueryable<TEntity> Query => _dbSet.AsQueryable();

    public async Task<TEntity> AddAsync(TEntity item)
    {
        var entityEntry = await _dbSet.AddAsync(item);
        return entityEntry.Entity;
    }

    public void Update(TEntity item)
    {
        _context.Update(item);
    }

    public void Delete(TEntity item)
    {
        _dbSet.Remove(item);
    }

    public async Task SaveChangesAsync() =>
        await _context.SaveChangesAsync();
}