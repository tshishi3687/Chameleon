using System.Net;
using Chameleon.Application.CompanySetting.Business.Dtos;
using Chameleon.Application.CompanySetting.Business.Services;
using Chameleon.Application.HumanSetting;
using Chameleon.Application.Securities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Chameleon.Application.ApiInput.PermissionRequested_Super_Admin_and_Admin;

[ApiController]
[Route("companyApostle")]
public class CompanyApostleController(IHttpContextAccessor cc, IConstente iContent, Context context) : AbstractController(cc, iContent)
{
    private readonly IConstente _constente;
    
    [HttpPut("/{companyGuid}")]
    public IActionResult AddUserInCompany(Guid companyGuid, [FromBody] AddCompanyUser dto)
    {
        if (!CanDoAction(companyGuid).StatusCode.Equals(HttpStatusCode.Accepted))
        {
            return new ObjectResult(CanDoAction(companyGuid))
            {
                StatusCode = (int)CanDoAction(companyGuid).StatusCode
            };
        }

        return Ok(new CompanyService(context).AddUserInCompany(dto, companyGuid));
    }
    
    private HttpResponseMessage CanDoAction(Guid companyGuid)
    {
        var companyConcerned = context.Companies.FirstOrDefault(c => c.Id.Equals(companyGuid));
        if (companyConcerned == null)
        {
            return new HttpResponseMessage(HttpStatusCode.NotFound)
            {
                Content = new StringContent("Company not found")
            };
        }
        
        if (!_constente.Connected.UserRoles()
                .Any(ur => ur.Roles.Company.Id.Equals(companyGuid) &&
                           (ur.Roles.Name.Equals(EnumUsersRoles.SUPER_ADMIN) ||
                            ur.Roles.Name.Equals(EnumUsersRoles.ADMIN))))
        {
            return new HttpResponseMessage(HttpStatusCode.Unauthorized)
            {
                Content = new StringContent("You are not allowed to do this!")
            };
        }

        return new HttpResponseMessage(HttpStatusCode.Accepted);
    }
}