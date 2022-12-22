using Microsoft.AspNetCore.Mvc;

namespace WebProje1.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
