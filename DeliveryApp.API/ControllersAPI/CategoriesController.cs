using AutoMapper;
using DeliveryApp.API.Models.DTO;
using DeliveryApp.Models.Data;
using DeliveryApp.Services.Contracts;
using Microsoft.AspNetCore.Cors;
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
        private readonly IMapper _mapper;

        public CategoriesController(ICategoryService categoryService, IMapper mapper)
        {
            this.categoryService = categoryService;
            _mapper = mapper;
        }

        [EnableCors("AllowAll")]
        [HttpGet]
        public ActionResult<IEnumerable<CategoryDtoWithBase64>> GetCategories(string searchQuery)
        {
            var allCategories = categoryService.GetCategoriesByName(searchQuery);

            return Ok(_mapper.Map<IEnumerable<CategoryDtoWithBase64>>(allCategories));
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
