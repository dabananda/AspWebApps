using AspWebApps.DataAccess.Data;
using AspWebApps.DataAccess.Repository.IRepository;
using AspWebApps.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Ecommerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            List<Product> objProductList = _unitOfWork.Product.GetAll().ToList();
            return View(objProductList);
        }

        public IActionResult Create()
        {
            IEnumerable<SelectListItem> CategoryList = _unitOfWork.Category.GetAll().Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });
            ViewBag.CategoryList = CategoryList;
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Product created successfully";
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Edit(int id)
        {
            if (id == 0 || id == null)
            {
                return NotFound();
            }
            Product objProduct = _unitOfWork.Product.Get(u => u.Id == id);
            if (objProduct == null)
            {
                return NotFound();
            }
            return View(objProduct);
        }

        [HttpPost]
        public IActionResult Edit(Product obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Product updated successfully";
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Delete(int id)
        {
            if (id == 0 || id == null)
            {
                return NotFound();
            }
            Product objProduct = _unitOfWork.Product.Get(u => u.Id == id);
            if (objProduct == null)
            {
                return NotFound();
            }
            return View(objProduct);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int id)
        {
            Product objProduct = _unitOfWork.Product.Get(u => u.Id == id);
            if (objProduct == null)
            {
                return NotFound();
            }
            _unitOfWork.Product.Remove(objProduct);
            _unitOfWork.Save();
            TempData["success"] = "Product deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
