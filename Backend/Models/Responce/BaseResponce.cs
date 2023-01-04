using Backend.Models.Enum;
using Backend.Models.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Backend.Models.Responce
{
    public class BaseResponse<T> : IBaseResponce<T>
    {
        public string Description { get; set; }

        public StatusCode StatusCode { get; set; }

        public T Data { get; set; }
    }
}
