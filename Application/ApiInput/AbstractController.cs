using Chameleon.Application.Securities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace Chameleon.Application.ApiInput;

public class AbstractController: ControllerBase
{
        
    public AbstractController(IHttpContextAccessor cc, IConstente iContent)
    {
        iContent.UserConnected(cc.HttpContext.GetTokenAsync("access_token").Result);
    }

}