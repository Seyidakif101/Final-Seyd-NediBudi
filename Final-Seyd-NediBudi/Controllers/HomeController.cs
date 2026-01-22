using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Final_Seyd_NediBudi.Controllers
{
    public class HomeController : Controller
    {
       

        public IActionResult Index()
        {
            return View();
        }

    }
}
