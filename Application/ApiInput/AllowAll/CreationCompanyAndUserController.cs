using System.Net;
using Chameleon.Application.Common.Business.Dtos;
using Chameleon.Application.CompanySetting.Business.Dtos;
using Chameleon.Application.HumanSetting.Business.Dtos;
using Chameleon.Application.HumanSetting.Business.Services;
using Microsoft.AspNetCore.Mvc;

namespace Chameleon.Application.ApiInput.AllowAll;


public class CreationCompanyAndUserController(IHttpContextAccessor cc, Context context, UserService userService): BaseController(cc, context)
{
    [HttpPost()]
    public async Task<ActionResult<Passport>> CreationCompanyAndUser([FromBody] CreationCompanyAndUserDto dto)
    {
        try
        {
            return Ok(await userService.CreateCompanyAndUser(dto));
        }
        catch (Exception e)
        {
            await Task.Delay(5000);
            return StatusCode((int)HttpStatusCode.BadRequest, e.Message);
        }
            
    }
    
}