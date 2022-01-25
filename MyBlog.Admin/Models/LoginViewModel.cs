using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyBlog.Admin.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Zorunlu alan")]
        [StringLength(50, ErrorMessage = "En fazla 50 karakter girilebilir.")]
        [EmailAddress(ErrorMessage = "ornek@mail.com şeklinde giriş yapınız.")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Zorunlu alan")]
        [StringLength(12, MinimumLength = 4, ErrorMessage = "En az 4, en fazla 12 karakter girilebilir.")]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
