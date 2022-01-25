using MyBlog.Data;
using MyBlog.Data.Entities;
using MyBlog.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyBlog.Services.Concrete
{
    public class TagRepository : ITagRepository
    {
        private readonly MyBlogDbContext _db;

        public TagRepository(MyBlogDbContext myBlogDbContext)
        {
            _db = myBlogDbContext;
        }

        public bool Add(Tag entity)
        {
            if (_db.Tags.Any(x=> x.Name == entity.Name))
            {
                return false;
            }
            entity.CreatedDate = DateTime.Now;
            _db.Tags.Add(entity);

            return _db.SaveChanges() > 0;
        }

        public bool Delete(int id)
        {
            var entity = GetTag(id);
            if (entity !=null)
            {
                entity.IsActive = false;
                entity.UpdatedDate = DateTime.Now;

                return _db.SaveChanges() > 0;
            }

            return false;
        }

        public bool Edit(Tag entity)
        {
            var uptEntity = GetTag(entity.Id);
            if (uptEntity == null)
            {
                return false;
            }

            uptEntity.Name = entity.Name;
            uptEntity.UpdatedDate = DateTime.Now;

            return _db.SaveChanges() > 0;
        }

        public Tag GetTag(int id)
        {
            return _db.Tags.FirstOrDefault(x=> x.Id == id && x.IsActive);
        }

        public List<Tag> GetTags()
        {
            return _db.Tags.Where(x => x.IsActive).ToList();
        }
    }
}
