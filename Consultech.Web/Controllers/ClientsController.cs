using System.Text.Json;
using Consultech.Web.DTOs;
using Consultech.Web.Models.Clients;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Consultech.Web.Controllers
{
    public class ClientsController : Controller
    {
        private readonly HttpClient _httpClient;

        public ClientsController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ConsultechApi");
        }

        //  GET: Clients
        public async Task<ActionResult> Index()
        {
            var clientsFromApi = await _httpClient.GetFromJsonAsync<IEnumerable<ClientDTO>>("api/clients");
            var clients = clientsFromApi?.Select(ClientViewModel.FromDTO) ?? new List<ClientViewModel>();
            return View(clients);
        }

        //  GET: Clients/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var clientFromApi = await _httpClient.GetFromJsonAsync<ClientDTO>($"api/clients/{id}");
            if (clientFromApi == null)
                return NotFound();

            var client = ClientViewModel.FromDTO(clientFromApi);
            return View(client);
        }

        //  GET: Clients/Create
        public IActionResult Create()
        {
            var client = PopulateActivitySectors(new ClientInputViewModel());
            return View(client);
        }

        //  POST: Clients/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CompanyName,Email,Address,ActivitySectorId")] ClientInputViewModel client)
        {
            if (ModelState.IsValid)
            {
                var dto = new ClientDTO
                {
                    CompanyName = client.CompanyName,
                    Email = client.Email,
                    Address = client.Address,
                    ActivitySector = (DTOs.Enums.ActivitySector)client.ActivitySectorId
                };

                var response = await _httpClient.PostAsJsonAsync("api/clients", dto);

                if (response.IsSuccessStatusCode)
                    return RedirectToAction(nameof(Index));

                ModelState.AddModelError(string.Empty, "Une erreur est survenue lors de la création du client.");
            }

            return View(PopulateActivitySectors(client));
        }

        //  GET: Clients/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var httpResponse = await _httpClient.GetAsync($"api/clients/{id}");
            if (!httpResponse.IsSuccessStatusCode)
                return NotFound();

            var clientFromApi = await httpResponse.Content.ReadFromJsonAsync<ClientDTO>() ?? new();

            var client = new ClientInputViewModel
            {
                Id = clientFromApi.Id,
                CompanyName = clientFromApi.CompanyName,
                Email = clientFromApi.Email,
                Address = clientFromApi.Address,
                ActivitySectorId = (int)clientFromApi.ActivitySector
            };

            return View(PopulateActivitySectors(client));
        }

        // POST: Clients/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CompanyName,Email,Address,ActivitySectorId")] ClientInputViewModel client)
        {
            if (id != client.Id)
                return BadRequest();

            if (ModelState.IsValid)
            {
                var dto = new ClientDTO
                {
                    Id = client.Id,
                    CompanyName = client.CompanyName,
                    Email = client.Email,
                    Address = client.Address,
                    ActivitySector = (DTOs.Enums.ActivitySector)client.ActivitySectorId
                };

                var json = JsonSerializer.Serialize(dto);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync($"api/clients/{id}", content);

                if (response.IsSuccessStatusCode)
                    return RedirectToAction(nameof(Index));

                ModelState.AddModelError(string.Empty, "Une erreur est survenue lors de la modification du client.");
            }

            return View(PopulateActivitySectors(client));
        }

        // GET: Clients/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var clientFromApi = await _httpClient.GetFromJsonAsync<ClientDTO>($"api/clients/{id}");
            if (clientFromApi == null)
                return NotFound();

            var client = ClientViewModel.FromDTO(clientFromApi);
            return View(client);
        }

        // POST: Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/clients/{id}");
            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            return NotFound();
        }

        // 🔹 Liste déroulante des secteurs d’activité
        private ClientInputViewModel PopulateActivitySectors(ClientInputViewModel client)
        {
            var sectors = Enum.GetValues(typeof(DTOs.Enums.ActivitySector))
                .Cast<DTOs.Enums.ActivitySector>()
                .Select(sector => new SelectListItem
                {
                    Value = ((int)sector).ToString(),
                    Text = sector.ToString()
                })
                .ToList();

            client.ActivitySectors = sectors;
            return client;
        }
    }
}
