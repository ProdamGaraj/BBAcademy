using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Models;
using Backend.Models.Enum;

namespace Backend.Services.Repository.ICRUD
{
    public interface IGetConditionalType<TEntity> where TEntity : class
    {
        public Task<IList<Question>> GetConditionalType(Dictionary<QuestionType, int> keyValue);
    }
}
