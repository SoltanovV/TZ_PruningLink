using Microsoft.AspNetCore.Mvc;

namespace PruningLink.Controllers
{
    public class IndexController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult RefactorUrl()
        {
            return View();
        }
    }
}
