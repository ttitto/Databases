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
            string input = string.Empty;
            do
            {
                PrintMenu();
                input = Console.ReadLine();
                switch (input.ToLower())
                {
                    case "1":
                        ListStudentsWithHomeworks(data);
                        break;
                    case "2":
                        ListCoursesWithMaterials(data);
                        break;
                    case "3":
                        if (AddCourseWithMaterials(data) > 0)
                        {
                            Console.WriteLine("Course with material added successfully");
                        }
                        break;
                    case "4":
                        if (AddStudent(data) > 0)
                        {
                            Console.WriteLine("Student added successfully");
                        }
                        break;
                    case "5":
                        if (AddCourseMaterial(data) > 0)
                        {
                            Console.WriteLine("Course material added successfully");
                        }
                        break;
                    case "end":
                        Console.WriteLine("Good Bye!");
                        break;
                    default:
                        Console.WriteLine("Invalid Input. Try again".ToUpper());
                        break;
                }
            } while (input.ToLower() != "end");
        }

        private static int AddCourseMaterial(StudentSystemData data)
        {
            Material material = new Material
            {
                Name = "JSAdvanced Simulating OOP Homework",
                Link = "https://softuni.bg/downloads/svn/javascript-oop/March-2015/2.%20Simulating-OOP-in-JavaScript-Homework.docx",
                MaterialType = MaterialType.HomeworkAssignment
            };

            data.Materials.Add(material);
            return data.SaveChanges();
        }

        private static int AddStudent(StudentSystemData data)
        {
            Student student = new Student
            {
                Name = "Gergana Gencheva",
                RegistrationDate = DateTime.Now,
                BirthDay = new DateTime(1825, 02, 02),
                PhoneNumber = "123469899"
            };

            data.Students.Add(student);
            return data.SaveChanges();
        }

        private static int AddCourseWithMaterials(StudentSystemData data)
        {
            Course course = new Course
            {
                Name = "JS Advanced",
                Description = "JavaScript course for advanced programmers",
                StartDate = new DateTime(2015, 02, 27),
                EndDate = new DateTime(2015, 04, 21),
                Price = 0m
            };

            Material material = new Material
            {
                Name = "JSAdvanced Function Expressions Demo",
                Link = "https://github.com/SoftUni/Advanced-JavaScript/tree/master/1.%20Functions-and-Function-Expressions",
                MaterialType = MaterialType.CodeStubs
            };

            data.Materials.Add(material);
            course.Materials.Add(material);
            //material.Courses.Add(course);
            data.Courses.Add(course);
            return data.SaveChanges();
        }

        private static void ListCoursesWithMaterials(StudentSystemData data)
        {
            StringBuilder sb = new StringBuilder();
            var courses = data.Courses.All().ToList();
            foreach (Course course in courses.ToList())
            {
                sb.AppendFormat("Course {0}", course.Name);
                sb.Append("\n\tMaterials:");
                foreach (var material in course.Materials)
                {
                    sb.AppendFormat("\n\t\tMaterial: {0}", material.Name);
                }

                sb.AppendLine();
            }

            Console.WriteLine(sb.ToString());
        }

        private static void ListStudentsWithHomeworks(StudentSystemData data)
        {
            StringBuilder sb = new StringBuilder();
            var students = data.Students.All().ToList();
            foreach (Student student in students.ToList())
            {
                sb.AppendFormat("Student {0}", student.Name);
                sb.Append("\n\tHomeworks:");
                foreach (var hw in student.Homeworks)
                {
                    sb.AppendFormat("\n\t\tHomework Sent on:{0}, Course: {1}", hw.SentDate.ToString(), hw.Course.Name);
                }
                sb.AppendLine();
            }
            Console.WriteLine(sb.ToString());
        }

        private static void PrintMenu()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("1. •	Lists all students and their homework submissions");
            sb.AppendLine("2. •	List all course and their materials");
            sb.AppendLine("3. •	Adds a new course with some materials");
            sb.AppendLine("4. •	Adds a new student");
            sb.AppendLine("5. •	Adds a new course material");
            sb.AppendLine("Print \"End\" to EXIT!");
            Console.WriteLine(sb.ToString());
        }
    }
}
