using Backend.Models;
using Backend.Services.Repository.ICRUD;

namespace Backend.Services.Repository.Interfaces
{
    public interface ICertificateRepository: IAdd<Certificate>, IGet<Certificate>, IGetAll<Certificate>, IUpdate<Certificate>, IMarkDeleted<Certificate>
    {
    }
}
