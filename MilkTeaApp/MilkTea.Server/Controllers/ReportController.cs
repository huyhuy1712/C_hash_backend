using Microsoft.AspNetCore.Mvc;

namespace MilkTea.Server.Controllers
{
    public class ReportController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
