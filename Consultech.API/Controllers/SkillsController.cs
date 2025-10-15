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
    public class SkillsController : ControllerBase
    {
        private readonly ISkillService _skillService;

        public SkillsController(ISkillService skillService)
        {
            _skillService = skillService;
        }

        // GET: api/skills
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Skill>>> GetAll()
        {
            var skills = await _skillService.GetAll();
            return Ok(skills);
        }

        // GET: api/skills/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Skill>> GetById(int id)
        {
            var skill = await _skillService.GetById(id);
            if (skill == null)
                return NotFound(new { message = "Compétence non trouvée." });
            return Ok(skill);
        }

        // POST: api/skills
        [HttpPost]
        public async Task<ActionResult<Skill>> Create([FromBody] SkillDto skill)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdId = await _skillService.Create(skill);

            return CreatedAtAction(nameof(GetById), new { id = skill.Id }, skill);
        }

        // PUT: api/skills/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] SkillDto skill)
        {
            if (id != skill.Id)
                return BadRequest(new { message = "L'ID ne correspond pas à la compétence envoyée." });

            var updatedId = await _skillService.Update(skill);
            if (updatedId <= 0)
                return NotFound(new { message = "Compétence introuvable." });



            return NoContent();
        }

        // DELETE: api/skills/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _skillService.Delete(id);
            if (!deleted)
                return NotFound(new { message = "Compétence introuvable." });



            return NoContent();
        }
    }
}
