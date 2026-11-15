using Microsoft.AspNetCore.Mvc;
using MYAcademy_PortfolioProject.Context;
using MYAcademy_PortfolioProject.Helpers;

namespace MYAcademy_PortfolioProject.ViewComponents.DefaultViewComponents
{
    public class _DefaultCertificateComponentPartial : ViewComponent
    {
        private readonly PortfolioContext _context;

        public _DefaultCertificateComponentPartial(PortfolioContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var values = _context.Certificates
                                 .Where(x => x.IsActive)           // passive ones are hidden
                                 .AsEnumerable()
                                 .OrderByDescending(x => DurationHelper.StartYear(x.DateText ?? ""))
                                 .ThenByDescending(x => x.CertificateId)
                                 .ToList();
            return View(values);
        }
    }
}
