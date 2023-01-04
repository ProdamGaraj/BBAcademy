using Backend.Models.Enum;

namespace Backend.Models.Interfaces
{
    public interface IBaseResponce<T>
    {
        StatusCode StatusCode { get; }
        T Data { get; }
    }
}
