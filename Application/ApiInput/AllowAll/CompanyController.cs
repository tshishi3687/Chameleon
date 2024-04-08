using Chameleon.Application.CompanySetting.Business.Dtos;
using Chameleon.Application.CompanySetting.Business.Services;
using Chameleon.Application.Securities;
using Microsoft.AspNetCore.Mvc;

namespace Chameleon.Application.ApiInput.AllowAll;

[ApiController]
[Route("[controller]")]
public class CompanyController(IHttpContextAccessor cc, IConstente iContent, Context context) : AbstractController(cc, iContent)
{
    [HttpPost]
    public IActionResult CreateCompany([FromBody] CreationCompanyAndUserDto dto)
    {
        var result = new CompanyServiceBase(context).CreateEntity(dto);
        return new ObjectResult(result)
        {
            StatusCode = (int)result.StatusCode
        };
    }
}