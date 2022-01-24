using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyBlog.Data.Entities
{
   public class Comment : BaseEntity
    {
        [Required]
        [StringLength(20)]
        public string Nickname { get; set; }

        [Required]
        [StringLength(500)]
        public string Content { get; set; }

        #region Relations

        public int PostId { get; set; }
        public Post Post { get; set; }

        #endregion
    }
}
