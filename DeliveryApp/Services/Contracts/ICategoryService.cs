using DeliveryApp.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.Services.Contracts
{
    public interface ICategoryService
    {
        Category AddCategory(Category newCategory);
        Category EditCategory(Category category);
        Category DeleteCategory(int categoryId);
        Category GetCategoryById(int categoryId);
        IEnumerable<Category> GetCategoriesByName(string name);
        IEnumerable<Category> GetAllCategories();
    }
}
