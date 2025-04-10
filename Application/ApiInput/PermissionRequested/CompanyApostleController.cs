using System.Net;
using Chameleon.Application.Common.Business.Dtos;
using Chameleon.Application.CompanySetting.Business.Dtos;
using Chameleon.Application.CompanySetting.Business.Services;
using Chameleon.Application.HumanSetting.Business.Dtos;
using Chameleon.Application.HumanSetting.Business.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Chameleon.Application.ApiInput.PermissionRequested;

public class CompanyApostleController(IHttpContextAccessor cc, Context context, CompanyService service, UserService userService) : BaseController(cc, context)
{

    [HttpGet("/getMyCompanies")]
    public async Task<ActionResult<ICollection<CompanyEasyVueDto>>> GetMyCompanies()
    {
        try
        {
            var user = await GetUser();
            if (user == null) throw new Exception("User not found");
            return Ok(await service.GetMyCompanies(user));
        }
        catch (Exception e)
        {
            await Task.Delay(5000);
            return StatusCode(HttpStatusCode.BadRequest.GetHashCode(), $"{e.Message}");
        }
    }

    [HttpGet("/connectMeWithThisCompany/{companyGuid:guid}")]
    public async Task<ActionResult<Passport>>ConnectMeWithThisCompany(Guid companyGuid)
    {
        try
        {
            var user = await GetUser();
            if (user == null) throw new Exception("User not found");
            var company = await context.Companies.FirstOrDefaultAsync(c => c.Id.Equals(companyGuid));
            if (company == null) throw new Exception("Company not found");
            
            var companyUser = await company.CompanyUser();
            if (!companyUser.Any(cu => cu.UserId.Equals(user.Id)))
                throw new Exception(
                    $"Error {HttpStatusCode.NotAcceptable.GetHashCode()} {HttpStatusCode.NotAcceptable}: You cannot interact with this company!");
            return Ok(await userService.CreateJwtWithRoles(user, company));
        }
        catch (Exception e)
        {
            await Task.Delay(5000);
            return StatusCode(HttpStatusCode.BadRequest.GetHashCode(), $"{e.Message}");
        }
    }
    
    [HttpGet("/addCompanyPictur/{companyId:guid}")]
    public async Task<ActionResult<CompanyPictureDto>> AddCompanyPictureAsync(Guid companyId)
    {
        try
        {
            var admin = await GetUser();
            return Ok(await userService.GetCompanyPictureDto(companyId, admin));
        }
        catch (Exception e)
        {
            await Task.Delay(5000);
            return StatusCode((int)HttpStatusCode.BadRequest, e.Message);
        }
    }

    [HttpGet("/getCards/{companyId:guid}/{date:datetime}")]
    public async Task<ActionResult<CompanyEasyVueDto>> GetCards(Guid companyId, DateTime date)
    {
        try
        {
            var admin = await GetUser();
            return Ok(await userService.GetCompanyPictureDto(companyId, admin));
        }
        catch (Exception e)
        {
            await Task.Delay(5000);
            return StatusCode((int)HttpStatusCode.BadRequest, e.Message);
        }
    }
        
}