namespace SomeEFActions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Softuni.Models;
    using System.Data.Entity;

    public class SomeEFActionsClass
    {
        private static SoftUniEntities softuniDbContext = new SoftUniEntities();

        static void Main(string[] args)
        {
            // Task 03.
            foreach (Employee emp in EmployeesWithProjectsInGivenYear(2002))
            {
                Console.WriteLine("{0} {1} - Project Names: {2}",
                    emp.FirstName,
                    emp.LastName,
                    string.Join(", ", emp.Projects.Select(p => p.Name)));
            }

            // Task 04.
            foreach (Employee emp in EmployeesWithProjectsInGivenYear("2002"))
            {
                Console.WriteLine("{0} {1}",
                  emp.FirstName,
                  emp.LastName);
            }

            // Task 08.
            var employeesForProject = softuniDbContext.Employees.Where(e => e.EmployeeID == 4 && e.EmployeeID == 45).ToList();

            Project newProject = AddProject("Test project", new DateTime(2014, 11, 25), new DateTime(2015, 05, 25), employeesForProject);

            if (null != newProject)
            {
                Console.WriteLine("Project {0} successfully added", newProject.Name);
            }


            // Task 09.

            Console.WriteLine(GetProjectsCountByEmployee("Guy", "Gilbert"));
            Console.WriteLine(GetProjectsCountByEmployee("Andrew", "Cencini"));
            Console.WriteLine(GetProjectsCountByEmployee("Andrew", "Hill"));

            Console.ReadLine();
        }

        // Task 03.
        public static IQueryable<Employee> EmployeesWithProjectsInGivenYear(int startingYear)
        {
            var employees = softuniDbContext.Employees
                  .Where(e => e.Projects.Any(p => p.StartDate.Year == startingYear));
            return employees;
        }

        // Task 04.
        public static List<Employee> EmployeesWithProjectsInGivenYear(string startingYear)
        {
            string query = @"SELECT DISTINCT e.[EmployeeID]
      ,[FirstName]
      ,[LastName]
      ,[MiddleName]
      ,[JobTitle]
      ,[DepartmentID]
      ,[ManagerID]
      ,[HireDate]
      ,[Salary]
      ,[AddressID]
  FROM [SoftUni].[dbo].[Employees] e
  JOIN EmployeesProjects ep ON ep.EmployeeID = e.EmployeeID
  JOIN Projects p ON p.ProjectID = ep.ProjectID
  WHERE DATEPART(YEAR, p.StartDate) = {0}";
            object[] parameters = { startingYear };
            var employees = softuniDbContext.Database.SqlQuery<Employee>(query, parameters);
            return employees.ToList();
        }

        // Task 05.
        public static IQueryable<Employee> GetEmployeesByDepartmentAndManager(
            string departmentName,
            string managerFirstName,
            string managerLastName)
        {
            IQueryable<Employee> employees = softuniDbContext.Employees
                 .Where(e => e.Department.Name == departmentName &&
                 e.Employee1.FirstName == managerFirstName &&
                 e.Employee1.LastName == managerLastName);

            return employees;
        }

        // Task 06 from previous version of the tasks file.
        /* Generate database twin from edmx models
         * 1. Right click in the diagram in the models edmx file => Generate Database from Model
         * 2. Save the generated script, open it and change the name of the database to SoftUniTwin
         * 3. Run the updated script in SQL Server
         */

        // Task 08.
        public static Project AddProject(
            string projectName,
            DateTime startDate,
            DateTime endDate,
            ICollection<Employee> employees)
        {
            using (softuniDbContext)
            {
                using (DbContextTransaction newProjectTransaction = softuniDbContext.Database.BeginTransaction())
                {
                    Project projectToAdd = new Project();
                    try
                    {
                        projectToAdd.Name = projectName;
                        projectToAdd.StartDate = startDate;
                        projectToAdd.EndDate = endDate;
                        projectToAdd.Employees = employees;

                        projectToAdd = softuniDbContext.Projects.Add(projectToAdd);
                        softuniDbContext.SaveChanges();
                        newProjectTransaction.Commit();

                    }
                    catch (Exception)
                    {
                        newProjectTransaction.Rollback();
                    }

                    return projectToAdd;
                }
            }
        }

        // Task 09.
        // Stored procedure usp_GetProjectsCountByEmployee has been created in SQL SERVER
        // EF Models were updated with the new stored procedure

        /// <summary>
        /// Method returns the total count of the projects for all found Employees with the given names.
        /// </summary>
        /// <param name="firstName">Employee FirstName</param>
        /// <param name="lastName">Employee LastName</param>
        /// <returns>Zero -  If no employee with the given names is found; Zero - If the given Employee has no projects</returns>
        public static int GetProjectsCountByEmployee(string firstName, string lastName)
        {
            using (SoftUniEntities softuniDbContext = new SoftUniEntities())
            {
                var projectsCount = softuniDbContext.usp_GetProjectsCountByEmployee(firstName, lastName).FirstOrDefault();
                return projectsCount.Value;
            }
        }
    }
}
