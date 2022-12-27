

using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Backend.Models
{
    public class User : IdentityUser
    {
        [Key]
        [NotNull]
        new public long Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public string LastName { get; set; }
        public string SurName { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string PhotoPath { get; set; }//
        public bool Sex { get; set; }
        new public string Email { get; set; }//
        public List<Key> Keys { get; set; }
        public string JobTitle { get; set; }
        public List<Certificate> Certificates { get; set; }
        public List<Course> CoursesInAction { get; set; }
        public List<Course> SolvedCourses { get; set; }//Курсы с пройденным тестированием (запретить тест)
        public List<Lesson> SolvedLessons { get; set; }
        public string AboutMe { get; set; }//Сделать ограничение
        public bool Deleted { get; set; }

        public User(string name, string lastName, string surName, string login, string password, string photoPath, bool sex, string email, List<Key> keys, string jobTitle, List<Certificate> certificates, List<Course> coursesInAction, List<Course> solvedCourses, List<Lesson> solvedLessons, string aboutMe)
        {
            Name = name;
            LastName = lastName;
            SurName = surName;
            Login = login;
            Password = password;
            PhotoPath = photoPath;
            Sex = sex;
            Email = email;
            Keys = keys;
            JobTitle = jobTitle;
            Certificates = certificates;
            CoursesInAction = coursesInAction;
            SolvedCourses = solvedCourses;
            SolvedLessons = solvedLessons;
            AboutMe = aboutMe;
        }
    }
}
