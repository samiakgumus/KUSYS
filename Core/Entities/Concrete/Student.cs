using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.Concrete
{
    public class Student:IEntity
    {
        public int Id { get; set; }
        public string? StudentNo { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? BirthDate { get; set; }
        public int UserId { get; set; }
        public bool Status { get; set; }
        public bool Deleted { get; set; }
        public DateTime? CreateDate { get; set; }
        public int CreatedUser { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int UpdatedUser { get; set; }

    }
}
