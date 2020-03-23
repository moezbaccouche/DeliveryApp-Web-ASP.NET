using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DeliveryApp.Data;
using DeliveryApp.Models.Data;
using DeliveryApp.Services.Contracts;
using DeliveryApp.Models.ViewModels;
using DeliveryApp.Models.Enums;
using Microsoft.AspNetCore.Http;
using DeliveryApp.Extensions;

namespace DeliveryApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ICategoryService categoryService;
        private readonly IHtmlHelper htmlHelper;
        private readonly IProductService productService;
        private readonly IProductImageService productImageService;

        public ProductController(ApplicationDbContext context, ICategoryService categoryService, IHtmlHelper htmlHelper,
            IProductService productService, IProductImageService productImageService)
        {
            _context = context;
            this.categoryService = categoryService;
            this.htmlHelper = htmlHelper;
            this.productService = productService;
            this.productImageService = productImageService;
        }

        [HttpGet]
        public IActionResult AddProduct()
        {
            var categories = categoryService.GetAllCategories();
            var units = htmlHelper.GetEnumSelectList<EnumProductUnit>();
            ProductViewModel pvm = new ProductViewModel { Categories = categories, Units = units };
            return View(pvm);
        }

        [HttpPost]
        public IActionResult AddProduct(ProductViewModel newProductVm, List<IFormFile> files, int categories)
        {
            var newProduct = new Product
            {
                Name = newProductVm.Product.Name,
                Description = newProductVm.Product.Description,
                ProductUnit = newProductVm.Product.ProductUnit,
                Price = double.Parse(newProductVm.Product.Price.ToString())
            };

            // Insert the new product in the database
            var category = categoryService.GetCategoryById(categories);
            newProduct.CategoryId = category.Id;

            var product = productService.AddProduct(newProduct);

            // Upload the chosen product pictures
            FileUploader fileUploader = new FileUploader(productImageService);
            fileUploader.UploadImages(files, product, "ProductsImages");



            return RedirectToAction("AllProducts");
        }

        public IActionResult AllProducts()
        {
            var allCategories = categoryService.GetAllCategories();
            var allProducts = productService.GetAllProducts();

            ProductViewModel pvm = new ProductViewModel { AllProducts = allProducts, Categories = allCategories };

            return View(pvm);
        }

        [HttpGet]
        public IActionResult EditProduct(int id)
        {
            var allCategories = categoryService.GetAllCategories();
            var product = productService.GetProductById(id);
            var productCategory = categoryService.GetCategoryById(product.CategoryId);
            var units = htmlHelper.GetEnumSelectList<EnumProductUnit>();
            var productImages = productImageService.GetProductImages(product);

            var model = new ProductViewModel
            { 
                Categories = allCategories,
                Product = product,
                ProductCategory = productCategory,
                Units = units,
                ProductImages = productImages

            };

            return PartialView(model);

        }
    }
}
