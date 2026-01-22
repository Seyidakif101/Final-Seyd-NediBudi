using System.Threading.Tasks;
using Final_Seyd_NediBudi.Context;
using Final_Seyd_NediBudi.Helper;
using Final_Seyd_NediBudi.Models;
using Final_Seyd_NediBudi.ViewModels.EmployeeViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Final_Seyd_NediBudi.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="Admin")]
    public class EmployeeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _environment;
        private readonly string _folderPath;

        public EmployeeController(AppDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
            _folderPath = Path.Combine(_environment.WebRootPath, "images");
        }

        public async Task<IActionResult> Index()
        {
            var employees = await _context.Employees.Select(x => new EmployeeGetVM()
            {
                Id=x.Id,
                Name=x.Name,
                ImagePath=x.ImagePath,
                PositionName=x.Position.Name
            }).ToListAsync();
            return View(employees);
        }
        public async Task<IActionResult> Create()
        {
            await _sendPositionViewBag();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(EmployeeCreateVM vm)
        {
            await _sendPositionViewBag();
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            var existPosition = await _context.Positions.AnyAsync(x => x.Id == vm.PositionId);
            if (!existPosition)
            {
                ModelState.AddModelError("PositionId", "Bele bir position yoxdur");
                return View(vm);
            }
            if (!vm.Image.CheckSize(2))
            {
                ModelState.AddModelError("Image", "Olcu 2 mb cox ola bilmez");
                return View(vm);
            }
            if (!vm.Image.CheckType("image"))
            {
                ModelState.AddModelError("Image", "File Image tipinde olmalidi");
                return View(vm);
            }
            string uniqueFileName = await vm.Image.FileUploadAsync(_folderPath);
            Employee employee = new()
            {
                Name=vm.Name,
                ImagePath=uniqueFileName,
                PositionId=vm.PositionId
            };
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if(employee is null)
            {
                return NotFound();
            }
            EmployeeUpdateVM vm = new()
            {
                Name=employee.Name,
                PositionId=employee.PositionId
            };
            await _sendPositionViewBag();
            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> Update(EmployeeUpdateVM vm)
        {
            await _sendPositionViewBag();
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            var existPosition = await _context.Positions.AnyAsync(x => x.Id == vm.PositionId);
            if (!existPosition)
            {
                ModelState.AddModelError("PositionId", "Bele bir position yoxdur");
                return View(vm);
            }
            if (!vm.Image?.CheckSize(2)??false)
            {
                ModelState.AddModelError("Image", "Olcu 2 mb cox ola bilmez");
                return View(vm);
            }
            if (!vm.Image?.CheckType("image")??false)
            {
                ModelState.AddModelError("Image", "File Image tipinde olmalidi");
                return View(vm);
            }
            var existEmployee = await _context.Employees.FindAsync(vm.Id);
            if (existEmployee is null)
            {
                return BadRequest();
            }
            existEmployee.Name = vm.Name;
            existEmployee.PositionId = vm.PositionId;
            if(vm.Image is { })
            {
                string uniqueFileName = await vm.Image.FileUploadAsync(_folderPath);
                string oldFileName = Path.Combine(_folderPath, existEmployee.ImagePath);
                FileHelper.FileDelete(oldFileName);
                existEmployee.ImagePath = uniqueFileName;
            }
            _context.Employees.Update(existEmployee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
        public async Task<IActionResult> Delete(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee is null)
            {
                return NotFound();
            }
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            string deleteFilName = Path.Combine(_folderPath, employee.ImagePath);
            FileHelper.FileDelete(deleteFilName);
            return RedirectToAction(nameof(Index));

        }

        private async Task _sendPositionViewBag()
        {
            var position = await _context.Positions.Select(x => new SelectListItem()
            {
                Value=x.Id.ToString(),
                Text=x.Name
            }).ToListAsync();
            ViewBag.Positions = position;
        }
    }
}
