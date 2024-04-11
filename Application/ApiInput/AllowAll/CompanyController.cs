using System.Net;
using Chameleon.Application.CompanySetting.Business.Dtos;
using Chameleon.Application.CompanySetting.Business.Mappers;
using Chameleon.Application.CompanySetting.Business.Services;
using Chameleon.Application.CompanySetting.DataAccess.Entities;
using Chameleon.Application.HumanSetting.Business.Mappers;
using Chameleon.Application.Securities;
using Microsoft.AspNetCore.Mvc;

namespace Chameleon.Application.ApiInput.AllowAll;

[ApiController]
[Route("[controller]")]
public class CompanyController(IHttpContextAccessor cc, IConstente iContent, Context context) : AbstractController(cc, iContent)
{
    private readonly CompanyEasyVueMapper _mapper = new();
    
    [HttpPost]
    public OkObjectResult CreateCompany([FromBody] CreationCompanyAndUserDto dto)
    {
            return Ok(_mapper.ToDto(new CompanyService(context).CreateCompanyAndUser(dto)));
    }
}