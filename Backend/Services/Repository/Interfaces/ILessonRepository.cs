using Backend.Models;
using Backend.Services.Repository.ICRUD;

namespace Backend.Services.Repository.Interfaces
{
    public interface ILessonRepository: IAdd<Lesson>, IGet<Lesson>, IGetAll<Lesson>, IUpdate<Lesson>, IMarkDeleted<Lesson>, IAddRange<Lesson>
    {
    }
}
