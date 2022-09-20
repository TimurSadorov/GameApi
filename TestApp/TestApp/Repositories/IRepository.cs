namespace TestApp.Repositories;

public interface IRepository<TEntity>
    where TEntity : class
{
    IQueryable<TEntity> Query { get; }
    Task<TEntity> AddAsync(TEntity item);
    void Update(TEntity item);
    void Delete(TEntity item);
    public Task SaveChangesAsync();
}