using MyBlog.Data;
using MyBlog.Data.Entities;
using MyBlog.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyBlog.Services.Concrete
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly MyBlogDbContext _db;
        public CategoryRepository( MyBlogDbContext context)
        {
            _db = context;
        }
        public bool Add(Category entity)
        {
            entity.IsActive = true;
            entity.CreatedDate = DateTime.Now;
            _db.Categories.Add(entity);
            var result = _db.SaveChanges() > 0;

            return result;
        }

        public bool Delete(int id)
        {
            var entity = GetCategory(id);
            if (entity != null)
            {
                entity.IsActive = false;
                entity.UpdatedDate = DateTime.Now;

                return _db.SaveChanges()>0;
            }
            return false;
        }

        public bool Edit(Category entity)
        {
            var updCategory = GetCategory(entity.Id);
            if (updCategory != null)
            {
                updCategory.CategoryName = entity.CategoryName;
                updCategory.Description = entity.Description;
                updCategory.Picture = entity.Picture;
                updCategory.Posts = entity.Posts;
                updCategory.UpdatedDate = DateTime.Now;

                return _db.SaveChanges() > 0;
            }
            return false;
        }

        public List<Category> GetCategories()
        {
            var categories = _db.Categories.Where(x=>x.IsActive).ToList();

            return categories;
        }

        public Category GetCategory(int id)
        {
            var category = _db.Categories.FirstOrDefault(x=> x.Id == id && x.IsActive);
            return category;
        }
    }
}
