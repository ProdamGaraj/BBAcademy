using Infrastructure.Models.Enum;

namespace Infrastructure.Models
{
    public class User : Entity
    {
        public UserRole Role { get; set; }

        public string Phone { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }

        public string Login { get; set; }

        public string PasswordHash { get; set; }

        public string PhotoPath { get; set; }

        public bool Sex { get; set; }

        public string Organisation { get; set; }

        public string JobTitle { get; set; }


        public string AboutMe { get; set; }

        public virtual ICollection<CourseProgress> CourseProgresses { get; set; }

        public virtual ICollection<Certificate> Certificates { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}