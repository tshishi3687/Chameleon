using Chameleon.Application.CompanySetting.DataAccess.Entities;
using Chameleon.Application.Securities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace Chameleon.Application.ApiInput;

[ApiController]
[Route("[controller]")]
public abstract class BaseController: Controller
{
    protected readonly Context Context;
    protected readonly Constantes Constantes;
    
    protected BaseController(IHttpContextAccessor cc, Context context)
    {
        Constantes = new Constantes(context);
        Context = context;
        var accessToken = cc.HttpContext?.GetTokenAsync("access_token").Result;
        if (accessToken != null)
        {
            Constantes.UseThisUserConnected(accessToken);
        }
    }

    protected Company Getcompany(Guid companyGuid)
    {
        var company = Context.Companies.First(c => c.Id.Equals(companyGuid));
        if (company == null) throw new Exception("Impossible to define the company!");
        return company;
    }

    protected void UserMatchCompany(Guid companyGuid)
    {
        if (!Getcompany(companyGuid).CompanyUser().Any(cu => cu.UserId.Equals(Constantes.Connected.Id)))
            throw new Exception("User does not interfere in this company");
    }
}