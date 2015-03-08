namespace StudentSystem.Models
{
    using System;

    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using StudentSystem.Models.Attributes;

    public class Homework
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Homework's content is required", ErrorMessageResourceName = "Homework Content")]
        public string Content { get; set; }

        [Required(ErrorMessage = "Homework's content type is required", ErrorMessageResourceName = "Homework ContentType")]
        public HomeworkContentType ContentType { get; set; }

        [Required(ErrorMessage = "Homework's SentDate is required", ErrorMessageResourceName = "Homework SentDate")]
        [PassedDate(ErrorMessage = "Homework's SentDate should be one in the past.", ErrorMessageResourceName = "Homework SentDate")]
        public DateTime SentDate { get; set; }

        public int CourseId { get; set; }

        [ForeignKey("CourseId")]
        public virtual Course Course { get; set; }

        public int StudentId { get; set; }

        [ForeignKey("StudentId")]
        public virtual Student Student { get; set; }
    }
}
