using Core.DataAccess.EntityFramework;
using Core.Dtos;
 
using Core.Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete
{
    public class EfStudentDal : EfEntityRepositoryBase<Student, KUSYSContext>, IStudentDal
    {
        public List<StudentListDto> GetStudents(int skip, int pageSize, string sortColumn,string sortColumnDirection,string searchValue )
        {
            using (var context = new KUSYSContext())
            {


                var studentsQuery = (from s in context.Students
                                    

                                    where s.Deleted==false
                                    select new StudentListDto
                                    {
                                        FirstName = s.FirstName,
                                        LastName = s.LastName,
                                         StudentNo=s.StudentNo,
                                         BirthDate=s.BirthDate,
                                         Status=s.Status,
                                         Id=s.Id



                                    }
                             );

                if (!string.IsNullOrEmpty(sortColumn) && !string.IsNullOrEmpty(sortColumnDirection))
                {
                    studentsQuery = studentsQuery.OrderBy(sortColumn + " " + sortColumnDirection);
                }


                if (!string.IsNullOrEmpty(searchValue))
                {
                    studentsQuery = studentsQuery.Where(m => m.FirstName.Contains(searchValue)
                                                || m.LastName.Contains(searchValue)
                                                || m.StudentNo.Contains(searchValue)
                                                 );
                }

               
                return studentsQuery.Skip(skip).Take(pageSize).ToList();

            }
        }

        public int GetTotalStudentCount(string sortColumn, string sortColumnDirection, string searchValue)
        {
            using (var context = new KUSYSContext())
            {



                var studentsQuery = (from s in context.Students


                                     where s.Deleted == false
                                     select new StudentListDto
                                     {
                                         FirstName = s.FirstName,
                                         LastName = s.LastName,
                                         StudentNo = s.StudentNo,
                                         BirthDate = s.BirthDate,
                                         Status = s.Status,
                                         Id = s.Id



                                     }
                             );

                if (!string.IsNullOrEmpty(sortColumn) && !string.IsNullOrEmpty(sortColumnDirection))
                {
                    studentsQuery = studentsQuery.OrderBy(sortColumn + " " + sortColumnDirection);
                }


                if (!string.IsNullOrEmpty(searchValue))
                {
                    studentsQuery = studentsQuery.Where(m => m.FirstName.Contains(searchValue)
                                                || m.LastName.Contains(searchValue)
                                                || m.StudentNo.Contains(searchValue)
                                                 );
                }


                return studentsQuery.Count();


            }
        }
    }
}
