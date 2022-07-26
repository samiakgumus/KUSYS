using Core.DataAccess;
using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IStudentCourseDal : IEntityRepository<StudentCourse>
    {
        int UpdateStudentCourses(List<int> studentCourses, int studentId);
        List<Core.Entities.Concrete.Course> GetSelectedCourses(int studentId);
    }
}
