using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Core.Dtos;
using Core.Entities.Concrete;
using Microsoft.AspNetCore.Identity;

namespace KUSYS.Initial
{
    public static class IdentityDataInitializer
    {

        

        public static void SeedUsers(IAuthService authService,IRoleService roleService, ICourseService courseService)
        {
             

            var userExists = authService.UserExists("admin");
            if (userExists.Success)
            {
                UserForRegisterDto model = new UserForRegisterDto();
                model.FirstName = "System";
                model.LastName = "Admin";
                model.Username = "admin";
                model.Password = "123456";
                 
                model.Role = 100;
                var registerResult = authService.Register(model);
                
            }

            var adminRoleExists = roleService.RoleExist(100);
            if (adminRoleExists == false)
            {
                Role role = new Role();
                role.Code = 100;
                role.Name = "Admin";

                roleService.Add(role);
            }
            var userRoleExists = roleService.RoleExist(200);
            if (userRoleExists == false)
            {
                Role role = new Role();
                role.Code = 200;
                role.Name = "User";

                roleService.Add(role);
            }

            var courseExists1 = courseService.CourseExists("CSI101");
            if (courseExists1 == false)
            {
                Course course = new Course();
                course.CourseId = "CSI101";
                course.CourseName = "Introduction to Computer Science";
                course.Status = true;

                courseService.Add(course);
            }
            var courseExists2 = courseService.CourseExists("CSI102");
            if (courseExists2 == false)
            {
                Course course = new Course();
                course.CourseId = "CSI102";
                course.CourseName = "Algorithms";
                course.Status = true;

                courseService.Add(course);
            }
            var courseExists3 = courseService.CourseExists("MAT101");
            if (courseExists3 == false)
            {
                Course course = new Course();
                course.CourseId = "MAT101";
                course.CourseName = "Calculus";
                course.Status = true;

                courseService.Add(course);
            }
            var courseExists4 = courseService.CourseExists("PHY101");
            if (courseExists4 == false)
            {
                Course course = new Course();
                course.CourseId = "PHY101";
                course.CourseName = "Physics";
                course.Status = true;

                courseService.Add(course);
            }
        }


        public static void SeedData(IAuthService authService, IRoleService roleService, ICourseService courseService)
        {
           
            SeedUsers(authService,roleService, courseService);
        }
    }
}
