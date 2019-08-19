using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using prodCat.Models;

namespace prodCat.Controllers {
    public class HomeController : Controller {
        private productCatContext dbContext;

        public HomeController(productCatContext context) {
            dbContext = context;
        }

        public IActionResult Index() {
            return View();
        }

        [HttpGet("products")]
        public IActionResult Products() {
            List<Product> AllProducts = dbContext.products.ToList();
            ViewBag.AllProducts = AllProducts;
            return View();
        }

        [HttpPost("AddProduct")]
        public IActionResult AddProduct(Product newProduct){
            if (ModelState.IsValid) {
                dbContext.Add(newProduct);
                dbContext.SaveChanges();
                return RedirectToAction("Products");
            }
            List<Product> AllProducts = dbContext.products.ToList();
            ViewBag.AllProducts = AllProducts;
            return View("Products");
        }

        [HttpGet("products/{productID}")]
        public IActionResult ProductInfo(int productID) {
            Product thisProduct = dbContext.products.Include(p => p.Groups).ThenInclude(ass => ass.Group).SingleOrDefault(p => p.ProductId == productID);
            List<Category> associated = new List<Category>();
            foreach (var group in thisProduct.Groups){
                associated.Add(group.Group);
            }
            List<Category> notAssociated = dbContext.categories.Except(associated).ToList();
            ViewBag.Associated = associated;
            ViewBag.NoAssociation = notAssociated;
            ViewBag.thisProduct = thisProduct;
            return View();
        }

        [HttpPost("products/{productID}/addCat")]
        public IActionResult AddCat(int productID, ProductViewModel modelData){
            Association newAssocation = modelData.NewAssociation;
            System.Console.WriteLine("*****************************");
            System.Console.WriteLine(ModelState.IsValid);
            System.Console.WriteLine("*****************************");
            System.Console.WriteLine(newAssocation.GroupID);
            System.Console.WriteLine("*****************************");
            if (ModelState.IsValid) {
                // newAssocation.ItemID = productID;
                dbContext.Add(newAssocation);
                dbContext.SaveChanges();
                return RedirectToAction("ProductInfo", new{ productID = productID});
            }
            System.Console.WriteLine("*****************************");
            Product thisProduct = dbContext.products.Include(p => p.Groups).ThenInclude(ass => ass.Group).SingleOrDefault(p => p.ProductId == productID);
            List<Category> associated = new List<Category>();
            foreach (var group in thisProduct.Groups){
                associated.Add(group.Group);
            }
            List<Category> notAssociated = dbContext.categories.Except(associated).ToList();
            ViewBag.Associated = associated;
            ViewBag.NoAssociation = notAssociated;
            ViewBag.thisProduct = thisProduct;
            // ProductViewModel myViewModel = new ProductViewModel();
            // myViewModel.product = dbContext.products.Include(p => p.Groups).ThenInclude(ass => ass.Group).SingleOrDefault(p => p.ProductId == productID);
            return View("ProductInfo", new{ productID = productID});
        }

        [HttpGet("categories")]
        public IActionResult Categories() {
            List<Category> AllCategories = dbContext.categories.OrderBy(c => c.Name).ToList();
            ViewBag.AllCategories = AllCategories;
            return View();
        }

        [HttpPost("AddCategory")]
        public IActionResult AddCategory(Category newCategory) {
            if (ModelState.IsValid) {
                dbContext.Add(newCategory);
                dbContext.SaveChanges();
                return RedirectToAction("Categories");
            }
            List<Category> AllCategories = dbContext.categories.OrderBy(c => c.Name).ToList();
            ViewBag.AllCategories = AllCategories;
            return View("Categories");
        }

        [HttpGet("categories/{categoryID}")]
        public IActionResult CategoryInfo(int categoryID) {
            Category thisCategory = dbContext.categories.Include(c => c.Items).ThenInclude(ass => ass.Item).SingleOrDefault(c => c.CategoryId == categoryID);
            List<Product> associatedProducts = new List<Product>();
            foreach (Association asso in thisCategory.Items) {
                associatedProducts.Add(asso.Item);
            }
            List<Product> notAssociated = dbContext.products.Except(associatedProducts).ToList();
            ViewBag.Associated = associatedProducts;
            ViewBag.NoAssociation = notAssociated;
            ViewBag.thisCategory = thisCategory;
            return View();
        }

        [HttpPost("categories/{categoryID}/addProd")]
        public IActionResult AddProd(int categoryID, ProductViewModel modelData) {
            Association newAssociation = modelData.NewAssociation;
            if (ModelState.IsValid) {
                dbContext.Add(newAssociation);
                dbContext.SaveChanges();
                return RedirectToAction("CategoryInfo", new { categoryID = categoryID });
            }
            Category thisCategory = dbContext.categories.Include(c => c.Items).ThenInclude(ass => ass.Item).SingleOrDefault(c => c.CategoryId == categoryID);
            List<Product> associated = new List<Product>();
            foreach (var item in thisCategory.Items) {
                associated.Add(item.Item);
            }
            List<Product> notAssociated = dbContext.products.Except(associated).ToList();
            ViewBag.Associated = associated;
            ViewBag.NoAssociation = notAssociated;
            ViewBag.thisCategory = thisCategory;
            return View("CategoryInfo", new{ categoryID = categoryID });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
