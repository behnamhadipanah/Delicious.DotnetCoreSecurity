using Microsoft.EntityFrameworkCore;
using NetCoreSecurity.WebClient.Models.Student;
using System.Collections.Generic;

namespace NetCoreSecurity.WebClient.Models.DataServices
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
