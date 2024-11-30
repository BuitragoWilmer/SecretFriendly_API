using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Services.DTO;
using Services.Repository;
using Services.Service;

namespace SecretSanta_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsFamilyController : Controller
    {
        private readonly IGroupFamilyService _gpService;
        public GroupsFamilyController(IGroupFamilyService userRepository)
        {
            _gpService = userRepository;
        }

        [HttpPost]
        public async Task<ActionResult<GroupsFamilyOperationDto>> PostGroupFamily(GroupsFamilyDto user)
        {
            await _gpService.AddAsync(user);
            return Ok();
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<GroupsFamilyOperationDto>> GetGroupFamily(int id)
        {
            var user = await _gpService.GetGroupFamilyByIdAsync(id);
            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GroupsFamilyOperationDto>>> GetGroupFamilys()
        {
            var users = await _gpService.GetAllGroupsFamilyAsync();
            return Ok(users);
        }


        [HttpDelete("{id}")]
        public ActionResult DeleteGroupFamily(int id)
        {
             _gpService.DeleteGroupsFamilyAsync(id);
            return NoContent();
        }
    }
}
