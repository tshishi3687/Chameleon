using Chameleon.Application.Securities;
using Microsoft.AspNetCore.Mvc;

namespace Chameleon.Application.ApiInput.AllowAll;

[ApiController]
[Route("[controller]")]
public class UserController(IHttpContextAccessor cc, IConstente iContent, Context context) : Controller
{
}