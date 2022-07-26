using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Core.Dtos
{
   public class UserForLoginDto :IDto
    {
        [Required(ErrorMessage ="Required")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Required")]
        public string Password { get; set; }
    }
}
