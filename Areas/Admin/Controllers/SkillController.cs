using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MYAcademy_PortfolioProject.Context;
using MYAcademy_PortfolioProject.Entities;

namespace MYAcademy_PortfolioProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SkillController : Controller
    {
        private readonly PortfolioContext _context;

        public SkillController(PortfolioContext context)
        {
            _context = context;
        }

        public IActionResult Index() => RedirectToAction("SkillList");

        public IActionResult SkillList()
        {
            var values = _context.Skills
                                 .Include(x => x.SkillCategory)
                                 .OrderBy(x => x.SkillCategory.Title)
                                 .ThenBy(x => x.Title)
                                 .ToList();
            return View(values);
        }

        public IActionResult CreateSkill()
        {
            LoadCategories();
            return View("Form", new Skill { Level = "Orta" });
        }

        public IActionResult UpdateSkill(int id)
        {
            var value = _context.Skills.Find(id);
            if (value == null) return NotFound();
            LoadCategories();
            return View("Form", value);
        }

        [HttpPost]
        public IActionResult Save(Skill skill)
        {
            if (!ModelState.IsValid)
            {
                LoadCategories();
                return View("Form", skill);
            }

            if (skill.SkillId == 0)
                _context.Skills.Add(skill);
            else
                _context.Skills.Update(skill);

            _context.SaveChanges();
            TempData["Success"] = "Skill saved";
            return RedirectToAction("SkillList");
        }

        public IActionResult DeleteSkill(int id)
        {
            var value = _context.Skills.Find(id);
            if (value != null)
            {
                _context.Skills.Remove(value);
                _context.SaveChanges();
                TempData["Success"] = "Skill deleted";
            }
            return RedirectToAction("SkillList");
        }

        // Formdaki kategori açılır listesi için
        private void LoadCategories()
        {
            ViewBag.Categories = new SelectList(_context.SkillCategories.ToList(), "SkillCategoryId", "Title");
        }
    }
}