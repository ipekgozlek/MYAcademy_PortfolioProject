using Microsoft.AspNetCore.Mvc;
using MYAcademy_PortfolioProject.Context;
using MYAcademy_PortfolioProject.Entities;

namespace MYAcademy_PortfolioProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MessageController : Controller
    {
        private readonly PortfolioContext _context;

        public MessageController(PortfolioContext context)
        {
            _context = context;
        }

        public IActionResult MessageList()
        {
            var values = _context.Messages.OrderByDescending(x => x.SendDate).ToList();
            return View(values);
        }
        [HttpPost]
        public IActionResult MarkAsRead(int id)
        {
            var value = _context.Messages.Find(id);
            if (value == null) return NotFound();

            if (!value.IsRead)
            {
                value.IsRead = true;
                _context.SaveChanges();
            }
            return Ok();
        }

        [HttpGet]
        public IActionResult CreateMessage(string email, string subject)
        {
            var model = new Message
            {
                Email = email,
                Subject = subject
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult CreateMessage(Message message)
        {
            if (!ModelState.IsValid)
                return View(message);
            message.SendDate = DateTime.Now;
            message.IsRead = false;
            _context.Messages.Add(message);
            _context.SaveChanges();
            return RedirectToAction("MessageList");
        }
        public IActionResult DeleteMessage(int id)
        {
            var value = _context.Messages.Find(id);
            if (value != null)
            {
                _context.Messages.Remove(value);
                _context.SaveChanges();
                TempData["Success"] = "Message deleted";
            }
            return RedirectToAction("MessageList");
        }
        
    }
}
