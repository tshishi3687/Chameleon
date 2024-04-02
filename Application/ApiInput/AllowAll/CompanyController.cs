using Chameleon.Application.CompanySetting.Business.Dtos;
using Chameleon.Application.CompanySetting.Business.Services;
using Chameleon.Application.Securities;
using Microsoft.AspNetCore.Mvc;

namespace Chameleon.Application.ApiInput.AllowAll;

[ApiController]
[Route("[controller]")]
public class CompanyController(IHttpContextAccessor cc, IConstente iContent, Context context)
    : AbstractController(cc, iContent)
{
    [HttpPost]
    public IActionResult CreateCompanyAndUser([FromBody] CreationCompanyAndUserDto dto)
    {
        return Ok(new CompanyService(context).CreateEntity(dto));
    }
}