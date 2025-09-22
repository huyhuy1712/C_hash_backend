using Microsoft.AspNetCore.Mvc;

namespace MilkTea.Server.Controllers
{
    public class DiscountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
