using Chameleon.Business.Dto;
using Chameleon.Business.Dtos;
using Chameleon.Business.Services;
using Microsoft.AspNetCore.Mvc;

namespace Chameleon.Controller.UserControllerTest
{
    [ApiController]
    [Route("/user")]
    public class UserControllerTest(Context context) : ControllerBase
    {
        [HttpPost("/add")]
        [ProducesResponseType(typeof(UserDto), 200)]
        [ProducesResponseType(400)]
        public IActionResult CreateUserDto([FromBody] CreationUserDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdUser = new CreationUserServiceBase(context).CreateEntity1(dto);
            return Ok(createdUser);
        }

        [HttpGet("/get")]
        public IActionResult get()
        {
            return Ok("test");
        }
    }
}