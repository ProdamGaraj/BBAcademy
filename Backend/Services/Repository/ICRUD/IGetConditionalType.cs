using Backend.Models;

namespace Backend.Services.Repository.ICRUD
{
    public interface IGetConditionalType<TEntity> where TEntity : class
    {
        public Task<IList<Question>> GetConditionalType(Dictionary<QuestionType, int> keyValue);
    }
}
