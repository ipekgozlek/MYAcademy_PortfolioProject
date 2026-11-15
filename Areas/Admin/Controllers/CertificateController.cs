using Microsoft.AspNetCore.Mvc;
using MYAcademy_PortfolioProject.Context;
using MYAcademy_PortfolioProject.Entities;
using MYAcademy_PortfolioProject.Helpers;

namespace MYAcademy_PortfolioProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CertificateController : Controller
    {
        private readonly PortfolioContext _context;

        public CertificateController(PortfolioContext context)
        {
            _context = context;
        }

        public IActionResult Index() => RedirectToAction("CertificateList");

        // Newest first (year read from the date text, e.g. "Sep 2026")
        public IActionResult CertificateList()
        {
            var values = _context.Certificates
                                 .AsEnumerable()
                                 .OrderByDescending(x => DurationHelper.StartYear(x.DateText ?? ""))
                                 .ThenByDescending(x => x.CertificateId)
                                 .ToList();
            return View(values);
        }

        // Create and Update share the same form (Form.cshtml)
        public IActionResult CreateCertificate() => View("Form", new Certificate { IsActive = true });

        public IActionResult UpdateCertificate(int id)
        {
            var value = _context.Certificates.Find(id);
            if (value == null) return NotFound();
            return View("Form", value);
        }

        [HttpPost]
        public IActionResult Save(Certificate certificate)
        {
            if (!ModelState.IsValid) return View("Form", certificate);

            if (certificate.CertificateId == 0)
                _context.Certificates.Add(certificate);
            else
                _context.Certificates.Update(certificate);

            _context.SaveChanges();
            TempData["Success"] = "Certificate saved";
            return RedirectToAction("CertificateList");
        }

        public IActionResult DeleteCertificate(int id)
        {
            var value = _context.Certificates.Find(id);
            if (value != null)
            {
                _context.Certificates.Remove(value);
                _context.SaveChanges();
                TempData["Success"] = "Certificate deleted";
            }
            return RedirectToAction("CertificateList");
        }

        public IActionResult ToggleStatus(int id)
        {
            var value = _context.Certificates.Find(id);
            if (value != null)
            {
                value.IsActive = !value.IsActive;
                _context.SaveChanges();
            }
            return RedirectToAction("CertificateList");
        }
    }
}
