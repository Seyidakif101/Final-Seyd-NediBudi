using System.Diagnostics;
using Final_Seyd_NediBudi.Context;
using Final_Seyd_NediBudi.ViewModels.EmployeeViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Final_Seyd_NediBudi.Controllers
{
    public class HomeController(AppDbContext _context) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var employees = await _context.Employees.Select(x => new EmployeeGetVM()
            {
                Id=x.Id,
                Name = x.Name,
                ImagePath = x.ImagePath,
                PositionName = x.Position.Name
            }).ToListAsync();
            return View(employees);
        }
    }
}
