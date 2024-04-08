using Chameleon.Application.HumanSetting.Business.Dtos;
using Chameleon.Application.HumanSetting.Business.Services;
using Chameleon.Application.Securities;
using Microsoft.AspNetCore.Mvc;

namespace Chameleon.Application.ApiInput.AllowAll;

[ApiController]
[Route("[controller]")]
public class UserController(IHttpContextAccessor cc, IConstente iContent, Context context) : AbstractController(cc, iContent)
{
    [HttpPost("/login")]
    public IActionResult CreateLogin([FromBody] LoggerDto dto)
    {
        var result = new UserVueServiceBase(context).Login(dto);
        return new ObjectResult(result)
        {
            StatusCode = (int)result.StatusCode
        };
    }
}