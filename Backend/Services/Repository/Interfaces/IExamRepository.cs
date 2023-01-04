using Backend.Models;
using Backend.Services.Repository.ICRUD;

namespace Backend.Services.Repository.Interfaces
{
    public interface IExamRepository:IAdd<Exam>, IGet<Exam>, IGetAll<Exam>, IGetWithoutContext<Exam>, IGetAllWithoutContext<Exam>, IUpdate<Exam>, IMarkDeleted<Exam>
    {
    }
}
