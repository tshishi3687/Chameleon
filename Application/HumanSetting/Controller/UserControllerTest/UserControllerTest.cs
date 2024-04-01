using Chameleon.Application.HumanSetting.Business.Dtos;
using Chameleon.Application.HumanSetting.Business.Services;
using Microsoft.AspNetCore.Mvc;

namespace Chameleon.Application.HumanSetting.Controller.UserControllerTest
{
    [ApiController]
    [Route("[controller]")]
    public class UserControllerTest(Context context) : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(UserVueDto), 200)]
        [ProducesResponseType(400)]
        public IActionResult CreateUserDto([FromBody] CreationUserDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(new CreationUserServiceBase(context).CreateEntity1(dto));
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok("test");
        }
    }
}