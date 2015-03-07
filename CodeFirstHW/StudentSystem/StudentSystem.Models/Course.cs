namespace StudentSystem.Models
{
    using System;
    using System.Collections.Generic;

    using System.ComponentModel.DataAnnotations;

    public class Course
    {
        private ICollection<Student> students;
        private ICollection<Homework> homeworks;
        private ICollection<Material> materials;

        public Course()
        {
            this.Students = new HashSet<Student>();
            this.Homeworks = new HashSet<Homework>();
            this.Materials = new HashSet<Material>();
        }

        public int Id { get; set; }

        [MinLength(2, ErrorMessage = "Course's Name should be at least 2 chars long")]
        [Required(ErrorMessage = "Course's Name is required")]
        public string Name { get; set; }

        [MinLength(50, ErrorMessage = "Course's description should be at least 50 characters")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Course's start date is required")]
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "Course's price date is required")]
        public decimal Price { get; set; }

        public virtual ICollection<Student> Students
        {
            get { return this.students; }
            set { this.students = value; }
        }

        public virtual ICollection<Homework> Homeworks
        {
            get { return this.homeworks; }
            set { this.homeworks = value; }
        }

        public virtual ICollection<Material> Materials
        {
            get { return this.materials; }
            set { this.materials = value; }
        }
    }
}
