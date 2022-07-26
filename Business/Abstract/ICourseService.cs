using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICourseService
    {
        List<Course> GetList();
        List<Course> GetList(List<int> idList);
        int Add(Course course);
        bool CourseExists(string courseId);
    }
}
