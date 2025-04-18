using System.Net;
using Chameleon.Application.HumanSetting.Business.Dtos;
using Chameleon.Application.HumanSetting.Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Chameleon.Application.ApiInput.PermissionRequested_Super_Admin_and_Admin;

[Authorize(Roles = "SUPER_ADMIN, ADMIN")]
public class UserJcController(IHttpContextAccessor cc, Context context, UserService userService) : BaseController(cc, context)
{
    [HttpGet(("/companyUsers/{companyGuid:guid}"))]
    public async Task<ActionResult<SimpleUserDto>> GetCompanyUser(Guid companyGuid)
    {
        try
        {
            await GetAdmin(companyGuid);
            return Ok(await userService.GetCompanyUser(companyGuid));
        }
        catch (Exception e)
        {
            return StatusCode(HttpStatusCode.Unauthorized.GetHashCode(),
                $"Error {HttpStatusCode.Unauthorized.GetHashCode()} {HttpStatusCode.Unauthorized}: {e.Message}!");
        }
    }

    [HttpPost("/addUSer/{companyGuid:guid}")]
    public async Task<ActionResult> AddUsers([FromBody] CreationUserDto dto, Guid companyGuid)
    {
        try
        {
            await GetAdmin(companyGuid);
            await userService.CreateUsers(dto, companyGuid);
            return Ok();
        }
        catch (Exception e)
        {
            return StatusCode(HttpStatusCode.Unauthorized.GetHashCode(),
                $"Error {HttpStatusCode.Unauthorized.GetHashCode()} {HttpStatusCode.Unauthorized}: {e.Message}!");
        }
    }
    // [HttpGet(("/isKnown/{companyGuid:guid}/{isKnown}"))]
    // public IActionResult IsKnown(Guid companyGuid, string isKnown)
    // {
    //     try
    //     {
    //         var company = context.Companies.FirstOrDefault(c => c.Id.Equals(companyGuid));
    //         if (company == null) throw new Exception("Company not found!");
    //         return Ok(userService.isKnown(company, isKnown));
    //     }
    //     catch (Exception e)
    //     {
    //         return StatusCode(HttpStatusCode.NotFound.GetHashCode(),
    //             $"Error {HttpStatusCode.NotFound.GetHashCode()} {HttpStatusCode.NotFound}: {e.Message}!");
    //     }
    // }

}