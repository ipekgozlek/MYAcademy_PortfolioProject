using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MYAcademy_PortfolioProject.Context;
using MYAcademy_PortfolioProject.Entities;

namespace MYAcademy_PortfolioProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SkillCategoryController : Controller
    {
        private readonly PortfolioContext _context;

        public SkillCategoryController(PortfolioContext context)
        {
            _context = context;
        }

        public IActionResult Index() => RedirectToAction("SkillCategoryList");

        public IActionResult SkillCategoryList()
        {
            var values = _context.SkillCategories.Include(x => x.Skills).ToList();
            return View(values);
        }

       
        public IActionResult CreateSkillCategory() => View("Form", new SkillCategory { Icon = "bi-code-slash" });

        public IActionResult UpdateSkillCategory(int id)
        {
            var value = _context.SkillCategories.Find(id);
            if (value == null) return NotFound();
            return View("Form", value);
        }

        [HttpPost]
        public IActionResult Save(SkillCategory category)
        {
            if (!ModelState.IsValid) return View("Form", category);

            if (category.SkillCategoryId == 0)
                _context.SkillCategories.Add(category);
            else
                _context.SkillCategories.Update(category);

            _context.SaveChanges();
            TempData["Success"] = "Category saved";
            return RedirectToAction("SkillCategoryList");
        }

        public IActionResult DeleteSkillCategory(int id)
        {
            var value = _context.SkillCategories.Find(id);
            if (value != null)
            {
                _context.SkillCategories.Remove(value);
                _context.SaveChanges();
                TempData["Success"] = "Category deleted";
            }
            return RedirectToAction("SkillCategoryList");
        }
    }
}