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
        private readonly ConsultechDbContext _context;

        public MissionsController(ConsultechDbContext context)
        {
            _context = context;
        }

        // GET: api/missions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Mission>>> GetAll()
        {
            var missions = await _context.Missions
                .Include(m => m.Client)
                .Include(m => m.Consultant)
                .ToListAsync();

            return Ok(missions);
        }

        // GET: api/missions/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Mission>> GetById(int id)
        {
            var mission = await _context.Missions
                .Include(m => m.Client)
                .Include(m => m.Consultant)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (mission == null)
                return NotFound(new { message = "Mission non trouvée." });

            return Ok(mission);
        }

        // POST: api/missions
        [HttpPost]
        public async Task<ActionResult<Mission>> Create([FromBody] Mission mission)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Missions.Add(mission);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = mission.Id }, mission);
        }

        // PUT: api/missions/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Mission mission)
        {
            if (id != mission.Id)
                return BadRequest(new { message = "L'ID ne correspond pas à la mission envoyée." });

            var existingMission = await _context.Missions
                .Include(m => m.Client)
                .Include(m => m.Consultant)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (existingMission == null)
                return NotFound(new { message = "Mission introuvable." });

            existingMission.Title = mission.Title;
            existingMission.Description = mission.Description;
            existingMission.StartDate = mission.StartDate;
            existingMission.EndDate = mission.EndDate;
            existingMission.Budget = mission.Budget;
            existingMission.Client = mission.Client;
            existingMission.Consultant = mission.Consultant;

            _context.Entry(existingMission).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/missions/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var mission = await _context.Missions.FindAsync(id);
            if (mission == null)
                return NotFound(new { message = "Mission introuvable." });

            _context.Missions.Remove(mission);
            await _context.SaveChangesAsync();

            return NoContent();
        }


    }
}
