using AutoMapper;
using BLL.Models.Click;
using WebApi.Models;

namespace WebApi.Mapping;

public class ClickMapping : Profile
{
    public ClickMapping()
    {
        CreateMap<ClickPrepareDto, ClickPrepareBllModel>();
        CreateMap<ClickPrepareBllResponseModel, ClickPrepareResponseDto>();

        CreateMap<ClickCompleteDto, ClickCompleteBllModel>();
    }
}