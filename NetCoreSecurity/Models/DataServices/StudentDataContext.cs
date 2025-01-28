using Microsoft.EntityFrameworkCore;
using NetCoreSecurity.Models.Student;
using System.Collections.Generic;

namespace NetCoreSecurity.Models.DataServices
{
    public class StudentDataContext : DbContext
    {
        public DbSet<CourseGrade> Grades { get; set; }

        public StudentDataContext(DbContextOptions<StudentDataContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
