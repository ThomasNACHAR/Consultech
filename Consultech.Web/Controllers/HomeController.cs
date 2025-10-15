using Microsoft.AspNetCore.Mvc;

namespace Consultech.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
