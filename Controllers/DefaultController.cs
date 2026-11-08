using Microsoft.AspNetCore.Mvc;
using MYAcademy_PortfolioProject.Context;
using MYAcademy_PortfolioProject.Entities;

namespace MYAcademy_PortfolioProject.Controllers
{
    public class DefaultController : Controller
    {
        private readonly PortfolioContext _context;

        public DefaultController(PortfolioContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SendMessage(Message message)
        {
            // Sunucu tarafı doğrulama (Message.cs'teki kurallar)
            if (!ModelState.IsValid)
            {
                var errors = ModelState
                    .Where(x => x.Value.Errors.Any())
                    .ToDictionary(x => x.Key, x => x.Value.Errors.First().ErrorMessage);

                return Json(new { success = false, errors });
            }

            // Bu alanları kullanıcı belirleyemez, sunucu atar
            message.MessageId = 0;
            message.SendDate = DateTime.Now;
            message.IsRead = false;

            _context.Messages.Add(message);
            _context.SaveChanges();

            return Json(new { success = true });
        }
    }
}