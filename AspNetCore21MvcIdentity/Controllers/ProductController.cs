using AspNetCore21MvcIdentity.Data;
using AspNetCore21MvcIdentity.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace AspNetCore21MvcIdentity.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;

        public ProductController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var products = _context.Products.ToList();

            var model = new ProductViewModel
            {
                Title = "All Products",
                Products = products
            };

            return View(model);
        }
    }
}
