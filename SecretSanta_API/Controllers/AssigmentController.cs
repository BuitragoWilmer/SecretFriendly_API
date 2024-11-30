using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Services.DTO;
using Services.Repository;
using Services.Service;

namespace SecretSanta_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssigmentController : Controller
    {
        private readonly IAssigmentService _AssigmentService;
        public AssigmentController(IAssigmentService AssigmentRepository)
        {
            _AssigmentService = AssigmentRepository;
        }


        [HttpGet("Generate/{RoundId}")]
        public async Task<ActionResult> GetAssigment(int RoundId)
        {
            await _AssigmentService.assigmentByGroupAsync(RoundId);
   
            return Ok("Se han asignado correctamente");
        }

        [HttpGet("{idUser}/{idRound}")]
        public async Task<ActionResult<AssigmentsDto>> Get(int idUser, int idRound)
        {
            var user = await _AssigmentService.GetAssigmentDtoByIdAsync(idUser, idRound);
            if (user == null)
                return NotFound();

            return Ok(user);
        }

    }
}
