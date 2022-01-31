using Microsoft.AspNetCore.Http;
using MyBlog.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyBlog.Admin.Models
{
    public class PostViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [Required]
        [StringLength(5000)]
        public string Content { get; set; }

        [Required]
        public IFormFile Picture { get; set; }

        public string PictureStr { get; set; }

        [DisplayName("Yayında mı?")]
        public bool IsPublished { get; set; }

        #region Releations
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        public List<Comment> Comments { get; set; }
        public List<PostTag> PostTags { get; set; }
        public string Tags { get; set; }
        #endregion
    }
}
