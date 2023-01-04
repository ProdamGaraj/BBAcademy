using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Backend.Models
{
    public class User : Entity
    {
        public UserRole Role { get; set; }
        public string LastName { get; set; }
        public string SurName { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string PhotoPath { get; set; }//
        public bool Sex { get; set; }
        public string Organisation { get; set; }
        public string JobTitle { get; set; }
        public List<Certificate> Certificates { get; set; }
        public List<Course> BoughtCourses { get; set; }
        public List<Course> CoursesInAction { get; set; }
        public List<Course> SolvedCourses { get; set; }//Курсы с пройденным тестированием (запретить тест)
        public List<Lesson> SolvedLessons { get; set; }
        public string AboutMe { get; set; }//Сделать ограничение
        public bool Deleted { get; set; }
        public User()
        {
               
        }
        public User( string name, DateTime createdAt, DateTime modifiedAt, string lastName, string surName, string login, string password, string photoPath, bool sex, string email, string organisation, string jobTitle, List<Certificate> certificates, List<Course> boughtCourses,List<Course> coursesInAction, List<Course> solvedCourses, List<Lesson> solvedLessons, string aboutMe, bool deleted=false)
        {
            Name = name;
            CreatedAt = createdAt;
            ModifiedAt = modifiedAt;
            LastName = lastName;
            SurName = surName;
            Login = login;
            Password = password;
            PhotoPath = photoPath;
            Sex = sex;
            Email = email;
            Organisation = organisation;
            JobTitle = jobTitle;
            Certificates = certificates;
            BoughtCourses = boughtCourses;
            CoursesInAction = coursesInAction;
            SolvedCourses = solvedCourses;
            SolvedLessons = solvedLessons;
            AboutMe = aboutMe;
            Deleted = deleted;
        }
    }
}