using Microsoft.AspNetCore.Mvc;
using MYAcademy_PortfolioProject.Context;

namespace MYAcademy_PortfolioProject.ViewComponents.DefaultViewComponents
{
    public class _DefaultSocialLinksComponentPartial: ViewComponent
    {
        private readonly PortfolioContext _context;

        public _DefaultSocialLinksComponentPartial(PortfolioContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            return View(_context.Contacts.FirstOrDefault());
        }
    }
}
