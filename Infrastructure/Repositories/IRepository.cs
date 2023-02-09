namespace Infrastructure.Repositories;

public interface IRepository<T>
    where T : class
{
    IQueryable<T> GetAll();

    Task Add(IEnumerable<T> entities);

    void Add(T entity);

    Task Update(IEnumerable<T> entities);

    Task Update(T entity);

    Task Delete(IEnumerable<T> entities);

    Task Delete(T entity);
    Task SaveChangesAsync();
}