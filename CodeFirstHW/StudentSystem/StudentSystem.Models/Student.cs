namespace StudentSystem.Models
{
    using System;
    using System.Collections.Generic;

    using System.ComponentModel.DataAnnotations;

    using StudentSystem.Models.Attributes;

    public class Student
    {
        private ICollection<Course> courses;
        private ICollection<Homework> homeworks;

        public Student()
        {
            this.Courses = new HashSet<Course>();
            this.Homeworks = new HashSet<Homework>();
        }

        public int Id { get; set; }

        [Required(ErrorMessage = "Student's name is required", ErrorMessageResourceName = "Student Name")]
        [MinLength(5, ErrorMessage = "Student's name should be at least 5 chars", ErrorMessageResourceName = "Student Name")]
        public string Name { get; set; }

        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Student's registration date is required", ErrorMessageResourceName = "Student RegistrationDate")]
        [PassedDate(ErrorMessage = "Student's registration date should be one in the past", ErrorMessageResourceName = "Student RegistrationDate")]
        public DateTime RegistrationDate { get; set; }

        [Required(ErrorMessage = "Student's BirthDay is required", ErrorMessageResourceName = "Student BirthDay")]
        [PassedDate(ErrorMessage = "Student's BirthDay date should be one in the past", ErrorMessageResourceName = "Student BirthDay")]
        public DateTime BirthDay { get; set; }

        public virtual ICollection<Course> Courses
        {
            get { return this.courses; }
            set { this.courses = value; }
        }

        public ICollection<Homework> Homeworks
        {
            get { return this.homeworks; }
            set { this.homeworks = value; }
        }
    }
}
