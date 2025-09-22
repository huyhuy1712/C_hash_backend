using Microsoft.AspNetCore.Mvc;

namespace MilkTea.Server.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
