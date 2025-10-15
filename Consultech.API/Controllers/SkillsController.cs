using Microsoft.AspNetCore.Mvc;

namespace Consultech.API.Controllers
{
    public class SkillsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
