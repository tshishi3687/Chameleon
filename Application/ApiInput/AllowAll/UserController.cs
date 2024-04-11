using Chameleon.Application.HumanSetting.Business.Dtos;
using Chameleon.Application.HumanSetting.Business.Mappers;
using Chameleon.Application.HumanSetting.Business.Services;
using Chameleon.Application.Securities;
using Microsoft.AspNetCore.Mvc;

namespace Chameleon.Application.ApiInput.AllowAll;

[ApiController]
[Route("[controller]")]
public class UserController(IHttpContextAccessor cc, IConstente iContent, Context context) : AbstractController(cc, iContent)
{
    // private readonly UserVueMapper _mapper = new();
    // [HttpPost("/login")]
    // public IActionResult CreateLogin([FromBody] LoggerDto dto)
    // {
    //     return Ok(_mapper.ToDto(new UserService(context).(dto)));
    //
    // }
}