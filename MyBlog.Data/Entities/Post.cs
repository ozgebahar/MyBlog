using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyBlog.Data.Entities
{
    public class Post : BaseEntity
    {
        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        [Required]
        [StringLength(5000)]
        public string Content { get; set; }

        public byte[] Picture { get; set; }


        #region Relations

        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public List<Comment> comments { get; set; }
        public List<PostTag> PostTags { get; set; }

        #endregion
    }
}
