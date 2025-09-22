using Microsoft.AspNetCore.Mvc;

namespace MilkTea.Server.Controllers
{
    public class InvoiceController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
