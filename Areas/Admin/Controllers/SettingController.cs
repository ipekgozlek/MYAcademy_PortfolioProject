using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MYAcademy_PortfolioProject.Context;
using MYAcademy_PortfolioProject.Entities;
using MYAcademy_PortfolioProject.Helpers;

namespace MYAcademy_PortfolioProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SettingController : Controller
    {
        private readonly PortfolioContext _context;
        private readonly IWebHostEnvironment _env;

        public SettingController(PortfolioContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
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
            // CV yolu bu formda yok, veritabanındaki değeri koru
            contact.CvFilePath = _context.Contacts.AsNoTracking()
                                         .Where(x => x.ContactId == contact.ContactId)
                                         .Select(x => x.CvFilePath)
                                         .FirstOrDefault();

            if (contact.ContactId == 0)
                _context.Contacts.Add(contact);
            else
                _context.Contacts.Update(contact);

            _context.SaveChanges();
            TempData["Success"] = "Settings saved";
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult UploadCv(IFormFile? cvFile)
        {
            var setting = _context.Contacts.FirstOrDefault();
            if (setting == null)
            {
                TempData["Error"] = "Please save the settings form first.";
                return RedirectToAction("Index");
            }

            if (cvFile == null)
            {
                TempData["Error"] = "Please select a PDF file.";
                return RedirectToAction("Index");
            }

            // 1) İçerik gerçekten PDF mi?
            if (!FileHelper.IsRealPdf(cvFile))
            {
                TempData["Error"] = "Only real PDF files are accepted.";
                return RedirectToAction("Index");
            }

            // 2) Uzantı + boyut kontrolü ve kaydetme
            var (path, error) = FileHelper.Save(cvFile, _env.WebRootPath, "cv", new[] { ".pdf" }, 5 * 1024 * 1024);
            if (error != null)
            {
                TempData["Error"] = error;
                return RedirectToAction("Index");
            }

            // 3) Eski CV'yi sil, yenisini kaydet
            var oldPath = setting.CvFilePath;
            setting.CvFilePath = path;
            _context.SaveChanges();
            FileHelper.Delete(oldPath, _env.WebRootPath);

            TempData["Success"] = "CV uploaded";
            return RedirectToAction("Index");
        }

        public IActionResult DeleteCv()
        {
            var setting = _context.Contacts.FirstOrDefault();
            if (setting?.CvFilePath != null)
            {
                FileHelper.Delete(setting.CvFilePath, _env.WebRootPath);
                setting.CvFilePath = null;
                _context.SaveChanges();
                TempData["Success"] = "CV removed";
            }
            return RedirectToAction("Index");
        }
    }
}