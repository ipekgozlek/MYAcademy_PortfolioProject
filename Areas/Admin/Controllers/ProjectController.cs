using MYAcademy_PortfolioProject.Helpers;
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
        private readonly IWebHostEnvironment _env;

        public ProjectController(PortfolioContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
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
            return View("Form", new Project { IsActive = true });
        }

        public IActionResult UpdateProject(int id)
        {
            var value = _context.Projects.Find(id);
            if (value == null) return NotFound();
            LoadCategories();
            return View("Form", value);
        }
        [HttpPost]
        public IActionResult Save(Project project, IFormFile? imageFile)
        {
            // Eski görsel yolunu formdan değil VERİTABANINDAN al
            var oldImage = project.ProjectId == 0 ? null
                : _context.Projects.AsNoTracking()
                                   .Where(x => x.ProjectId == project.ProjectId)
                                   .Select(x => x.CoverImageUrl)
                                   .FirstOrDefault();
            project.CoverImageUrl = oldImage;

            // Önce diğer alanları kontrol et (hata varsa dosyayı boşuna kaydetme)
            if (!ModelState.IsValid)
            {
                LoadCategories();
                return View("Form", project);
            }

            // Yeni dosya seçildiyse yükle
            if (imageFile != null)
            {
                var (path, error) = FileHelper.Save(
                    imageFile, _env.WebRootPath, "projects",
                    new[] { ".jpg", ".jpeg", ".png", ".webp" },
                    2 * 1024 * 1024);   // 2 MB

                if (error != null)
                {
                    ModelState.AddModelError("imageFile", error);
                    LoadCategories();
                    return View("Form", project);
                }
                project.CoverImageUrl = path;
            }

            if (project.ProjectId == 0)
                _context.Projects.Add(project);
            else
                _context.Projects.Update(project);

            _context.SaveChanges();

            // Görsel değiştiyse eski dosyayı sil
            if (imageFile != null && oldImage != project.CoverImageUrl)
                FileHelper.Delete(oldImage, _env.WebRootPath);

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
                FileHelper.Delete(value.CoverImageUrl, _env.WebRootPath);
                TempData["Success"] = "Project deleted";
            }
            return RedirectToAction("ProjectList");
        }
        public IActionResult ToggleStatus(int id)
        {
            var value = _context.Projects.Find(id);
            if (value != null)
            {
                value.IsActive = !value.IsActive;   // true ise false, false ise true yap
                _context.SaveChanges();
            }
            return RedirectToAction("ProjectList");
        }
        private void LoadCategories()
        {
            ViewBag.Categories = new SelectList(_context.Categories.ToList(), "CategoryId", "CategoryName");
        }
    }
}