using AutoMapper;
using BLL.ClickService;
using BLL.Models.Click;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using ClickPrepareDto = WebApi.Models.ClickPrepareDto;
using ClickPrepareResponseDto = WebApi.Models.ClickPrepareResponseDto;

namespace WebApi.Controllers;

[Controller]
[Route("[controller]/[action]")]
[ResponseCache(NoStore = true)]
public class ClickController : Controller
{
    private readonly IClickService _clickService;
    private readonly IMapper _mapper;

    public ClickController(IClickService clickService, IMapper mapper)
    {
        _clickService = clickService;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<ActionResult<ClickPrepareResponseDto>> Prepare([FromForm] ClickPrepareDto dto)
    {
        var bllModel = _mapper.Map<ClickPrepareBllModel>(dto);
        var response = await _clickService.ProcessPrepare(bllModel);

        var responseDto = _mapper.Map<ClickPrepareResponseDto>(response);

        return responseDto;
    }

    [HttpPost]
    public async Task<ActionResult<ClickCompleteResponseDto>> Complete([FromForm] ClickCompleteDto dto)
    {
        var bllModel = _mapper.Map<ClickCompleteBllModel>(dto);
        var response = await _clickService.ProcessComplete(bllModel);

        var responseDto = _mapper.Map<ClickCompleteResponseDto>(response);

        return responseDto;
    }
}