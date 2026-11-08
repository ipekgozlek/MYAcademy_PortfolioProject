using Microsoft.AspNetCore.Mvc;
using MYAcademy_PortfolioProject.Context;
using MYAcademy_PortfolioProject.Entities;

namespace MYAcademy_PortfolioProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HeroSectionController : Controller
    {
        private readonly PortfolioContext _context;

        public HeroSectionController(PortfolioContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var value = _context.HeroSections.FirstOrDefault() ?? new HeroSection();
            return View(value);
        }

        [HttpPost]
        public IActionResult Index(HeroSection hero)
        {
            if (hero.HeroSectionId == 0)
                _context.HeroSections.Add(hero);
            else
                _context.HeroSections.Update(hero);

            _context.SaveChanges();
            TempData["Success"] = "Hero section updated";
            return RedirectToAction("Index");
        }
    }
}