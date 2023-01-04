using Backend.Models;
using Backend.Services.Repository.ICRUD;

namespace Backend.Services.Repository.Interfaces
{
    public interface ICourseRepository: IAdd<Course>, IGet<Course>, IGetAll<Course>, IGetWithoutContext<Course>, IGetAllWithoutContext<Course>, IUpdate<Course>,IMarkDeleted<Course>
    {
    }
}
