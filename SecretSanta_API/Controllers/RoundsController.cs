using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Services.DTO;
using Services.Repository;
using Services.Service;

namespace SecretSanta_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoundsController : Controller
    {
        private readonly IRoundService _RoundService;
        public RoundsController(IRoundService userRepository)
        {
            _RoundService = userRepository;
        }

        [HttpPost]
        public async Task<ActionResult<RoundDto>> Post(RoundDto user)
        {
            int id = await _RoundService.AddAsync(user);
            return Ok(id);
        }

        [HttpPost]
        [Route("User")]
        public async Task<ActionResult<UserRoundDto>> Post(UserRoundDto user)
        {
            int id = await _RoundService.AddUsertoRoundAsync(user);
            return Ok(id);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<RoundDto>> Get(int id)
        {
            var user = await _RoundService.GetRoundByIdAsync(id);
            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> Get()
        {
            var users = await _RoundService.GetRoundAsync();
            return Ok(users);
        }
    }
}
