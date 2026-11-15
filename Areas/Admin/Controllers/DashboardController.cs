using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MYAcademy_PortfolioProject.Context;
using MYAcademy_PortfolioProject.Models;

namespace MYAcademy_PortfolioProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DashboardController : Controller
    {
        private readonly PortfolioContext _context;

        public DashboardController(PortfolioContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var model = new DashboardViewModel
            {
                // Count / Where
                TotalProjects = _context.Projects.Count(),
                ActiveProjects = _context.Projects.Count(x => x.IsActive),
                SkillCount = _context.Skills.Count(),
                ExperienceCount = _context.Experiences.Count(),
                EducationCount = _context.Educations.Count(),
                TotalMessages = _context.Messages.Count(),
                UnreadMessages = _context.Messages.Count(x => !x.IsRead),

                // OrderByDescending + Take
                LastMessages = _context.Messages
                                       .OrderByDescending(x => x.SendDate)
                                       .Take(5)
                                       .ToList(),

                // Include + OrderByDescending + Take
                LastProjects = _context.Projects
                                       .Include(x => x.Category)
                                       .OrderByDescending(x => x.ProjectId)
                                       .Take(3)
                                       .ToList(),

                // GroupBy
                CategorySummaries = _context.Projects
                                            .GroupBy(x => x.Category!.CategoryName)
                                            .Select(g => new CategorySummary
                                            {
                                                CategoryName = g.Key,
                                                ProjectCount = g.Count()
                                            })
                                            .OrderByDescending(x => x.ProjectCount)
                                            .ToList()
            };

            return View(model);
        }
    }
}