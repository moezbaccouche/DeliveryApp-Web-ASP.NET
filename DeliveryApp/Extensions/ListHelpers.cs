using DeliveryApp.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.Extensions
{
    public static class ListHelpers
    {
        public static bool CategoryExists(this List<Category> categories, Category category)
        {
            for(int i = 0; i < categories.Count; i++)
            {
                if(categories[i].Id == category.Id)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
