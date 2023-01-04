using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Backend.Models
{
    public class User : Entity
    {
        public UserRole Role { get; set; }
        [MaybeNull]
        public string LastName { get; set; }
        [MaybeNull]
        public string SurName { get; set; }
        [MaybeNull]
        public string Email { get; set; }
        public string Login { get; set; }
        public string PasswordHash { get; set; }
        [MaybeNull]
        public string PhotoPath { get; set; }//
        [MaybeNull]
        public bool Sex { get; set; }
        [MaybeNull]
        public string Organisation { get; set; }
        [MaybeNull]
        public string JobTitle { get; set; }
        [MaybeNull]
        public List<Certificate> Certificates { get; set; }
        [MaybeNull]
        public List<Course> BoughtCourses { get; set; }
        [MaybeNull]
        public List<Course> CoursesInAction { get; set; }
        [MaybeNull]
        public List<Course> SolvedCourses { get; set; }//Курсы с пройденным тестированием (запретить тест)
        [MaybeNull]
        public List<long> SolvedLessons { get; set; }
        [MaybeNull]
        public string AboutMe { get; set; }//Сделать ограничение

        public User()
        {

        }
        public User(string login, string password)
        {
            Login = login;
            PasswordHash = password;
        }
        public User( string name, DateTime createdAt, DateTime modifiedAt, string lastName, string surName, string login, string password, string photoPath, bool sex, string email, string organisation, string jobTitle, List<Certificate> certificates, List<Course> boughtCourses,List<Course> coursesInAction, List<Course> solvedCourses, List<Lesson> solvedLessons, string aboutMe, bool deleted=false)
        {
            Name = name;
            CreatedAt = createdAt;
            ModifiedAt = modifiedAt;
            LastName = lastName;
            SurName = surName;
            Login = login;
            PasswordHash = password;
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