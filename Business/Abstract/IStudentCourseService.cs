using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IStudentCourseService
    {
        int UpdateStudentCourses(List<int> courses, int studentId);
        List<StudentCourse> GetList(int studentId);
        List<Core.Entities.Concrete.Course> GetSelectedCourses(int studentId);
        int Add(StudentCourse studentCourse);


    }
}
