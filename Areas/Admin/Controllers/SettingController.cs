using Microsoft.AspNetCore.Mvc;
using MYAcademy_PortfolioProject.Context;
using MYAcademy_PortfolioProject.Entities;

namespace MYAcademy_PortfolioProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SettingController : Controller
    {
        private readonly PortfolioContext _context;

        public SettingController(PortfolioContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var value = _context.Contacts.FirstOrDefault() ?? new Contact();
            return View(value);
        }

        [HttpPost]
        public IActionResult Index(Contact contact)
        {
            if (contact.ContactId == 0)
                _context.Contacts.Add(contact);
            else
                _context.Contacts.Update(contact);

            _context.SaveChanges();
            TempData["Success"] = "Settings saved";
            return RedirectToAction("Index");
        }
    }
}