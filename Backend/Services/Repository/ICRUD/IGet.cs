namespace Backend.Services.Repository.ICRUD
{
    public interface IGet<TEntity> where TEntity : class
    {
        Task<TEntity> Get(long id);
    }
}