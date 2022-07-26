using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.Concrete
{
    public class Course:IEntity
    {
        public int Id { get; set; }     
        public string? CourseId { get; set; }    
        public string? CourseName { get; set; }
        public bool Status { get; set; }
        public bool Deleted { get; set; }

    }
}
