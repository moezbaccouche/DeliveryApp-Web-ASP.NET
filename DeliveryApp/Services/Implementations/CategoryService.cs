using DeliveryApp.Data;
using DeliveryApp.Models.Data;
using DeliveryApp.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using PFEGestionConges.Data.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.Services.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepository<Category> repoCategory;
        private readonly ApplicationDbContext context;

        public CategoryService(IRepository<Category> repoCategory, ApplicationDbContext context)
        {
            this.repoCategory = repoCategory;
            this.context = context;
        }

        public Category AddCategory(Category newCategory)
        {
            repoCategory.Insert(newCategory);
            return newCategory;
        }

        public Category DeleteCategory(int categoryId)
        {
            var category = GetCategoryById(categoryId);
            repoCategory.Delete(category);
            return category;
        }

        public Category EditCategory(Category category)
        {
            repoCategory.Update(category);
            return category;
        }

        public IEnumerable<Category> GetAllCategories()
        {
            var query =  from c in repoCategory.TableNoTracking
                         orderby c.Name
                         select c;
            return query;
        }

        public IEnumerable<Category> GetCategoriesByName(string name)
        {
            var query = from c in repoCategory.TableNoTracking
                        where c.Name.StartsWith(name) || string.IsNullOrEmpty(name)
                        orderby c.Name
                        select c;
            return query;
        }

        public Category GetCategoryById(int categoryId)
        {
            var category = repoCategory.TableNoTracking.Where(c => c.Id == categoryId).FirstOrDefault();
            return category;
        }
    }
}
