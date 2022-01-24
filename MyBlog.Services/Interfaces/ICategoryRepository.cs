using MyBlog.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyBlog.Services.Interfaces
{
   public interface ICategoryRepository
    {
        List<Category> GetCategories();
        Category GetCategory(int id);
        bool Add(Category entity);
        bool Edit(Category entity);
        bool Delete(int id);
    }
}
