using Autofac;
using Business.Abstract;
using Business.Concrete;
 
using DataAccess.Abstract;
using DataAccess.Concrete;
using DataAccess.Concrete.EntityFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
        
            builder.RegisterType<EfUserDal>().As<IUserDal>();
            builder.RegisterType<EfRoleDal>().As<IRoleDal>();
            builder.RegisterType<EfStudentDal>().As<IStudentDal>();
            builder.RegisterType<EfStudentCourseDal>().As<IStudentCourseDal>();
            builder.RegisterType<EfCourseDal>().As<ICourseDal>();


            builder.RegisterType<AuthManager>().As<IAuthService>();          
            builder.RegisterType<UserManager>().As<IUserService>();
            builder.RegisterType<RoleManager>().As<IRoleService>();
            builder.RegisterType<StudentManager>().As<IStudentService>();
            builder.RegisterType<StudentCourseManager>().As<IStudentCourseService>();
            builder.RegisterType<CourseManager>().As<ICourseService>();

        }
    }
}
