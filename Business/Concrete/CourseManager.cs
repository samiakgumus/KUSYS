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
    public class CourseManager : ICourseService
    {
        ICourseDal _courseDal;


        public CourseManager(ICourseDal courseDal)
        {
            _courseDal = courseDal;

        }

        public bool CourseExists(string courseId)
        {
           var dataCourse= _courseDal.Get(filter:f=>f.CourseId==courseId && f.Deleted==false  );

            return  dataCourse!=null?true:false;
        }

        public int Add(Course course)
        {
            return _courseDal.Add(course);
        }
        public List<Course> GetList()
        {
            return _courseDal.GetList(filter: f => f.Deleted == false && f.Status == true).ToList();
        }
        public List<Course> GetList(List<int> idList)
        {
            return _courseDal.GetList(filter: f =>idList.Contains(f.Id) &&  f.Deleted == false && f.Status == true).ToList();
        }
    }
}
