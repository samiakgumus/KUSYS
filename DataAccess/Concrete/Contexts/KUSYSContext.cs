using Core.Entities.Concrete;

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.EntityFramework.Contexts
{
    public class KUSYSContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string computerName = System.Environment.MachineName;
            string connectionString = "";
            switch (computerName)
            {
                case "DESKTOP-DMJSAHU":
                    // connectionString = @"Data Source=(localdb)\mssqllocaldb; Initial Catalog=KUSYS; Integrated Security=True;";
                     connectionString = @"Data Source= DESKTOP-DMJSAHU; Initial Catalog=KUSYS; Integrated Security=True;";
                  
                    break;

                default:
                    connectionString = @"Data Source=localhost; Initial Catalog=KUSYS; Integrated Security=True;";
                    break;
            }
            optionsBuilder.UseSqlServer(connectionString);


        }
 

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<StudentCourse> StudentCourses { get; set; }

    } 
}
