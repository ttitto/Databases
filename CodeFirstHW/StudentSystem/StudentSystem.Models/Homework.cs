namespace StudentSystem.Models
{
    using System;

    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using StudentSystem.Models.Attributes;

    public class Homework
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Homework's content is required")]
        public string Content { get; set; }

        [Required(ErrorMessage = "Homework's content type is required")]
        public HomeworkContentType ContentType { get; set; }

        [Required(ErrorMessage = "Homework's SentDate is required")]
        [PassedDate(ErrorMessage = "Homework's SentDate should be one in the past.")]
        public DateTime SentDate { get; set; }

        public int CourseId { get; set; }

        [ForeignKey("CourseId")]
        public virtual Course Course { get; set; }

        public int StudentId { get; set; }

        [ForeignKey("StudentId")]
        public virtual Student Student { get; set; }
    }
}
