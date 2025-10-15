using Consultech.API.Models;
using Consultech.Business.Abstractions;
using Consultech.Business.DTOs;
using Consultech.DAL;
using Consultech.DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConsultTech.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsultantsController : ControllerBase
    {
        private readonly IConsultantService _consultantService;

        public ConsultantsController(IConsultantService consultantService)
        {
            _consultantService = consultantService;
        }

        // GET: api/consultants
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Consultant>>> GetAll()
        {
            var consultants = await _consultantService.GetAll();

            return Ok(consultants);
        }

        // GET: api/consultants/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Consultant>> GetById(int id)
        {
            var consultant = await _consultantService.GetById(id);

            if (consultant == null)
                return NotFound(new { message = "Consultant non trouvé." });

            return Ok(consultant);
        }

        // POST: api/consultants
        [HttpPost]
        public async Task<ActionResult> Create(ConsultantInput consultant)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdId = await _consultantService.Create(new ConsultantDto
            {
                FirstName = consultant.FirstName,
                LastName = consultant.LastName,
                Email = consultant.Email,
                StartDate = consultant.StartDate,
                IsAvailable = consultant.IsAvailable,
                Skills = consultant.SkillsId.Select(id => new SkillDto { Id = id }).ToList()
            });

            return CreatedAtAction(nameof(GetById), new { id = consultant.Id }, consultant);
        }

        // PUT: api/consultants/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ConsultantInput consultant)
        {
            if (id != consultant.Id)
                return BadRequest("L’ID ne correspond pas.");

            var updatedId = await _consultantService.Update(new ConsultantDto
            {
                Id = consultant.Id,
                FirstName = consultant.FirstName,
                LastName = consultant.LastName,
                Email = consultant.Email,
                StartDate = consultant.StartDate,
                IsAvailable = consultant.IsAvailable,
                Skills = consultant.SkillsId.Select(id => new SkillDto { Id = id }).ToList()
            });
            if (updatedId <= 0)
                return NotFound(new { message = "Consultant introuvable." });

            return NoContent();
        }

        // DELETE: api/consultants/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _consultantService.Delete(id);
            if (!deleted)
                return NotFound(new { message = "Consultant introuvable." });

            return NoContent();
        }
    }
}

