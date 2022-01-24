using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyBlog.Data.Entities
{
   public class Category : BaseEntity
    {
        [Required]
        [StringLength(30)]
        public string CategoryName { get; set; }

        [Required]
        [StringLength(500)]
        public string Description { get; set; }

        public byte[] Picture { get; set; }

        #region Relations

        public List<Post> Posts { get; set; }

        #endregion
    }
}
