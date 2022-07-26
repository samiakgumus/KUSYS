using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete
{
    public class EfStudentCourseDal : EfEntityRepositoryBase<StudentCourse, KUSYSContext>, IStudentCourseDal
    {
        public int UpdateStudentCourses(List<int> studentCourses, int studentId)
        {
            using (var context = new KUSYSContext())
            {

                var dataCurrentStudentCourses = (from sc in context.StudentCourses where sc.StudentId == studentId select sc).ToList();
                context.RemoveRange(dataCurrentStudentCourses);

               var deleteResult= context.SaveChanges();

                if (studentCourses != null && studentCourses.Count > 0)
                {
                    foreach (var item in studentCourses)
                    {
                        context.Add(new StudentCourse { CourseId = item, StudentId = studentId });
                    }
                    return context.SaveChanges();
                }
                else
                {
                    return deleteResult;
                }


            }
        }

        public List<Core.Entities.Concrete.Course> GetSelectedCourses( int studentId)
        {
            using (var context = new KUSYSContext())
            {

                var dataCurrentStudentCourses = (from sc in context.StudentCourses 
                                                 join c in context.Courses on sc.CourseId equals c.Id
                                                 
                                                 where sc.StudentId == studentId select c).ToList();
                

                

                return dataCurrentStudentCourses;



            }
        }
    }
}
