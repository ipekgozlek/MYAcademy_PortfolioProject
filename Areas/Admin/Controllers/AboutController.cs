using Microsoft.AspNetCore.Mvc;
using MYAcademy_PortfolioProject.Context;
using MYAcademy_PortfolioProject.Entities;

namespace MYAcademy_PortfolioProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AboutController : Controller
    {
        private readonly PortfolioContext _context;

        public AboutController(PortfolioContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
           
            var value = _context.Abouts.FirstOrDefault() ?? new About();
            return View(value);
        }

        [HttpPost]
        public IActionResult Index(About about)
        {
            if (about.AboutId == 0)
                _context.Abouts.Add(about);    
            else
                _context.Abouts.Update(about);   

            _context.SaveChanges();
            TempData["Success"] = "About section updated";
            return RedirectToAction("Index");
        }
    }
}