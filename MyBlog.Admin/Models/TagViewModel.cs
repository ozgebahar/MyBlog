using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyBlog.Admin.Models
{
    public class TagViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Zorunlu alan")]
        [StringLength(50, ErrorMessage = "Maksimum 50 karakter girilebilir.")]
        public string Name { get; set; }

       
    }
}
