using Microsoft.AspNetCore.Mvc;

namespace PastryShop.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
