using Microsoft.AspNetCore.Mvc;

namespace Harmic.Controllers
{
    public class BlogcommentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
