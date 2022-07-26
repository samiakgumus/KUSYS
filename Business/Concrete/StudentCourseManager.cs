using Business.Abstract;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class StudentCourseManager : IStudentCourseService
    {
        IStudentCourseDal _studentCourseDal;


        public StudentCourseManager(IStudentCourseDal studentCourseDal)
        {
            _studentCourseDal = studentCourseDal;

        }

        public  int Add(StudentCourse studentCourse)
        {
            return _studentCourseDal.Add(studentCourse);
        }

        public List<StudentCourse> GetList(int studentId)
        {
            return _studentCourseDal.GetList(filter: f =>f.StudentId==studentId).ToList();
        }
       

        public int UpdateStudentCourses(List<int> courses,int studentId)
        {
            return _studentCourseDal.UpdateStudentCourses(courses,studentId);
        }

        public List<Core.Entities.Concrete.Course> GetSelectedCourses(int studentId)
        {
            return _studentCourseDal.GetSelectedCourses(studentId);
        }
    }
}
