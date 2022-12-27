using Backend.Models;
using Backend.Services.Repository.ICRUD;

namespace Backend.Services.Repository.Interfaces
{
    public interface ICourseRepository: IAdd<Course>, IGet<Course>, IGetAll<Course>, IUpdate<Course>,IMarkDeleted<Course>
    {
    }
}
