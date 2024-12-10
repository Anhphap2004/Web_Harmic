using Microsoft.AspNetCore.Mvc;
using Harmic.Utilities;
namespace Harmic.Areas.Admin.Controllers
{      
    [Area("Admin")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            if(!Function.IsLogin())
            {
                return RedirectToAction("Index", "Login");
            }    
            return View();
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
            return Task.FromResult<IActionResult>(RedirectToAction("Index", "Login"));
        }
    }

}
