using Backend.Models;
using Backend.Services.Repository.ICRUD;

namespace Backend.Services.Repository.Interfaces
{
    public interface ICertificateRepository: IAdd<Certificate>, IGet<Certificate>, IGetAll<Certificate>, IGetWithoutContext<Certificate>, IGetAllWithoutContext<Certificate>, IUpdate<Certificate>, IMarkDeleted<Certificate>
    {
    }
}
