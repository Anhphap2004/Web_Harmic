using Harmic.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Harmic.Utilities;
namespace Harmic.Controllers
{
    public class HomeController : Controller
    {
        private readonly HarmicContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(HarmicContext context, ILogger<HomeController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Index()
        {
            ViewBag.productCategories = _context.TbProductCategories.ToList();
            ViewBag.productNew=_context.TbProducts.Where(m=>m.IsNew).ToList();
            return View("Index","Home");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public Task<IActionResult> Logout()
        {
            // Logic xử lý logout
            Function._AccountId = 0;
            Function._Message = string.Empty;
            Function._MessageEmail = string.Empty;
            Function._Email = string.Empty;
            Function._Username = string.Empty;
            Function._FullName = string.Empty;
            Function._Phone = string.Empty;
            return Task.FromResult<IActionResult>(RedirectToAction("Index", "Home"));
        }

    }
}
