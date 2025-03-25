using System.Net;
using Chameleon.Application.HumanSetting.Business.Mappers;
using Chameleon.Application.HumanSetting.DataAccess.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Chameleon.Application.ApiInput.AllowAll;

public class GetMe(IHttpContextAccessor cc, Context context): BaseController(cc, context)
{
    [HttpGet("/GetMe")]
    public async Task<IActionResult> GetUsers()
    {
        try
        {
            var users = getUser();
            return Ok(new UserVueMapper().ToDto(users));
        }
        catch (Exception e)
        {
            await Task.Delay(5000);
            return StatusCode((int)HttpStatusCode.BadRequest, e.Message);
        }
    }

    private User getUser()
    {
        var users = Constantes.Connected;

        if (users == null)
        {
            throw new UnauthorizedAccessException("No connection found");
        }
        
        return users;
    }
}