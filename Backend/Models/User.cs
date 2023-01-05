using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using Backend.Models.Enum;

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
        public List<UserToCertificate> Certificates { get; set; }
        [MaybeNull]
        public List<UserToCourse> Courses { get; set; }
        [MaybeNull]
        public List<UserToLesson> SolvedLessons { get; set; }
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
        public User( string name, DateTime createdAt, DateTime modifiedAt, string lastName, string surName, string login, string password, string photoPath, bool sex, string email, string organisation, string jobTitle, List<UserToCertificate> certificates, List<UserToCourse> courses, List<UserToLesson> solvedLessons, string aboutMe, bool deleted=false)
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
            Courses = courses;
            SolvedLessons = solvedLessons;
            AboutMe = aboutMe;
            Deleted = deleted;
        }
    }
}