using Microsoft.AspNetCore.Mvc;

namespace Final_Seyd_NediBudi.Areas.Admin.Controllers
{
    public class DashboardController : Controller
    {
        [Area("Admin")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
