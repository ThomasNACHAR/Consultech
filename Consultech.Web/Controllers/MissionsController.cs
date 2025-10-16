using Consultech.Web.DTOs;
using Consultech.Web.Models.Consultants;
using Consultech.Web.Models.Missions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text;
using System.Text.Json;

namespace Consultech.Web.Controllers
{
    public class MissionsController : Controller
    {
        private readonly HttpClient _httpClient;
        public MissionsController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ConsultechApi");
        }
        // GET: MissionsController
        public async Task<ActionResult> Index()
        {
            var missionsFromApi = await _httpClient.GetFromJsonAsync<IEnumerable<MissionDTO>>("api/missions");
            var missions = missionsFromApi.Select(missionDTO =>
                MissionViewModel.FromDTO(missionDTO)) ?? new List<MissionViewModel>();
            return View(missions);
        }

        // GET: MissionsController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var missionFromApi = await _httpClient.GetFromJsonAsync<MissionDTO>($"api/missions/{id}");
            var missions = MissionViewModel.FromDTO(missionFromApi ?? new MissionDTO());
            return View(missions);
        }

        // GET: MissionsController/Create
        public async Task<IActionResult> Create()
        {
            MissionInputViewModel mission = new();
            mission = await this.PopulateListAsync(mission);
            return View(mission);
        }

        // POST: MissionsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title","Description","StartDate","EndDate","Budget","ClientId","ConsultantId")] MissionInputViewModel mission)
        {
            if (ModelState.IsValid)
            {
                var response = await _httpClient.PostAsJsonAsync("api/missions", mission);

                if (response.IsSuccessStatusCode)
                    return RedirectToAction(nameof(Index));

                else
                    ModelState.AddModelError(string.Empty, "Une erreur est survenue lors de la création de la mission.");

                return RedirectToAction(nameof(Index));
            }

            return View(await PopulateListAsync(mission));
        }

        // GET: MissionsController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var httpResponse = await this._httpClient.GetAsync($"api/missions/{id}");

            if (!httpResponse.IsSuccessStatusCode)
                return NotFound();

            var missionFromApi = await httpResponse.Content.ReadFromJsonAsync<MissionDTO>() ?? new();

            var mission = new MissionInputViewModel
            {
                Id = id,
                Title = missionFromApi.Title,
                Description = missionFromApi.Description,
                StartDate = missionFromApi.StartDate,
                EndDate = missionFromApi.EndDate,
                Budget = missionFromApi.Budget,
                ClientId = missionFromApi.Client.Id,
                ConsultantId = missionFromApi.Consultant?.Id
            };

            mission = await this.PopulateListAsync(mission);
            return View(mission);
        }

        // POST: MissionsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Title", "Description", "StartDate", "EndDate", "Budget", "ClientId", "ConsultantId")] MissionInputViewModel mission)
        {
            if (id != mission.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                var json = JsonSerializer.Serialize(mission);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await this._httpClient.PutAsync($"api/missions/{id}", content);
                if (response.IsSuccessStatusCode)
                    return RedirectToAction(nameof(Index));
            }
            return View(await PopulateListAsync(mission));
        }

        // GET: MissionsController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var httpResponse = await this._httpClient.GetAsync($"api/missions/{id}");

            if (!httpResponse.IsSuccessStatusCode)
                return NotFound();

            var missionFromApi = await httpResponse.Content.ReadFromJsonAsync<MissionDTO>() ?? new();
            var mission = MissionViewModel.FromDTO(missionFromApi);

            return View(mission);
        }

        // POST: MissionsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, IFormCollection collection)
        {
            var httpResponse = await this._httpClient.DeleteAsync($"api/missions/{id}");

            if (httpResponse.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            return NotFound();
        }
        private async Task<MissionInputViewModel> PopulateListAsync(MissionInputViewModel mission)
        {
            var httpResponse = await this._httpClient.GetAsync("api/clients");
            if (httpResponse.IsSuccessStatusCode)
            {
                var clientsFromApi = await httpResponse.Content.ReadFromJsonAsync<IEnumerable<ClientDTO>>();
                mission.Clients = clientsFromApi?
                    .Select(client => new SelectListItem
                    {
                        Text = client.CompanyName,
                        Value = client.Id.ToString()
                    }).ToList() ?? new();
            }

            httpResponse = await this._httpClient.GetAsync("api/consultants");
            if (httpResponse.IsSuccessStatusCode)
            {
                var consultantsFromApi = await httpResponse.Content.ReadFromJsonAsync<IEnumerable<ConsultantDTO>>();
                mission.Consultants = consultantsFromApi?
                    .Select(consultant => new SelectListItem
                    {
                        Text = $"{consultant.FirstName} {consultant.LastName}",
                        Value = consultant.Id.ToString()
                    }).ToList() ?? new();
            }

            return mission;
        }

    }
}
