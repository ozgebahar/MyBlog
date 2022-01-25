using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyBlog.Admin.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Bu alan zorunludur.")]
        [StringLength(30, ErrorMessage = "Maksimum 30 karakter girilebilir.")]
        public string Firstname { get; set; }

        [Required(ErrorMessage = "Bu alan zorunludur.")]
        [StringLength(50, ErrorMessage = "Maksimum 30, minimum 2 karakter girilebilir.", MinimumLength =2)]
        public string Lastname { get; set; }

        [Required(ErrorMessage = "Bu alan zorunludur.")]
        [EmailAddress(ErrorMessage ="ornek@mail.com şeklinde giriş yapınız.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Bu alan zorunludur.")]
        [StringLength(12, ErrorMessage = "Maksimum 12, minimum 4 karakter girilebilir.", MinimumLength = 4)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Bu alan zorunludur.")]
        [StringLength(12, ErrorMessage = "Maksimum 12, minimum 4 karakter girilebilir.", MinimumLength = 4)]
        [Compare(nameof(Password),ErrorMessage ="Şifreler uyuşmuyor.")]
        public string ConfirmPassword { get; set; }
    }
}
