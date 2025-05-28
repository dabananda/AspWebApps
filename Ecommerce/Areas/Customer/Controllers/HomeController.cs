using System.Diagnostics;
using System.Security.Claims;
using AspWebApps.DataAccess.Repository.IRepository;
using AspWebApps.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> products = _unitOfWork.Product.GetAll(includeProperties: "Category");
            return View(products);
        }

        public IActionResult Details(int id)
        {
            Product product = _unitOfWork.Product.Get(u => u.Id == id, includeProperties: "Category");

            if (product == null)
            {
                return NotFound();
            }

            ShoppingCart cart = new()
            {
                Product = product,
                Count = 1,
                ProductId = id
            };

            return View(cart);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Details(ShoppingCart cart)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            cart.ApplicationUserId = userId;

            ShoppingCart existingCart = _unitOfWork.ShoppingCart.Get(
                u => u.ApplicationUserId == userId && u.ProductId == cart.ProductId);
            if (existingCart != null)
            {
                existingCart.Count += cart.Count;
                _unitOfWork.ShoppingCart.Update(existingCart);
            }
            else
            {
                ShoppingCart newCart = new()
                {
                    ProductId = cart.ProductId,
                    Count = cart.Count,
                    ApplicationUserId = userId
                };
                _unitOfWork.ShoppingCart.Add(newCart);
            }

            _unitOfWork.Save();
            TempData["success"] = "Item added to cart successfully!";
            return RedirectToAction(nameof(Index));
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
    }
}
