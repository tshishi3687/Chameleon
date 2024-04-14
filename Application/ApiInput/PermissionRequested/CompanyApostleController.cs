using System.Net;
using Chameleon.Application.CompanySetting.Business.Dtos;
using Chameleon.Application.CompanySetting.Business.Services;
using Chameleon.Application.HumanSetting;
using Chameleon.Application.Securities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace Chameleon.Application.ApiInput.PermissionRequested;

[ApiController]

[Route("companyApostle")]
public class CompanyApostleController(IHttpContextAccessor cc, Context context): Controller
{
    private readonly Constantes _constente = new(context);
    
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

        var jwt = cc.HttpContext.GetTokenAsync("access_token").Result;
        _constente.UseThisUserConnected(jwt);
        var user = _constente.Connected;
        if (user == null)
        {
            return new HttpResponseMessage(HttpStatusCode.NotFound)
            {
                Content = new StringContent("user not found")
            };
        }

        return new HttpResponseMessage(HttpStatusCode.Accepted);
    }
}