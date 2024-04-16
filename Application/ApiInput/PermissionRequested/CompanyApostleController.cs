using System.Net;
using Chameleon.Application.CompanySetting.Business.Dtos;
using Chameleon.Application.CompanySetting.Business.Services;
using Microsoft.AspNetCore.Mvc;

namespace Chameleon.Application.ApiInput.PermissionRequested;

public class CompanyApostleController(IHttpContextAccessor cc, Context context) : BaseController(cc, context)
{
    [HttpPut("/{companyGuid:guid}")]
    public IActionResult AddUserInCompany(Guid companyGuid, [FromBody] AddCompanyUser dto)
    {
        try
        {
            UserMatchCompany(companyGuid);
            return Ok(new CompanyService(Context).AddUserInCompany(dto, Getcompany(companyGuid)));
        }
        catch (Exception)
        {
            return StatusCode(HttpStatusCode.NotAcceptable.GetHashCode(), $"Error {HttpStatusCode.NotAcceptable.GetHashCode()} {HttpStatusCode.NotAcceptable}: User has not been added to the company!");
        }
    }
}