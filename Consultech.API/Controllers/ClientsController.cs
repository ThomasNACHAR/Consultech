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
    public class ClientsController : ControllerBase
    {
        private readonly IClientService _clientService;

        public ClientsController(IClientService clientService)
        {
            _clientService = clientService;
        }

        // GET: api/clients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Client>>> GetAll()
        {
            var clients = await _clientService.GetAll();
            return Ok(clients);
        }

        // GET: api/clients/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Client>> GetById(int id)
        {
            var client = await _clientService.GetById(id);
            if (client == null)
                return NotFound(new { message = "Client non trouvé." });

            return Ok(client);
        }

        // POST: api/clients
        [HttpPost]
        public async Task<ActionResult<Client>> Create([FromBody] ClientDto client)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var createdId = await _clientService.Create(client);
                return CreatedAtAction(nameof(GetById), new { id = createdId }, client);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // PUT: api/clients/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ClientDto client)
        {
            if (id != client.Id)
                return BadRequest(new { message = "L'ID ne correspond pas à l'entité fournie." });

            try
            {
                var updatedId = await _clientService.Update(client);
                if (updatedId <= 0)
                    return NotFound(new { message = "Client introuvable." });

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // DELETE: api/clients/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var deleted = await _clientService.Delete(id);
                if (!deleted)
                    return NotFound(new { message = "Client introuvable." });

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        [HttpGet("{id}/missions")]
        public async Task<ActionResult<IEnumerable<Mission>>> GetMissionsByClient(int id)
        {
            var missions = await _clientService.GetMissionsByClient(id);
            if (missions == null || !missions.Any())
                return NotFound(new { message = "Aucune mission trouvée pour ce client." });

            return Ok(missions);
        }
    }
}

