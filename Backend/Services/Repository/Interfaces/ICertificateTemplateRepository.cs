using Backend.Models;
using Backend.Services.Repository.ICRUD;

namespace Backend.Services.Repository.Interfaces
{
    public interface ICertificateTemplateRepository:IAdd<CertificateTemplate>,
                                                    IMarkDeleted<CertificateTemplate>,
                                                    IGet<CertificateTemplate>,
                                                    IUpdate<CertificateTemplate>,
                                                    IGetAll<CertificateTemplate>    
    {
    }
}