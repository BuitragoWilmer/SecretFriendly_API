using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Services.DTO;
using Services.Repository;
using Services.Service;

namespace SecretSanta_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GiftsController : Controller
    {
        private readonly IGiftService _GiftService;
        public GiftsController(IGiftService userRepository)
        {
            _GiftService = userRepository;
        }

        [HttpPost]
        public async Task<ActionResult<GiftDto>> Post(GiftOperationDto user)
        {
            await _GiftService.AddAsync(user);
            return Ok();
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<GiftDto>> Get(int id)
        {
            var user = await _GiftService.GetGiftDtoByIdAsync(id);
            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GiftDto>>> Get()
        {
            var users = await _GiftService.GetAllGiftsAsync();
            return Ok(users);
        }
    }
}
