using System.Net;
using Chameleon.Application.Common.Business.Dtos;
using Chameleon.Application.HumanSetting.Business.Dtos;
using Chameleon.Application.HumanSetting.Business.Mappers;
using Chameleon.Application.HumanSetting.Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Chameleon.Application.ApiInput.PermissionRequested_Super_Admin_and_Admin;

[Authorize(Roles = "SUPER_ADMIN, ADMIN")]
public class UserJcController(IHttpContextAccessor cc, Context context) : BaseController(cc, context)
{
    [HttpGet(("/companyUsers/{companyGuid:guid}"))]
    public IActionResult GetCompanyUser(Guid companyGuid)
    {
        try
        {
            var company = context.Companies.FirstOrDefault(c => c.Id.Equals(companyGuid));
            if (company == null) throw new Exception("Company not found!");
            return Ok(new UserVueMapper().ToDtos(new UserService(context).GetCompanyUser(company)));
        }
        catch (Exception e)
        {
            return StatusCode(HttpStatusCode.Unauthorized.GetHashCode(),
                $"Error {HttpStatusCode.Unauthorized.GetHashCode()} {HttpStatusCode.Unauthorized}: {e.Message}!");
        }
    }
    
    [HttpGet(("/isKnown/{companyGuid:guid}/{isKnown}"))]
    public IActionResult IsKnown(Guid companyGuid, string isKnown)
    {
        try
        {
            var company = context.Companies.FirstOrDefault(c => c.Id.Equals(companyGuid));
            if (company == null) throw new Exception("Company not found!");
            return Ok(new UserService(context).isKnown(company, isKnown));
        }
        catch (Exception e)
        {
            return StatusCode(HttpStatusCode.NotFound.GetHashCode(),
                $"Error {HttpStatusCode.NotFound.GetHashCode()} {HttpStatusCode.NotFound}: {e.Message}!");
        }
    }

    [HttpGet("/dto")]
    public IActionResult GetDto([FromBody] AllDto dto)
    {
        return Ok(new AllDto());
    }

}