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
            var data = new StudentSystemData();
            data.SaveChanges();
            //var materialToAdd = new Material();
            //materialToAdd.Name = "EF Code First Demo";
            //materialToAdd.Link = "http://github.com/EFCF/EFCFDemo.ppt";
            //materialToAdd.MaterialType = MaterialType.CodeStubs;
            //data.Materials.Add(materialToAdd);

            //data.SaveChanges();

            //Console.WriteLine("Material added successfuly: Id: {0}", materialToAdd.Id);

            //var materialToDelete = data.Materials.All(m => m.Id == 2).FirstOrDefault();
            //data.Materials.Delete(materialToDelete);

            //data.SaveChanges();
            //Console.WriteLine("Material added successfuly: Id: {0}", materialToDelete.Id);
            //StudentSystemDbContext dbContext = new StudentSystemDbContext();

            //Student student = dbContext.Students.FirstOrDefault();
            //Console.WriteLine(student.Name);
        }
    }
}
