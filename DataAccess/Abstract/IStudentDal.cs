using Core.DataAccess;
using Core.Dtos;
 
using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface  IStudentDal: IEntityRepository<Student>
    {
        List<StudentListDto> GetStudents(int skip, int pageSize, string sortColumn, string sortColumnDirection, string searchValue);
        int GetTotalStudentCount(string sortColumn, string sortColumnDirection, string searchValue);
    }
}
