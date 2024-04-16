using System.Net;
using Chameleon.Application.CompanySetting.Business.Dtos;
using Chameleon.Application.CompanySetting.Business.Services;
using Microsoft.AspNetCore.Mvc;

namespace Chameleon.Application.ApiInput.PermissionRequested;

public class CardApostleController(IHttpContextAccessor cc, Context context) : BaseController(cc, context)
{
    [HttpPost("/{companyGuid:guid}")]
    public IActionResult CreateEntity(Guid companyGuid, [FromBody] CardDto dto)
    {
        try
        {
            UserMatchCompany(companyGuid);
            return Ok(new CardService(Context).CreateEntity(Getcompany(companyGuid), dto));
        }
        catch (Exception)
        {
            return StatusCode(HttpStatusCode.NotAcceptable.GetHashCode(), $"Error {HttpStatusCode.NotAcceptable.GetHashCode()} {HttpStatusCode.NotAcceptable}: Card has not been created!");
        }
    }
}