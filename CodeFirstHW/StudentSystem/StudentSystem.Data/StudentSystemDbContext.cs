namespace StudentSystem.Data
{
    using System.Data.Entity;

    using StudentSystem.Data.Migrations;
    using StudentSystem.Models;

    public class StudentSystemDbContext : DbContext, IStudentSystemDbContext
    {
        public StudentSystemDbContext()
            : base("StudentSystemDbContext")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<StudentSystemDbContext, Configuration>());
        }

        public IDbSet<Student> Students { get; set; }

        public IDbSet<Course> Courses { get; set; }

        public IDbSet<Homework> Homeworks { get; set; }

        public IDbSet<Material> Materials { get; set; }
        
        public new IDbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }
    }
}