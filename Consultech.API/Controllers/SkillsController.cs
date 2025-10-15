using Consultech.DAL;
using Consultech.DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Consultech.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkillsController : ControllerBase
    {
        private readonly ConsultechDbContext _context;

        public SkillsController(ConsultechDbContext context)
        {
            _context = context;
        }

        // GET: api/skills
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Skill>>> GetAll()
        {
            var skills = await _context.Skills.ToListAsync();
            return Ok(skills);
        }

        // GET: api/skills/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Skill>> GetById(int id)
        {
            var skill = await _context.Skills.FindAsync(id);
            if (skill == null)
                return NotFound(new { message = "Compétence non trouvée." });
            return Ok(skill);
        }

        // POST: api/skills
        [HttpPost]
        public async Task<ActionResult<Skill>> Create([FromBody] Skill skill)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Skills.Add(skill);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = skill.Id }, skill);
        }

        // PUT: api/skills/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Skill skill)
        {
            if (id != skill.Id)
                return BadRequest(new { message = "L'ID ne correspond pas à la compétence envoyée." });

            var existingSkill = await _context.Skills.FindAsync(id);
            if (existingSkill == null)
                return NotFound(new { message = "Compétence introuvable." });

            existingSkill.Title = skill.Title;
            existingSkill.Category = skill.Category;
            existingSkill.Level = skill.Level;

            _context.Entry(existingSkill).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/skills/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var skill = await _context.Skills.FindAsync(id);
            if (skill == null)
                return NotFound(new { message = "Compétence introuvable." });

            _context.Skills.Remove(skill);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
