namespace Backend.Services.Repository.ICRUD
{
    public interface IUpdate<TEntity> where TEntity : class
    {
        void Update(TEntity entity);
    }
}
