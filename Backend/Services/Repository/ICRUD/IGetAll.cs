namespace Backend.Services.Repository.ICRUD
{
    public interface IGetAll<TEntity> where TEntity : class
    {
        IList<TEntity> GetAll();
    }
}
