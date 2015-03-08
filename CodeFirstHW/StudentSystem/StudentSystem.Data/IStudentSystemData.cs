namespace StudentSystem.Data
{
    using StudentSystem.Data.Repositories;
    using StudentSystem.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public interface IStudentSystemData
    {
        IGenericRepository<Student> Students { get; }

        IGenericRepository<Homework> Homeworks { get; }

        IGenericRepository<Course> Courses { get; }

        IGenericRepository<Material> Materials { get; }

        int SaveChanges();
    }
}
