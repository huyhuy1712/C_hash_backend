using Microsoft.AspNetCore.Mvc;

namespace MilkTea.Server.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
