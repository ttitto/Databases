namespace DAOClass
{
    using Softuni.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class DAOClass
    {
        private static SoftUniEntities softuniDbContext = new SoftUniEntities();

        static void Main(string[] args)
        {
            DeleteEmployee(1089); // does not delete any employee because no such ID
            UpdateEmployee(23, "JobTitle", "Production Control Manager");
            // UpdateEmployee(23, "HireDate", "12.12.2015"); // throws ArgumentException because DateTime is expected

            Employee employeeToInsert = new Employee();
            employeeToInsert.FirstName = "Pesho";
            employeeToInsert.LastName = "Peshev";
            employeeToInsert.HireDate = DateTime.Now;
            employeeToInsert.JobTitle = "ASP.NET Developer";
            employeeToInsert.ManagerID = 45;
            employeeToInsert.MiddleName = "Mishev";
            employeeToInsert.Salary = 2345.80m;
            employeeToInsert.AddressID = 3;
            employeeToInsert.DepartmentID = 4;

            int newEmployeeId = InsertEmployee(employeeToInsert);
            Console.WriteLine("Employee with ID = {0} added.", newEmployeeId);

        }

        public static int InsertEmployee(Employee employee)
        {
            softuniDbContext.Employees.Add(employee);
            softuniDbContext.SaveChanges();
            return employee.EmployeeID;
        }

        public static void UpdateEmployee(int employeeId, string propertyName, object newValue)
        {
            Employee employee = softuniDbContext.Employees.Find(employeeId);

            if (null != employee)
            {
                employee.GetType().GetProperty(propertyName).SetValue(employee, newValue);
                softuniDbContext.SaveChanges();
            }
        }

        public static void DeleteEmployee(int employeeId)
        {
            Employee employeeToDelete = softuniDbContext.Employees.Find(employeeId);

            if (null != employeeToDelete)
            {
                softuniDbContext.Employees.Remove(employeeToDelete);
                softuniDbContext.SaveChanges();
            }
        }
    }
}
