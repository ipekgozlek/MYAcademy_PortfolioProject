using Microsoft.AspNetCore.Mvc;

namespace MYAcademy_PortfolioProject.Controllers
{
    public class DefaultController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
