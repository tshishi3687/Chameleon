using Chameleon.Business.Dto;
using Chameleon.Business.Dtos;
using Chameleon.Business.Services;
using Microsoft.AspNetCore.Mvc;

namespace Chameleon.Controller.UserControllerTest
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