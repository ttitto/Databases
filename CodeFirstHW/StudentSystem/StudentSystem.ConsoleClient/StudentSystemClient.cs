namespace StudentSystem.ConsoleClient
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using StudentSystem.Data;
    using StudentSystem.Models;

    public class StudentSystemClient
    {
        static void Main(string[] args)
        {
            StudentSystemDbContext dbContext = new StudentSystemDbContext();
           
            Student student = dbContext.Students.FirstOrDefault();
            Console.WriteLine(student.Name);
        }
    }
}
