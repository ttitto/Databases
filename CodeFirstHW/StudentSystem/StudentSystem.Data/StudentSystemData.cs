namespace StudentSystem.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using StudentSystem.Data.Repositories;
    using StudentSystem.Models;

    public class StudentSystemData : IStudentSystemData
    {
        private IStudentSystemDbContext context;
        private IDictionary<Type, object> repositories;

        public StudentSystemData()
            : this(new StudentSystemDbContext())
        {
        }

        public StudentSystemData(IStudentSystemDbContext context)
        {
            this.context = context;
            this.repositories = new Dictionary<Type, object>();
        }

        public IGenericRepository<Student> Students
        {
            get { return this.GetRepository<Student>(); }
        }

        public IGenericRepository<Homework> Homeworks
        {
            get { return this.GetRepository<Homework>(); }
        }

        public IGenericRepository<Course> Courses
        {
            get { return this.GetRepository<Course>(); }
        }

        public IGenericRepository<Material> Materials
        {
            get { return this.GetRepository<Material>(); }
        }

        public int SaveChanges()
        {
            return this.context.SaveChanges();
        }

        private IGenericRepository<T> GetRepository<T>() where T : class
        {
            Type typeOfModel = typeof(T);

            if (!this.repositories.ContainsKey(typeOfModel))
            {
                Type typeOfRepository = typeof(GenericRepository<T>);
                this.repositories.Add(typeOfModel, Activator.CreateInstance(typeOfRepository, this.context));
            }

            return this.repositories[typeOfModel] as IGenericRepository<T>;
        }
    }
}
