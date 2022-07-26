using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.Concrete
{
    public class StudentCourse:IEntity
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public int StudentId { get; set; }
    }
}
