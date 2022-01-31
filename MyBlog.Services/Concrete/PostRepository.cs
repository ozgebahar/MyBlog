using Microsoft.EntityFrameworkCore;
using MyBlog.Data;
using MyBlog.Data.Entities;
using MyBlog.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyBlog.Services.Concrete
{
    public class PostRepository : IPostRepository
    {
        private readonly MyBlogDbContext _db;

        public PostRepository(MyBlogDbContext myBlogDbContext)
        {
            _db = myBlogDbContext;
        }

        public void CreateTags(Post entity)
        {
            var allTags = _db.Tags.ToList();

            var tagList = new List<PostTag>();

            foreach (var item in entity.PostTags)
            {
                var tag = allTags.FirstOrDefault(x => x.Name == item.Tag.Name);
                if (tag != null)
                {
                    tagList.Add(new PostTag() { TagId = tag.Id });
                }
                else
                {
                    tagList.Add(new PostTag() { Tag = item.Tag });
                }
            }

            entity.PostTags = tagList;

        }

        public bool Add(Post entity)
        {
            CreateTags(entity);

            _db.Add(entity);
            return _db.SaveChanges() > 0;
        }

        public bool Delete(int id)
        {
            var deletedEntity = GetPost(id);
            if (deletedEntity == null)
            {
                return false;
            }

            deletedEntity.IsActive = false;
            deletedEntity.UpdatedDate = DateTime.Now;

            return _db.SaveChanges() > 0;
        }

        public bool Edit(Post entity)
        {
            var uptEntity = GetPost(entity.Id);
            if (uptEntity == null)
            {
                return false;
            }

            uptEntity.CategoryId = entity.CategoryId;
            uptEntity.Title = entity.Title;
            uptEntity.Content = entity.Content;
            uptEntity.IsPublished = entity.IsPublished;
            uptEntity.Picture = entity.Picture;
            uptEntity.UpdatedDate = DateTime.Now;
            uptEntity.PostTags = entity.PostTags;

            CreateTags(uptEntity);

            return _db.SaveChanges() > 0;
        }

        public Post GetPost(int id)
        {
            return _db.Posts
                .Include(x => x.Category)
                .Include(x => x.comments)
                .Include(x => x.PostTags).ThenInclude(x => x.Tag)
                .FirstOrDefault(x => x.Id == id && x.IsActive);
        }

        public List<Post> GetPosts()
        {
            return _db.Posts.Include(x=> x.Category).Where(x => x.IsActive).ToList();
        }
    }
}
