using System.Threading.Tasks;
using Backend.Models.Interfaces;
using Backend.ViewModels;

namespace Backend.Services.Interfaces
{
    public interface ICreationService
    {
       public Task<IBaseResponce<DataViewModel>> CreateFullCourse(DataViewModel dataViewModel);
    }
}
