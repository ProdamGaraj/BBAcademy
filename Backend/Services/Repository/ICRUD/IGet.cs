namespace Backend.Services.Repository.ICRUD
{
    public interface IGet<TEntity> where TEntity : class
    {
        TEntity Get(int id);
    }
}
