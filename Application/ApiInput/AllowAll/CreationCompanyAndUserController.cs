using System.Net;
using Chameleon.Application.CompanySetting.Business.Dtos;
using Chameleon.Application.CompanySetting.Business.Mappers;
using Chameleon.Application.CompanySetting.Business.Services;
using Chameleon.Application.HumanSetting.Business.Dtos;
using Chameleon.Application.HumanSetting.Business.Services;
using Microsoft.AspNetCore.Mvc;

namespace Chameleon.Application.ApiInput.AllowAll;


public class CreationCompanyAndUserController(IHttpContextAccessor cc, Context context): BaseController(cc, context)
{
    private readonly CompanyEasyVueMapper _mapper = new();
    
    [HttpPost()]
    public async Task<ActionResult<Data>> CreationCompanyAndUser([FromBody] CreationCompanyAndUserDto dto)
    {
        try
        {
            return Ok(new UserService(Context).CreateCompanyAndUser(dto, Constantes));
        }
        catch (Exception e)
        {
            await Task.Delay(5000);
            return StatusCode((int)HttpStatusCode.BadRequest, e.Message);
        }
            
    }
}