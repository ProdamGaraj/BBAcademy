using Backend.Models;
using Backend.Services.Repository.ICRUD;

namespace Backend.Services.Repository.Interfaces
{
    public interface IProfessionRepository:IAdd<Profession>, IGet<Profession>, IGetAll<Profession>, IUpdate<Profession>, IMarkDeleted<Profession>
    {
    }
}
