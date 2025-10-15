using Consultech.Web.DTOs;
using Consultech.Web.Models.Consultants;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text;
using System.Text.Json;

namespace Consultech.Web.Controllers
{
    public class ConsultantsController : Controller
    {
        private readonly HttpClient _httpClient;

        public ConsultantsController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ConsultechApi");
        }
        // GET: Consultants
        public async Task<ActionResult> Index()
        {
            var consultantsFromApi = await _httpClient.GetFromJsonAsync<IEnumerable<ConsultantDTO>>("api/consultants");
            var consultants = consultantsFromApi.Select(consultantDTO => 
                ConsultantViewModel.FromDTO(consultantDTO)) ?? new List<ConsultantViewModel>();
            return View(consultants);
        }

        // GET: Consultants/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var consultantFromApi = await _httpClient.GetFromJsonAsync<ConsultantDTO>($"api/consultants/{id}");
            var consultant = ConsultantViewModel.FromDTO(consultantFromApi ?? new ConsultantDTO());
            return View(consultant);
        }

        // GET: Consultants/Create
        public async Task<IActionResult> Create()
        {
            ConsultantInputViewModel consultant = new();
            consultant = await this.PopulateListAsync(consultant);
            return View(consultant);
        }

        // POST: Consultants/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FirstName,LastName,Email,StartDate,IsAvailable,SkillsId")] ConsultantInputViewModel consultant)
        {
            if (ModelState.IsValid)
            {
                var response = await _httpClient.PostAsJsonAsync("api/consultants", consultant);

                if (response.IsSuccessStatusCode)
                    return RedirectToAction(nameof(Index));

                else
                    ModelState.AddModelError(string.Empty, "Une erreur est survenue lors de la création du consultant.");

                return RedirectToAction(nameof(Index));
            }

            return View(await PopulateListAsync(consultant));
        }

        // GET: Consultants/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var httpResponse = await this._httpClient.GetAsync($"api/consultants/{id}");

            if (!httpResponse.IsSuccessStatusCode)
                return NotFound();

            var consultantFromApi = await httpResponse.Content.ReadFromJsonAsync<ConsultantDTO>() ?? new();

            var consultant = new ConsultantInputViewModel
            {
                Id = id,
                FirstName = consultantFromApi.FirstName,
                LastName = consultantFromApi.LastName,
                Email = consultantFromApi.Email,
                StartDate = consultantFromApi.StartDate,
                IsAvailable = consultantFromApi.IsAvailable,
                SkillsId = consultantFromApi.Skills.Select(skill => skill.Id).ToList()
            };

            consultant = await this.PopulateListAsync(consultant);
            return View(consultant);
        }

        // POST: Consultants/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,Email,StartDate,IsAvailable,SkillsId")] ConsultantInputViewModel consultant)
        {
            if (id != consultant.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                var json = JsonSerializer.Serialize(consultant);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await this._httpClient.PutAsync($"api/consultants/{consultant.Id}", content);
                if (response.IsSuccessStatusCode)
                    return RedirectToAction(nameof(Index));
                else
                    ModelState.AddModelError(string.Empty, "Une erreur est survenue lors de la modification du consultant.");
            }
            return View(await PopulateListAsync(consultant));
        }

        // GET: Consultants/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var httpResponse = await this._httpClient.GetAsync($"api/consultants/{id}");

            if (!httpResponse.IsSuccessStatusCode)
                return NotFound();

            var consultantFromApi = await httpResponse.Content.ReadFromJsonAsync<ConsultantDTO>() ?? new();
            var consultant = ConsultantViewModel.FromDTO(consultantFromApi);

            return View(consultant);
        }

        // POST: Consultants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var httpResponse = await this._httpClient.DeleteAsync($"api/consultants/{id}");

            if (httpResponse.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            return NotFound();
        }

        private async Task<ConsultantInputViewModel> PopulateListAsync(ConsultantInputViewModel consultant)
        {
            var skillsFromApi = await _httpClient.GetFromJsonAsync<IEnumerable<SkillDTO>>("api/skills");
            consultant.Skills = skillsFromApi.Select(skill => new SelectListItem {
                Value = skill.Id.ToString(), Text = skill.Title 
            }).ToList();
            return consultant;
        }
    }
}
