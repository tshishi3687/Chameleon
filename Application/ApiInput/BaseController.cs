using Chameleon.Application.CompanySetting.DataAccess.Entities;
using Chameleon.Application.Securities;
using Microsoft.AspNetCore.Mvc;

namespace Chameleon.Application.ApiInput;

[ApiController]

[Route("[controller]")]
public abstract class BaseController(IHttpContextAccessor cc, Context context): Controller
{
    protected readonly Context Context = context;
    protected readonly Constantes Constente = new(context);
    protected readonly IHttpContextAccessor HttpContextAccessor = cc;
    
    protected Company Getcompany(Guid companyGuid)
    {
        var company = Context.Companies.First(c => c.Id.Equals(companyGuid));
        if (company == null) throw new Exception("Impossible to define the company!");
        return company;
    }

    protected void UserMatchCompany(Guid companyGuid)
    {
        if (!Getcompany(companyGuid).CompanyUser().Any(cu => cu.UserId.Equals(Constente.Connected.Id)))
            throw new Exception("User does not interfere in this company");
    }
}