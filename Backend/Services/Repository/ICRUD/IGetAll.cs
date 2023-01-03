namespace Backend.Services.Repository.ICRUD
{
    public interface IGetAll<TEntity> where TEntity : class
    {
        Task<IList<TEntity>> GetAll();
    }
}
