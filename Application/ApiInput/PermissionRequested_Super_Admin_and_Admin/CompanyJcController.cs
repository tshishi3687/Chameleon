using Chameleon.Application.CompanySetting.Business.Dtos;
using Chameleon.Application.CompanySetting.Business.Services;
using Chameleon.Application.Securities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Chameleon.Application.ApiInput.PermissionRequested_Super_Admin_and_Admin;

[ApiController]
[Route("[controller]")]
[Authorize(Roles = "SUPER_ADMIN, ADMIN")] 
public class CompanyJcController(IHttpContextAccessor cc, IConstente iContent, Context context) : AbstractController(cc, iContent)
{
    [HttpPut("/{companyGuid}")]
    public IActionResult AddUserInCompany(Guid companyGuid, [FromBody] AddCompanyUser dto)
    {
        var result = new CompanyServiceBase(context).AddUserInCompany(dto, companyGuid);
        return new ObjectResult(result)
        {
            StatusCode = (int)result.StatusCode
        };
    }
}