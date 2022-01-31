using MyBlog.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyBlog.Services.Interfaces
{
    public interface ICommentRepository
    {
        List<Comment> GetComments();
        Comment GetComment(int id);
        bool Add(Comment entity);
        bool Edit(Comment entity);
        bool Delete(int id);
    }
}
