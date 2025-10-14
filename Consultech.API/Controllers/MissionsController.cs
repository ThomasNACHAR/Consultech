using Microsoft.AspNetCore.Mvc;

namespace Consultech.API.Controllers
{
    public class MissionsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
