using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Core.Dtos
{
    public class UserForRegisterDto:IDto
    {
        [MaxLength(50)]
        [Required(ErrorMessage = "Bu alan zorunlu")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Şifre tekrar alanı boş bırakılamaz.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Şifre tekrar alanı boş bırakılamaz.")]
        [Compare("Password", ErrorMessage = "Girilen şifreler uyuşmuyor.")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "Bu alan zorunlu")]
        [MaxLength(50)]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Bu alan zorunlu")]
        [MaxLength(50)]
        public string LastName { get; set; }
        public int Role { get; set; }
        
    }
}
