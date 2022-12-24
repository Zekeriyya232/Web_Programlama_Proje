using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebProje1.Controllers
{
    [Authorize(Roles ="admin")]   //roles="admin,manager,user"
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult MovieDesign()
        {
            return View();
        }

        public IActionResult SeriesDesign()
        {
            return View();
        }
    }
}
