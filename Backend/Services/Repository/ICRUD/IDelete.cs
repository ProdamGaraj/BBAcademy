namespace Backend.Services.Repository.ICRUD
{
    public interface IDelete<TEntity> where TEntity : class
    {
        void Delete(TEntity entity);
    }
}
