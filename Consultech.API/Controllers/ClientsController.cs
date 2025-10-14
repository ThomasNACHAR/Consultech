using Microsoft.AspNetCore.Mvc;

namespace Consultech.API.Controllers
{
    public class ClientsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
