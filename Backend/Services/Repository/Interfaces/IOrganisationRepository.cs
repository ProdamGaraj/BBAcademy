using Backend.Models;
using Backend.Services.Repository.ICRUD;

namespace Backend.Services.Repository.Interfaces
{
    public interface IOrganisationRepository: IAdd<Organisation>, IGet<Organisation>, IGetAll<Organisation>, IUpdate<Organisation>, IMarkDeleted<Organisation>
    {
    }
}
