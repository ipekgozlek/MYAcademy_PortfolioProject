using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MYAcademy_PortfolioProject.Context;
using MYAcademy_PortfolioProject.Entities;

namespace MYAcademy_PortfolioProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProjectController : Controller
    {
        private readonly PortfolioContext _context;

        public ProjectController(PortfolioContext context)
        {
            _context = context;
        }

        public IActionResult Index() => RedirectToAction("ProjectList");

        public IActionResult ProjectList()
        {
            var values = _context.Projects
                                 .Include(x => x.Category)
                                 .OrderByDescending(x => x.ProjectId)
                                 .ToList();
            return View(values);
        }

        public IActionResult CreateProject()
        {
            LoadCategories();
            return View("Form", new Project());
        }

        public IActionResult UpdateProject(int id)
        {
            var value = _context.Projects.Find(id);
            if (value == null) return NotFound();
            LoadCategories();
            return View("Form", value);
        }

        [HttpPost]
        public IActionResult Save(Project project)
        {
            if (!ModelState.IsValid)
            {
                LoadCategories();
                return View("Form", project);
            }

            if (project.ProjectId == 0)
                _context.Projects.Add(project);
            else
                _context.Projects.Update(project);

            _context.SaveChanges();
            TempData["Success"] = "Project saved";
            return RedirectToAction("ProjectList");
        }

        public IActionResult DeleteProject(int id)
        {
            var value = _context.Projects.Find(id);
            if (value != null)
            {
                _context.Projects.Remove(value);
                _context.SaveChanges();
                TempData["Success"] = "Project deleted";
            }
            return RedirectToAction("ProjectList");
        }

        private void LoadCategories()
        {
            ViewBag.Categories = new SelectList(_context.Categories.ToList(), "CategoryId", "CategoryName");
        }
    }
}