namespace Backend.Services.Repository.ICRUD
{
    public interface IGet<TEntity> where TEntity : class
    {
        IList<TEntity> Get(TEntity entity);
    }
}
