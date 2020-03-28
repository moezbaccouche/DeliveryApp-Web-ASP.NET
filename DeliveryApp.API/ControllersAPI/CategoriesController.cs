using DeliveryApp.Models.Data;
using DeliveryApp.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.API.ControllersAPI
{
    [ApiController]
    [Route("delivery-app/categories")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Category>> GetCategories(string searchQuery)
        {
            var allCategories = categoryService.GetCategoriesByName(searchQuery);
            return Ok(allCategories);
        }


        [HttpGet("{categoryId}")]
        public ActionResult<Category> GetCategory(int categoryId)
        {
            var category = categoryService.GetCategoryById(categoryId);
            if(category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

    }
}
