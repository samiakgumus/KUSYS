using Core.Dtos;
 
using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public  interface IStudentService
    {
        int GetTotalStudentCount(string sortColumn, string sortColumnDirection, string searchValue);
        List<StudentListDto> GetStudents(int skip, int pageSize, string sortColumn, string sortColumnDirection, string searchValue);
        Student Get(int id);
        int Add(Student student);
        int Update(Student student);
        Student GetByUserId(int id);
    }
}
