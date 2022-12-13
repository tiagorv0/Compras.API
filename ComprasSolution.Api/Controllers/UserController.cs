using ComprasSolution.Application.DTOs;
using ComprasSolution.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ComprasSolution.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("token")]
        public async Task<IActionResult> PostAsync([FromForm] UserDTO userDto)
        {
            var result = await _userService.GenerateTokenAsync(userDto);
            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);
        }
    }
}
