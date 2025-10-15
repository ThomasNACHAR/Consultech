using Consultech.DAL;
using Consultech.DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Consultech.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly ConsultechDbContext _context;

        public ClientsController(ConsultechDbContext context)
        {
            _context = context;
        }

        // GET: api/clients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Client>>> GetAll()
        {
            var clients = await _context.Clients
                .Include(c => c.Missions)
                .ToListAsync();

            return Ok(clients);
        }

        // GET: api/clients/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Client>> GetById(int id)
        {
            var client = await _context.Clients
                .Include(c => c.Missions)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (client == null)
                return NotFound(new { message = "Client non trouvé." });

            return Ok(client);
        }

        // POST: api/clients
        [HttpPost]
        public async Task<ActionResult<Client>> Create([FromBody] Client client)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Clients.Add(client);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = client.Id }, client);
        }

        // PUT: api/clients/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Client client)
        {
            if (id != client.Id)
                return BadRequest(new { message = "L'ID ne correspond pas à l'entité fournie." });

            var existingClient = await _context.Clients.FindAsync(id);
            if (existingClient == null)
                return NotFound(new { message = "Client introuvable." });

            existingClient.CompanyName = client.CompanyName;
            existingClient.Email = client.Email;
            existingClient.Address = client.Address;
            existingClient.ActivitySector = client.ActivitySector;

            _context.Entry(existingClient).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/clients/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var client = await _context.Clients.FindAsync(id);
            if (client == null)
                return NotFound(new { message = "Client introuvable." });

            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        [HttpGet("{id}/missions")]
        public async Task<ActionResult<IEnumerable<Mission>>> GetMissionsByClient(int id)
        {
            var client = await _context.Clients
                .Include(c => c.Missions)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (client == null)
                return NotFound(new { message = "Client non trouvé." });

            return Ok(client.Missions);
        }
    }
}

