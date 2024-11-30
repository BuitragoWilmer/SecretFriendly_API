using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Services.DTO;
using Services.Repository;
using Services.Service;

namespace SecretSanta_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userRepository)
        {
            _userService = userRepository;
        }

        [HttpPost]
        public async Task<ActionResult<UserDto>> PostUser(UserCreateDto user)
        {
            await _userService.AddAsync(user);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUserAsync(int id, [FromBody] UserUpdateDto updatedUser)
        {
            // Buscar el usuario por ID
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound(new { Message = "User not found." });
            }
            _userService.UpdateUser(id, updatedUser,user);
            return Ok(new { Message = "User updated successfully.", User = user });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUser(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(int id)
        {
             _userService.DeleteUserAsync(id);
            return NoContent();
        }
    }
}
