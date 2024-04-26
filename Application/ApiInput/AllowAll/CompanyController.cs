using System.Net;
using Chameleon.Application.CompanySetting.Business.Dtos;
using Chameleon.Application.CompanySetting.Business.Mappers;
using Chameleon.Application.CompanySetting.Business.Services;
using Microsoft.AspNetCore.Mvc;

namespace Chameleon.Application.ApiInput.AllowAll;


public class CompanyController(IHttpContextAccessor cc, Context context): BaseController(cc, context)
{
    private readonly CompanyEasyVueMapper _mapper = new();
    
    [HttpPost("/create")]
    public async Task<IActionResult> CreateCompany([FromBody] CreationCompanyAndUserDto dto)
    {
        try
        {
            return Ok(_mapper.ToDto(new CompanyService(Context).CreateCompanyAndUser(dto)));
        }
        catch (Exception e)
        {
            await Task.Delay(5000);
            return StatusCode(HttpStatusCode.BadRequest.GetHashCode(), $"Error {HttpStatusCode.BadRequest.GetHashCode()} {HttpStatusCode.BadRequest}: {e.Message}!");
        }
            
    }
}