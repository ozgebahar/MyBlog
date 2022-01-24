using Microsoft.EntityFrameworkCore;
using MyBlog.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyBlog.Data
{
  public  class MyBlogDbContext : DbContext
    {
        public MyBlogDbContext(DbContextOptions<MyBlogDbContext> options) : base(options)
        { 
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PostTag>().HasKey(x => new { x.PostId, x.TagId });
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<PostTag> PostTags { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}
