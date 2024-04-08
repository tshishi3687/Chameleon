using Chameleon.Application.Securities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace Chameleon.Application.ApiInput;

public class AbstractController: ControllerBase
{
        
    public AbstractController(IHttpContextAccessor cc, IConstente iContent)
    {
        iContent.UseThisUserConnected(cc.HttpContext.GetTokenAsync("access_token").Result);
    }

}