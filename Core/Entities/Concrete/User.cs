using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Core.Entities.Concrete
{
    public class User :IEntity
    {
        public int Id { get; set; }       
        [Required(ErrorMessage ="Bu alan zorunlu")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Bu alan zorunlu")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Bu alan zorunlu")]
        public string UserName { get; set; }
        public byte[] PasswordSalt { get; set; }
        public byte[] PasswordHash { get; set; }
        public bool Status { get; set; }      
        public int Role { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public bool Deleted { get; set; }
        public DateTime? InsertedDate { get; set; }
        public int InsertedUserId { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int UpdatedUserId { get; set; }


    }
}
