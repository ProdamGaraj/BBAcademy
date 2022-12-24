namespace Backend.Models
{
    public class User : Entity
    {
        public string Name { get; set; }
        public string PhotoPath { get; set; }//
        public string LastName { get; set; }
        public string SurName { get; set; }
        public bool Sex { get; set; }
        public string Email { get; set; }//
        public Key Key { get; set; }
        public string JobTitle { get; set; }
        public List<Certificate> Certificates { get; set; }
        public Profession Profession { get; set; }
        public List<Course> CoursesInAction { get; set; }
        public List<Course> SolvedCourses { get; set; }
        public List<Lesson> SolvedLessons { get; set; }
        public string AboutMe { get; set; }//Сделать ограничение
        public User(string photoPath, string lastName, string surName, bool sex, string email, Key key, string jobTitle, List<Certificate> certificates, Profession profession, List<Course> coursesInAction, List<Course> solvedCourses, List<Lesson> solvedLessons, string aboutMe)
        {
            PhotoPath = photoPath;
            LastName = lastName;
            SurName = surName;
            Sex = sex;
            Email = email;
            Key = key;
            JobTitle = jobTitle;
            Certificates = certificates;
            Profession = profession;
            CoursesInAction = coursesInAction;
            SolvedCourses = solvedCourses;
            SolvedLessons = solvedLessons;
            AboutMe = aboutMe;
        }
    }
}
