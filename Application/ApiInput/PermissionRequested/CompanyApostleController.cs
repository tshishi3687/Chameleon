using System.Net;
using Chameleon.Application.CompanySetting.Business.Mappers;
using Chameleon.Application.CompanySetting.Business.Services;
using Chameleon.Application.HumanSetting.Business.Services;
using Microsoft.AspNetCore.Mvc;

namespace Chameleon.Application.ApiInput.PermissionRequested;

public class CompanyApostleController(IHttpContextAccessor cc, Context context) : BaseController(cc, context)
{

    [HttpGet("/getMyCompanies")]
    public IActionResult GetMyCompanies()
    {
        try
        {
            var user = Context.User.First(u => u.Id.Equals(Constantes.Connected.Id));
            if (user == null) throw new Exception("User not found");
            return Ok(new CompanyEasyVueMapper().ToDtos(new CompanyService(Context).GetMyCompanies(user)));
        }
        catch (Exception)
        {
            return StatusCode(HttpStatusCode.NotAcceptable.GetHashCode(),
                $"Error {HttpStatusCode.NotAcceptable.GetHashCode()} {HttpStatusCode.NotAcceptable}: There was an authentication problem!");
        }
    }

    [HttpGet("/connectMeWithThisCompany/{companyGuid:guid}")]
    public IActionResult ConnectMeWithThisCompany(Guid companyGuid)
    {
        try
        {
            var company = Context.Companies.First(c => c.Id.Equals(companyGuid));
            if (company == null)
                return StatusCode(HttpStatusCode.BadRequest.GetHashCode(),
                    $"Error {HttpStatusCode.BadRequest.GetHashCode()} {HttpStatusCode.BadRequest}: Company not found!");
            if (!company.CompanyUser().Any(cu => cu.UserId.Equals(Constantes.Connected.Id)))
                return StatusCode(HttpStatusCode.NotAcceptable.GetHashCode(),
                    $"Error {HttpStatusCode.NotAcceptable.GetHashCode()} {HttpStatusCode.NotAcceptable}: You cannot interact with this company!");
            return Ok(new UserService(Context).CreateJwtWithRoles(Constantes, companyGuid));
        }
        catch (Exception e)
        {
            return StatusCode(HttpStatusCode.NotAcceptable.GetHashCode(),
                $"Error {HttpStatusCode.NotAcceptable.GetHashCode()} {HttpStatusCode.NotAcceptable}: {e.Message}");
        }
    }
}