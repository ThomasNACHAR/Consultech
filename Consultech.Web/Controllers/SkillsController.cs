using Consultech.Web.DTOs;
using Consultech.Web.DTOs.Enums;
using Consultech.Web.Models.Skills;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text;
using System.Text.Json;

namespace Consultech.Web.Controllers
{
    public class SkillsController : Controller
    {
        private readonly HttpClient _httpClient;

        public SkillsController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ConsultechApi");
        }

        // GET: Skills
        public async Task<IActionResult> Index()
        {
            var skillsFromApi = await _httpClient.GetFromJsonAsync<IEnumerable<SkillDTO>>("api/skills");
            var skills = skillsFromApi.Select(SkillViewModel.FromDTO) ?? new List<SkillViewModel>();
            return View(skills);
        }

        // GET: Skills/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var skillFromApi = await _httpClient.GetFromJsonAsync<SkillDTO>($"api/skills/{id}");
            var skill = SkillViewModel.FromDTO(skillFromApi ?? new SkillDTO());
            return View(skill);
        }

        // GET: Skills/Create
        public IActionResult Create()
        {
            var skill = new SkillInputViewModel();
            skill = PopulateLevels(skill);
            return View(skill);
        }

        // POST: Skills/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Category,Level")] SkillInputViewModel skill)
        {
            if (ModelState.IsValid)
            {
                var response = await _httpClient.PostAsJsonAsync("api/skills", skill);
                if (response.IsSuccessStatusCode)
                    return RedirectToAction(nameof(Index));

                ModelState.AddModelError(string.Empty, "Une erreur est survenue lors de la création de la compétence.");
            }

            return View(PopulateLevels(skill));
        }

        // GET: Skills/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var httpResponse = await _httpClient.GetAsync($"api/skills/{id}");

            if (!httpResponse.IsSuccessStatusCode)
                return NotFound();

            var skillFromApi = await httpResponse.Content.ReadFromJsonAsync<SkillDTO>() ?? new SkillDTO();

            var skill = new SkillInputViewModel
            {
                Id = skillFromApi.Id,
                Title = skillFromApi.Title,
                Category = skillFromApi.Category,
                Level = skillFromApi.Level
            };

            return View(PopulateLevels(skill));
        }

        // POST: Skills/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Category,Level")] SkillInputViewModel skill)
        {
            if (id != skill.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                var json = JsonSerializer.Serialize(skill);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync($"api/skills/{id}", content);

                if (response.IsSuccessStatusCode)
                    return RedirectToAction(nameof(Index));
            }

            return View(PopulateLevels(skill));
        }

        // GET: Skills/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var httpResponse = await _httpClient.GetAsync($"api/skills/{id}");

            if (!httpResponse.IsSuccessStatusCode)
                return NotFound();

            var skillFromApi = await httpResponse.Content.ReadFromJsonAsync<SkillDTO>() ?? new SkillDTO();
            var skill = SkillViewModel.FromDTO(skillFromApi);

            return View(skill);
        }

        // POST: Skills/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var httpResponse = await _httpClient.DeleteAsync($"api/skills/{id}");

            if (httpResponse.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            return NotFound();
        }

        private SkillInputViewModel PopulateLevels(SkillInputViewModel skill)
        {
            skill.Levels = Enum.GetValues(typeof(SkillLevel))
                .Cast<SkillLevel>()
                .Select(level => new SelectListItem
                {
                    Value = ((int)level).ToString(),
                    Text = level.ToString()
                }).ToList();

            return skill;
        }
    }
}
