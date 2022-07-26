using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dtos
{
    public class AddStudentDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string StudentNo { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public DateTime BirthDate { get; set; }
        public bool Status { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string Password { get; set; }
        [Required(ErrorMessage = "This field is required")]
        [Compare("Password",ErrorMessage ="Password not matched")]
        public string ConfirmPassword { get; set; }

        public List<int>Courses { get; set; }
    }
}
