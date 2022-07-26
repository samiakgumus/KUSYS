using Business.Abstract;
using Core.Dtos;
 
using Core.Entities.Concrete;
using DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class StudentManager:IStudentService
    {
        IStudentDal _studentDal;


        public StudentManager(IStudentDal studentDal)
        {
            _studentDal = studentDal;

        }

        public Student Get(int id)
        {
            return _studentDal.Get(filter:s=>s.Id==id);
        }

        public Student GetByUserId(int id)
        {
            return _studentDal.Get(filter: s => s.UserId == id);
        }

        public int Add(Student student)
        {
            return _studentDal.Add(student);
        }

        public int Update(Student student)
        {
            return _studentDal.Update(student);
        }

        public List<StudentListDto> GetStudents(int skip, int pageSize, string sortColumn, string sortColumnDirection, string searchValue)
        {
            return _studentDal.GetStudents( skip,  pageSize,  sortColumn,  sortColumnDirection,  searchValue);
        }

        public int GetTotalStudentCount(string sortColumn, string sortColumnDirection, string searchValue)
        {
            return _studentDal.GetTotalStudentCount( sortColumn,  sortColumnDirection,  searchValue);
        }
    }
}
