using MyBlog.Data;
using MyBlog.Data.Entities;
using MyBlog.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyBlog.Services.Concrete
{
    public class CommentRepository : ICommentRepository
    {
        private readonly MyBlogDbContext _db;
        public CommentRepository(MyBlogDbContext myBlogDbContext)
        {
            _db = myBlogDbContext;
        }
        public bool Add(Comment entity)
        {
            entity.IsActive = true;
            entity.CreatedDate = DateTime.Now;

            _db.Comments.Add(entity);

            return _db.SaveChanges() > 0;
        }

        public bool Delete(int id)
        {
            var deletedEntity = GetComment(id);
            if (deletedEntity != null)
            {
                deletedEntity.IsActive = false;
                deletedEntity.UpdatedDate = DateTime.Now;

                return _db.SaveChanges() > 0;
            }
            return false;
        }

        public bool Edit(Comment entity)
        {
            var uptEntity = GetComment(entity.Id);
            if (uptEntity == null)
            {
                return false;
            }

            uptEntity.Nickname = entity.Nickname;
            uptEntity.PostId = entity.PostId;
            uptEntity.Content = entity.Content;
            uptEntity.UpdatedDate = DateTime.Now;

            return _db.SaveChanges() > 0;
        }

        public Comment GetComment(int id)
        {
            return _db.Comments.FirstOrDefault(x => x.Id == id && x.IsActive);
        }

        public List<Comment> GetComments()
        {
            return _db.Comments.Where(x => x.IsActive).ToList();
        }
    }
}
