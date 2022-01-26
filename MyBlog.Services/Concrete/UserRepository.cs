using MyBlog.Data;
using MyBlog.Data.Entities;
using MyBlog.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyBlog.Services.Concrete
{
    public class UserRepository : IUserRepository
    {
        private readonly MyBlogDbContext _db;

        public UserRepository(MyBlogDbContext myBlogDbContext)
        {
            _db = myBlogDbContext;
        }

        public bool Add(User entity)
        {
            if (_db.Users.Any(x => x.Email == entity.Email))
            {
                return false;
            }
            entity.CreatedDate = DateTime.Now;
            entity.Role = entity.Role.ToLower();
            _db.Users.Add(entity);

            return _db.SaveChanges() > 0;
        }

        public bool Delete(int id)
        {
            var entity = GetUser(id);
            if (entity != null)
            {
                entity.IsActive = false;
                entity.UpdatedDate = DateTime.Now;

                return _db.SaveChanges() > 0;
            }

            return false;
        }

        public bool Edit(User entity)
        {
            var uptEntity = GetUser(entity.Id);
            if (uptEntity == null)
            {
                return false;
            }
            uptEntity.Name = entity.Name;
            uptEntity.Surname = entity.Surname;
            uptEntity.Email = entity.Email;
            uptEntity.Password = entity.Password;
            uptEntity.UpdatedDate = DateTime.Now;
            uptEntity.Role = entity.Role.ToLower();

            return _db.SaveChanges() > 0;
        }

        public User GetUser(int id)
        {
            return _db.Users.FirstOrDefault(x => x.Id == id && x.IsActive);
        }

        public List<User> GetUsers()
        {
            return _db.Users.Where(x => x.IsActive).ToList();
        }

        public User Login(string email, string password)
        {
            return _db.Users.FirstOrDefault(x => x.Email == email && x.Password == password && x.IsActive);
        }
    }
}
