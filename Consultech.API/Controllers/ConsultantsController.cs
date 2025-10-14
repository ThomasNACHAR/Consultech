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
        private readonly ConsultechDbContext _context;

        public ConsultantsController(ConsultechDbContext context)
        {
            _context = context;
        }

        // GET: api/consultants
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Consultant>>> GetAll()
        {
            var consultants = await _context.Consultants
                .Include(c => c.Skills)
                .ToListAsync();

            return Ok(consultants);
        }

        // GET: api/consultants/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Consultant>> GetById(int id)
        {
            var consultant = await _context.Consultants
                .Include(c => c.Skills)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (consultant == null)
                return NotFound(new { message = "Consultant non trouvé." });

            return Ok(consultant);
        }

        // POST: api/consultants
        [HttpPost]
        public async Task<ActionResult> Create(Consultant consultant)
        {
            if (consultant == null)
                return BadRequest("Les données du consultant sont invalides.");

            _context.Consultants.Add(consultant);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = consultant.Id }, consultant);
        }

        // PUT: api/consultants/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Consultant consultant)
        {
            if (id != consultant.Id)
                return BadRequest("L’ID ne correspond pas.");

            _context.Entry(consultant).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Consultants.Any(c => c.Id == id))
                    return NotFound(new { message = "Consultant introuvable." });

                throw;
            }

            return NoContent();
        }

        // DELETE: api/consultants/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var consultant = await _context.Consultants.FindAsync(id);
            if (consultant == null)
                return NotFound(new { message = "Consultant introuvable." });

            _context.Consultants.Remove(consultant);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}

