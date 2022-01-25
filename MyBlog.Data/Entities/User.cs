using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyBlog.Data.Entities
{
  public class User : BaseEntity
    {

        [Required]
        [StringLength(30)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string Surname { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        [StringLength(12)]
        public string Password { get; set; }

        [Required]
        [StringLength(10)]
        public string Role { get; set; }
    }
}
