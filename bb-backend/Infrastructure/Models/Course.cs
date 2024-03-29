﻿namespace Infrastructure.Models
{
    public class Course : Entity
    {
        public string MediaPath { get; set; }

        public float DurationHours { get; set; }

        public string Title { get; set; }
        
        public string Description { get; set; }

        public decimal Price { get; set; }

        public long CertificateTemplateId { get; set; }
        public virtual CertificateTemplate CertificateTemplate { get; set; }

        public long ExamId { get; set; }

        public virtual Exam Exam { get; set; }

        public virtual ICollection<Lesson> Lessons { get; set; }
        public virtual ICollection<CourseProgress> CourseProgresses { get; set; }

        public virtual ICollection<OrderLine> OrderLines { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}