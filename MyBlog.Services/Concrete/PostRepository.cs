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

        public bool Add(Post entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public bool Edit(Post entity)
        {
            throw new NotImplementedException();
        }

        public Post GetPost(int id)
        {
            return _db.Posts.FirstOrDefault(x => x.Id == id && x.IsActive);
        }

        public List<Post> GetPosts()
        {
            return _db.Posts.Where(x => x.IsActive).ToList();
        }
    }
}
