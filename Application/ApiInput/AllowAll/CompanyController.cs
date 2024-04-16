using System.Net;
using Chameleon.Application.CompanySetting.Business.Dtos;
using Chameleon.Application.CompanySetting.Business.Mappers;
using Chameleon.Application.CompanySetting.Business.Services;
using Microsoft.AspNetCore.Mvc;

namespace Chameleon.Application.ApiInput.AllowAll;

[ApiController]
[Route("[controller]")]
public class CompanyController(IHttpContextAccessor cc, Context context): BaseController(cc, context)
{
    private readonly CompanyEasyVueMapper _mapper = new();
    
    [HttpPost]
    public IActionResult CreateCompany([FromBody] CreationCompanyAndUserDto dto)
    {
        try
        {
            return Ok(_mapper.ToDto(new CompanyService(Context).CreateCompanyAndUser(dto)));
        }
        catch (Exception)
        {
            return StatusCode(HttpStatusCode.BadRequest.GetHashCode(), $"Error {HttpStatusCode.BadRequest.GetHashCode()} {HttpStatusCode.BadRequest}: Your account and your business have not been created!");
        }
            
    }
}