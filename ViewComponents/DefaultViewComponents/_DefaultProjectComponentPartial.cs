using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using MYAcademy_PortfolioProject.Context;

namespace MYAcademy_PortfolioProject.ViewComponents.DefaultViewComponents
{
    public class _DefaultProjectComponentPartial:ViewComponent
    {
        private readonly PortfolioContext _context;
        public _DefaultProjectComponentPartial(PortfolioContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            var values = _context.Projects
                       .Include(x => x.Category)
                       .Where(x => x.IsActive)
                       .OrderByDescending(x => x.ProjectId)
                       .ToList();
            return View(values);
        }
    }
}
