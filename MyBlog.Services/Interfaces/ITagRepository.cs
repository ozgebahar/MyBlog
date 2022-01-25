using MyBlog.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyBlog.Services.Interfaces
{
   public interface ITagRepository
    {
        List<Tag> GetTags();
        Tag GetTag(int id);
        bool Add(Tag entity);
        bool Edit(Tag entity);
        bool Delete(int id);
    }
}
