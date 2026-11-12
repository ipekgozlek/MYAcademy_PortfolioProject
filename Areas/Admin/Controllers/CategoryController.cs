using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MYAcademy_PortfolioProject.Context;
using MYAcademy_PortfolioProject.Entities;

namespace MYAcademy_PortfolioProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly PortfolioContext _context;

        public CategoryController(PortfolioContext context)
        {
            _context = context;
        }

        public IActionResult Index() => RedirectToAction("CategoryList");

        public IActionResult CategoryList()
        {
            var values = _context.Categories.Include(x => x.Projects).ToList();
            return View(values);
        }

        public IActionResult CreateCategory() => View("Form", new Category());

        public IActionResult UpdateCategory(int id)
        {
            var value = _context.Categories.Find(id);
            if (value == null) return NotFound();
            return View("Form", value);
        }

        [HttpPost]
        public IActionResult Save(Category category)
        {
            if (!ModelState.IsValid) return View("Form", category);

            if (category.CategoryId == 0)
                _context.Categories.Add(category);
            else
                _context.Categories.Update(category);

            _context.SaveChanges();
            TempData["Success"] = "Category saved";
            return RedirectToAction("CategoryList");
        }

        public IActionResult DeleteCategory(int id)
        {
            // Projesi olan kategori silinmesin
            if (_context.Projects.Any(x => x.CategoryId == id))
            {
                TempData["Error"] = "This category has projects. Move or delete them first.";
                return RedirectToAction("CategoryList");
            }

            var value = _context.Categories.Find(id);
            if (value != null)
            {
                _context.Categories.Remove(value);
                _context.SaveChanges();
                TempData["Success"] = "Category deleted";
            }
            return RedirectToAction("CategoryList");
        }
    }
}