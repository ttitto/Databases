namespace StudentSystem.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Material
    {
        private ICollection<Course> courses;

        public Material()
        {
            this.Courses = new HashSet<Course>();
        }

        public int Id { get; set; }

        [Required(ErrorMessage = "Material's Name is required", ErrorMessageResourceName = "Material Name")]
        [MinLength(2, ErrorMessage = "Material's name should be at least 2 chars", ErrorMessageResourceName = "Material Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Material's type is required", ErrorMessageResourceName = "Material MaterialType")]
        public MaterialType MaterialType { get; set; }

        [Required(ErrorMessage = "Material's Link is required", ErrorMessageResourceName = "Material Link")]
        public string Link { get; set; }

        public ICollection<Course> Courses
        {
            get { return this.courses; }
            set { this.courses = value; }
        }
    }
}
