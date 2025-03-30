using System.Net;
using Chameleon.Application.HumanSetting.Business.Dtos;
using Chameleon.Application.HumanSetting.Business.Services;
using Microsoft.AspNetCore.Mvc;

namespace Chameleon.Application.ApiInput.AllowAll;

public class LoginController(IHttpContextAccessor cc, Context context, UserService userService): BaseController(cc, context)
    
{

    [HttpPost]
    public async Task<ActionResult<Data>> Login([FromBody] LoggerDto dto)
    {
        try
        {
            return Ok(userService.Login(dto, Constantes));
        }
        catch (Exception e)
        {
            await Task.Delay(5000);
            return StatusCode(HttpStatusCode.BadRequest.GetHashCode(), $"{e.Message}");
        }
    }
}