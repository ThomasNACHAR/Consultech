using Consultech.API.Models;
using Consultech.Business.Abstractions;
using Consultech.Business.DTOs;
using Consultech.DAL;
using Consultech.DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Consultech.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MissionsController : ControllerBase
    {
        private readonly IMissionService _missionService;

        public MissionsController(IMissionService missionService)
        {
            _missionService = missionService;
        }

        // GET: api/missions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Mission>>> GetAll()
        {
            var missions = await _missionService.GetAll();

            return Ok(missions);
        }

        // GET: api/missions/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Mission>> GetById(int id)
        {
            var mission = await _missionService.GetById(id);

            if (mission == null)
                return NotFound(new { message = "Mission non trouvée." });

            return Ok(mission);
        }

        // POST: api/missions
        [HttpPost]
        public async Task<ActionResult<Mission>> Create([FromBody] MissionInput mission)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdId = await _missionService.Create(new MissionDto
            {
                Title = mission.Title,
                Description = mission.Description,
                StartDate = mission.StartDate,
                EndDate = mission.EndDate,
                Budget = mission.Budget,
                Client = new ClientDto { Id = mission.ClientId },
                Consultant = mission.ConsultantId.HasValue ? new ConsultantDto { Id = mission.ConsultantId.Value } : null
            });

            return CreatedAtAction(nameof(GetById), new { id = mission.Id }, mission);
        }

        // PUT: api/missions/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, MissionInput mission)
        {
            if (id != mission.Id)
                return BadRequest(new { message = "L'ID ne correspond pas à la mission envoyée." });

            var updatedId = await _missionService.Update(new MissionDto
            {
                Id = mission.Id,
                Title = mission.Title,
                Description = mission.Description,
                StartDate = mission.StartDate,
                EndDate = mission.EndDate,
                Budget = mission.Budget,
                Client = new ClientDto { Id = mission.ClientId },
                Consultant = mission.ConsultantId.HasValue ? new ConsultantDto { Id = mission.ConsultantId.Value } : null
            });
            if (updatedId <= 0)
                return NotFound(new { message = "Mission introuvable." });

            return NoContent();
        }

        // DELETE: api/missions/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _missionService.Delete(id);
            if (!deleted)
                return NotFound(new { message = "Mission introuvable." });



            return NoContent();
        }


    }
}
