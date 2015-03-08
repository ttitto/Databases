using StudentSystem.Models;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
namespace StudentSystem.Data
{
    public interface IStudentSystemDbContext
    {
        IDbSet<Student> Students { get; set; }

        IDbSet<Course> Courses { get; set; }

        IDbSet<Homework> Homeworks { get; set; }

        IDbSet<Material> Materials { get; set; }

        IDbSet<T> Set<T>() where T : class;

        DbEntityEntry<T> Entry<T>(T entity) where T : class;

        int SaveChanges();
    }
}
