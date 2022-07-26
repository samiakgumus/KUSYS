using Business.Abstract;
using Core.Dtos;

using Core.Entities.Concrete;
using Core.Extensions;
using Core.Utilities.Results;
using Core.Utilities.Security.Hasing;
using KUSYS.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace KUSYS.Controllers
{
    [Authorize]
    public class StudentController : Controller
    {
        private readonly IStudentService _studentService;
        private readonly IUserService _userService;
        private readonly ICourseService _courseService;
        private readonly IStudentCourseService _studentCourseService;

        public StudentController(IStudentService studentService, IUserService userService, ICourseService courseService, IStudentCourseService studentCourseService)
        {
            _studentService = studentService;
            _userService = userService;
            _courseService = courseService;
            _studentCourseService = studentCourseService;
        }


        public IActionResult Index()
        {
            ViewBag.ActivePage = "Student";

            return View();
        }

        #region Get all students datatable method for admin
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult GetStudents()
        {

            var draw = Request.Form["draw"].FirstOrDefault();
            var start = Request.Form["start"].FirstOrDefault();
            var length = Request.Form["length"].FirstOrDefault();
            var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
            var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
            var searchValue = Request.Form["search[value]"].FirstOrDefault();
            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordsTotal = 0;

            recordsTotal = _studentService.GetTotalStudentCount(sortColumn, sortColumnDirection, searchValue);
            var data = _studentService.GetStudents(skip, pageSize, sortColumn, sortColumnDirection, searchValue);
            var jsonData = new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data };
            return Ok(jsonData);



        }
        #endregion

        #region Gets Add or edit student modal content 
        public IActionResult GetAddEditStudentForm(int id)
        {
            var model = new AddStudentDto();
            if (id > 0)
            {
                var dataStudent = _studentService.Get(id);


                if (dataStudent != null)
                {

                    var dataUser = _userService.Get(dataStudent.UserId);
                    if (dataUser != null)
                    {
                        model.Phone = dataUser.Phone;
                        model.Email = dataUser.Email;
                    }

                    model.FirstName = dataStudent.FirstName;
                    model.LastName = dataStudent.LastName;
                    model.Id = dataStudent.Id;
                    model.BirthDate = dataStudent.BirthDate.GetValueOrDefault();
                    model.Status = dataStudent.Status;
                    model.StudentNo = dataStudent.StudentNo;


                    var dataStudentCourses = _studentCourseService.GetList(id);
                    model.Courses = dataStudentCourses.Select(sc => sc.CourseId).ToList();




                }


            }


            ViewBag.CourseList = _courseService.GetList();

            return PartialView("~/Views/Student/_Form.cshtml", model);
        }
        #endregion


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddStudent(AddStudentDto model)
        {
            int userId = 0;
            Int32.TryParse(User.GetClaimValue(System.Security.Claims.ClaimTypes.Sid), out userId);
            DateTime date = System.DateTime.Now;
            try
            {
                var userModel = new User();

                userModel.Email = model.Email;
                userModel.UserName = model.Email;
                userModel.FirstName = model.FirstName;
                userModel.LastName = model.LastName;
                userModel.Phone = model.Phone;
                userModel.Role = 200;
                userModel.InsertedDate = date;
                userModel.InsertedUserId = userId;
                userModel.Status = true;

                byte[] passwordHash, passwordSalt;

                HashingHelper.CreatePasswordHash(model.Password, out passwordHash, out passwordSalt);
                userModel.PasswordSalt = passwordSalt;
                userModel.PasswordHash = passwordHash;

                if (_userService.Add(userModel) > 0)
                {
                    var studentModel = new Student();
                    studentModel.Status = model.Status;
                    studentModel.CreatedUser = userId;
                    studentModel.CreateDate = date;
                    studentModel.FirstName = model.FirstName;
                    studentModel.LastName = model.LastName;
                    studentModel.BirthDate = model.BirthDate;
                    studentModel.StudentNo = model.StudentNo;
                    studentModel.UserId = userModel.Id;

                    if (_studentService.Add(studentModel) > 0)
                    {
                        _studentCourseService.UpdateStudentCourses(model.Courses, studentModel.Id);

                        return Json(new SuccessResult("The process has been done successfully"));
                    }
                    else
                    {
                        return Json(new ErrorResult("Error occured while creating user"));
                    }

                }
                else
                {

                    return Json(new ErrorResult("Temporarily unavailable. Please try again later"));
                }




            }
            catch (Exception ex)
            {
                return Json(new ErrorResult("Temporarily unavailable. Please try again later"));
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditStudent(AddStudentDto model)
        {
            int userId = 0;
            Int32.TryParse(User.GetClaimValue(System.Security.Claims.ClaimTypes.Sid), out userId);
            DateTime date = System.DateTime.Now;
            try
            {
                var dataStudent = _studentService.Get(model.Id);


                if (dataStudent != null)
                {

                    var dataUser = _userService.Get(dataStudent.UserId);

                    if (dataUser != null)
                    {
                        dataUser.Email = model.Email;
                        dataUser.UserName = model.Email;
                        dataUser.FirstName = model.FirstName;
                        dataUser.LastName = model.LastName;
                        dataUser.Phone = model.Phone;
                        dataUser.Role = 200;
                        dataUser.InsertedDate = date;
                        dataUser.InsertedUserId = userId;
                        dataUser.Status = true;

                        if (!String.IsNullOrEmpty(model.Password))
                        {
                            byte[] passwordHash, passwordSalt;

                            HashingHelper.CreatePasswordHash(model.Password, out passwordHash, out passwordSalt);
                            dataUser.PasswordSalt = passwordSalt;
                            dataUser.PasswordHash = passwordHash;
                        }
                        _userService.Update(dataUser);
                    }



                    dataStudent.Status = model.Status;
                    dataStudent.CreatedUser = userId;
                    dataStudent.CreateDate = date;
                    dataStudent.FirstName = model.FirstName;
                    dataStudent.LastName = model.LastName;
                    dataStudent.BirthDate = model.BirthDate;
                    dataStudent.StudentNo = model.StudentNo;
                    dataStudent.UserId = dataUser != null ? dataUser.Id : 0;

                    if (_studentService.Update(dataStudent) > 0)
                    {
                        _studentCourseService.UpdateStudentCourses(model.Courses, dataStudent.Id);

                        return Json(new SuccessResult("The process has been done successfully"));
                    }
                    else
                    {
                        return Json(new ErrorResult("Error occured while creating user"));
                    }



                }
                else
                {
                    return Json(new ErrorResult("Student not found"));
                }





            }
            catch (Exception ex)
            {
                return Json(new ErrorResult("Temporarily unavailable. Please try again later"));
            }

        }

        [HttpPost]
        public IActionResult DeleteStudent(int id)
        {

            try
            {

                int userId = 0;
                Int32.TryParse(User.GetClaimValue(System.Security.Claims.ClaimTypes.Sid), out userId);
                var currentDate = System.DateTime.Now;

                var dataStudent = _studentService.Get(id);
                if (dataStudent != null)
                {
                    dataStudent.Deleted = true;
                    dataStudent.UpdateDate = currentDate;
                    dataStudent.UpdatedUser = userId;
                    if (_studentService.Update(dataStudent) > 0)
                    {
                        var datauser = _userService.Get(dataStudent.UserId);
                        if (datauser != null)
                        {
                            datauser.UpdatedDate = currentDate;
                            datauser.UpdatedUserId = userId;
                            datauser.Deleted = true;
                            if (_userService.Update(datauser) > 0)
                            {

                            }
                            else
                            {
                                return Json(new ErrorResult("Error occured while updating user"));
                            }


                        }

                        return Json(new SuccessResult("User deleted successfully"));

                    }
                    else
                    {
                        return Json(new ErrorResult("Error occured while updating student"));
                    }


                }
                else
                {
                    return Json(new ErrorResult("Student not found"));
                }



            }
            catch (Exception ex)
            {
                return Json(new ErrorResult("Unexpected error occured. Please try again later."));
            }

        }


        #region Get Student Selected Courses For Student Datatable Details
        [HttpPost]
        public IActionResult GetStudentDatatableDetails(int id)
        {

            try
            {

                var dataStudentCourses = _studentCourseService.GetList(id);
                if (dataStudentCourses != null && dataStudentCourses.Count > 0)
                {
                    return PartialView("~/Views/Student/_DtDetailSelectedCourses.cshtml", _courseService.GetList(dataStudentCourses.Select(sc => sc.CourseId).ToList()));

                }
                else
                {
                    return PartialView("~/Views/Student/_DtDetailSelectedCourses.cshtml", null);
                }

            }
            catch (Exception ex)
            {
                return PartialView("~/Views/Student/_DtDetailSelectedCourses.cshtml", null);
            }

        }
        #endregion


        #region Gets select course form for student
        [Authorize(Roles = "User")]
        public IActionResult GetSelectCourseForm()
        {
            var dataCourses = _courseService.GetList();

            List<StudentSelectCourseDto> courses = new List<StudentSelectCourseDto>();

            int userId = 0;
            Int32.TryParse(User.GetClaimValue(System.Security.Claims.ClaimTypes.Sid), out userId);
            var dataSelectedCourses = _studentCourseService.GetSelectedCourses(userId);


            foreach (var item in dataCourses.Where(c => !dataSelectedCourses.Select(sc => sc.Id).ToList().Contains(c.Id)))
            {
                courses.Add(new StudentSelectCourseDto { CourseId = item.Id, CourseName = item.CourseId + " " + item.CourseName, Selected = false });
            }

            return PartialView("~/Views/Student/_SelectCourse.cshtml", courses);
        }
        #endregion

        #region Select course method for student
        [Authorize(Roles = "User")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SelectCourse(List<StudentSelectCourseDto> selectedCourses)
        {
            int userId = 0;
            Int32.TryParse(User.GetClaimValue(System.Security.Claims.ClaimTypes.Sid), out userId);

            try
            {
                foreach (var item in selectedCourses.Where(c => c.Selected == true))
                {
                    _studentCourseService.Add(new StudentCourse { CourseId = item.CourseId, StudentId = userId });
                }

                return Json(new SuccessResult("Courses selected successfully"));
            }
            catch
            {
                return Json(new ErrorResult("Unexpected error occured. Please try again later."));
            }


        }

        #endregion


        #region Gets selected courses for student
        [Authorize(Roles = "User")]
        public IActionResult SelectedCourses()
        {
            ViewBag.ActivePage = "StudentSelectedCourses";

            int userId = 0;
            Int32.TryParse(User.GetClaimValue(System.Security.Claims.ClaimTypes.Sid), out userId);

            var dataSelectedCourses = _studentCourseService.GetSelectedCourses(userId);

            return View("~/Views/Student/SelectedCourses.cshtml", dataSelectedCourses.Select(c => c.CourseId + " " + c.CourseName).ToList());
        }

        #endregion

        #region Gets student profile page

        [Authorize(Roles = "User")]
        public IActionResult Profile()
        {
            ViewBag.ActivePage = "StudentProfile";

            int userId = 0;
            Int32.TryParse(User.GetClaimValue(System.Security.Claims.ClaimTypes.Sid), out userId);

            var model = new StudentProfileDto();

            var dataUser = _userService.Get(userId);
            if (dataUser != null)
            {
                var dataStudent = _studentService.GetByUserId(dataUser.Id);
                if (dataStudent != null)
                {
                    model.StudentNo = dataStudent.StudentNo;
                    model.BirthDate = dataStudent.BirthDate.GetValueOrDefault();
                    model.FirstName = dataStudent.FirstName;
                    model.LastName = dataStudent.LastName;
                    model.Email = dataUser.Email;
                    model.Phone = dataUser.Phone;

                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return NotFound();
            }



            return View(model);
        }
        #endregion

        #region Gets student profile page for admin datatable student detail modal

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult ProfilePartial(int id)
        {


            var model = new StudentProfileDto();
            var dataStudent = _studentService.Get(id);

            if (dataStudent != null)
            {
                var dataUser = _userService.Get(dataStudent.UserId);
                if (dataUser != null)
                {
                    model.StudentNo = dataStudent.StudentNo;
                    model.BirthDate = dataStudent.BirthDate.GetValueOrDefault();
                    model.FirstName = dataStudent.FirstName;
                    model.LastName = dataStudent.LastName;
                    model.Email = dataUser.Email;
                    model.Phone = dataUser.Phone;

                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return NotFound();
            }



            return PartialView("~/Views/Student/_Profile.cshtml", model);
        }
        #endregion

    }
}
