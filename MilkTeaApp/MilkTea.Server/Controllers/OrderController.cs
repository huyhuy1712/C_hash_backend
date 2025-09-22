using Microsoft.AspNetCore.Mvc;

namespace MilkTea.Server.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
