using Harmic.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Harmic.Controllers
{
    public class ProductController : Controller
    {
        private readonly HarmicContext _context;

        public ProductController(HarmicContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        [Route("/product/{alias}-{id}.html")]
        public async Task<IActionResult> Details(int? id)
        {
            if(id == null || _context.TbProducts == null)
            {
                return NotFound();
            }
            var product = await _context.TbProducts.Include(i=>i.CategoryProduct).FirstOrDefaultAsync(m=>m.ProductId==id);
            if (product == null)
            {
                return NotFound();
            }
            ViewBag.productid = id;
            ViewBag.ProductReview=_context.TbProductReviews.Where(i=>i.ProductId==id && i.IsActive).ToList();
            ViewBag.ProductRelated = _context.TbProducts.Where(i=>i.ProductId != id && i.CategoryProductId == product.CategoryProductId).Take(5).OrderByDescending(i=>i.ProductId).ToList();
            ViewBag.CategoryProduct = _context.TbProductCategories.Where(i=>i.CategoryProductId == product.CategoryProductId && i.IsActive).ToList();
            return View(product);
        }
        [HttpPost]
        public async Task<IActionResult> Create(int menuId, string name, string phone, string email, string message)
        {
            try
            {
                // Kiểm tra xem blogId có hợp lệ không
                var product = await _context.TbProducts.FirstOrDefaultAsync(b => b.ProductId == menuId);
                if (product == null)
                {
                    return Json(new { status = false, message = "Product không tồn tại." });
                }

                // Tạo đối tượng bình luận mới
                TbProductReview review = new TbProductReview
                {
                    ProductId = menuId,  // Gán BlogId
                    Name = name,
                    Phone = phone,
                    Email = email,
                    Detail = message,
                    CreatedDate = DateTime.Now,
                    IsActive = true,

                };

                // Thêm vào cơ sở dữ liệu
                _context.Add(review);
                await _context.SaveChangesAsync(); // Lưu thay đổi

                return Json(new { status = true });
            }
            catch
            {
                return Json(new { status = false });
            }
        }
    }
}
