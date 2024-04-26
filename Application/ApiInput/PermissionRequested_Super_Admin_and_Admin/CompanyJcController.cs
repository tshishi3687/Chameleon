using System.Net;
using Chameleon.Application.CompanySetting.Business.Dtos;
using Chameleon.Application.CompanySetting.Business.Mappers;
using Chameleon.Application.CompanySetting.Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Chameleon.Application.ApiInput.PermissionRequested_Super_Admin_and_Admin;

[Authorize(Roles = "SUPER_ADMIN, ADMIN")]

public class CompanyJcController(IHttpContextAccessor cc, Context context) : BaseController(cc, context)
{
    
    [HttpPut("/addUser/{companyGuid:guid}")]
    public async Task<IActionResult> AddUserInCompany(Guid companyGuid, [FromBody] AddCompanyUser dto)
    {
        try
        {
            UserMatchCompany(companyGuid);
            return Ok(new CompanyEasyVueMapper().ToDto(
                new CompanyService(Context).AddUserInCompany(dto, Getcompany(companyGuid))));
        }
        catch (Exception e)
        {
            await Task.Delay(3000);
            return StatusCode(HttpStatusCode.NotAcceptable.GetHashCode(),
                $"Error {HttpStatusCode.NotAcceptable.GetHashCode()} {HttpStatusCode.NotAcceptable}: {e.Message}!");
        }
    }
    
    [HttpPost("/addCard/{companyGuid:guid}")]
    public IActionResult CreateEntity(Guid companyGuid, [FromBody] CardDto dto)
    {
        try
        {
            UserMatchCompany(companyGuid);
            return Ok(new CardMapper().ToDto(new CardService(Context).CreateEntity(Getcompany(companyGuid), dto)));
        }
        catch (Exception e)
        {
            return StatusCode(HttpStatusCode.NotAcceptable.GetHashCode(), $"Error {HttpStatusCode.NotAcceptable.GetHashCode()} {HttpStatusCode.NotAcceptable}: {e.Message}!");
        }
    }
}