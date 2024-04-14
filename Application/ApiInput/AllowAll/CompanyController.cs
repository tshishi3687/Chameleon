using System.Net;
using Chameleon.Application.CompanySetting.Business.Dtos;
using Chameleon.Application.CompanySetting.Business.Mappers;
using Chameleon.Application.CompanySetting.Business.Services;
using Chameleon.Application.Securities;
using Microsoft.AspNetCore.Mvc;

namespace Chameleon.Application.ApiInput.AllowAll;

[ApiController]
[Route("[controller]")]
public class CompanyController(IHttpContextAccessor cc, IConstente iContent, Context context) : Controller
{
    private readonly CompanyEasyVueMapper _mapper = new();
    
    [HttpPost]
    public OkObjectResult CreateCompany([FromBody] CreationCompanyAndUserDto dto)
    {
            return Ok(_mapper.ToDto(new CompanyService(context).CreateCompanyAndUser(dto)));
    }
}