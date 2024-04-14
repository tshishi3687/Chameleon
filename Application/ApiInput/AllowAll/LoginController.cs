using System.Net;
using System.Security.Claims;
using Chameleon.Application.HumanSetting.Business.Dtos;
using Chameleon.Application.HumanSetting.Business.Services;
using Chameleon.Application.Securities;
using Microsoft.AspNetCore.Mvc;

namespace Chameleon.Application.ApiInput.AllowAll;

[ApiController]
[Route("[Controller]")]
public class LoginController(IHttpContextAccessor cc, IConstente iContent, Context context)
    
{
    private readonly Constantes _constantes = new(context);

    [HttpPost]
    public async Task<IActionResult> Login([FromBody] LoggerDto dto)
    {
        try
        {
            var user = new UserService(context).Login(dto);
            var claims = new List<Claim> { new(ClaimTypes.Email, user.Email) };
            var response = new HttpResponseMessage(HttpStatusCode.Accepted);
            response.Headers.Add("user", user.FirstName);
            response.Headers.Add("Authorization", "Bearer " + _constantes.GenerateToken(claims));
            return new ObjectResult(response) { StatusCode = (int)response.StatusCode };
        }
        catch (Exception)
        {
            await Task.Delay(3000);
            var response = new HttpResponseMessage(HttpStatusCode.BadRequest);
            return new ObjectResult(response)
            {
                StatusCode = (int)response.StatusCode
            };
        }
    }
}