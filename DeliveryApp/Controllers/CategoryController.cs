using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeliveryApp.Extensions;
using DeliveryApp.Models.Data;
using DeliveryApp.Models.ViewModels;
using DeliveryApp.Services.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryApp.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }


        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddCategory([Bind("Name,Description")] Category category, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                string path = FileUploader.UploadImage(file);
                category.ImagePath = path;
                categoryService.AddCategory(category);
                TempData["Message"] = "Catégorie ajoutée avec succès !";
                return RedirectToAction("AllCategories");
            }
            return View();
        }

        [HttpGet]
        public IActionResult AllCategories()
        {
            var allCategories = categoryService.GetAllCategories();
            var catViewModel = new CategoryViewModel { Categories = allCategories };
            return View(catViewModel);
        }
    }
}