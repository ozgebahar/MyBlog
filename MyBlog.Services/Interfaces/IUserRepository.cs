﻿using MyBlog.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyBlog.Services.Interfaces
{
  public interface IUserRepository
    {
        List<User> GetUsers();
        User GetUser(int id);
        bool Add(User entity);
        bool Edit(User entity);
        bool Delete(int id);
        User Login(string email, string password);
    }
}
