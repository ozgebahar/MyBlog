using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyBlog.Admin.Models
{
    public class CategoryViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Zorunlu Alan")]
        [StringLength(30,ErrorMessage ="Maksimum 30 karakter yazabilirsiniz")]
        public string CategoryName { get; set; }

        [Required(ErrorMessage ="Zorunlu Alan")]
        [StringLength(500, ErrorMessage = "Maksimum 500 karakter yazabilirsiniz")]
        public string Description { get; set; }

        [Required]
        public IFormFile Picture { get; set; }
        public string PictureStr { get; set; }
    }
}
