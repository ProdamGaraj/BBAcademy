namespace Backend.Models
{
    public class User : Entity
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string SurName { get; set; }
        public string PhotoPath { get; set; }//
        public bool Sex { get; set; }
        public string Email { get; set; }//
        public Key Key { get; set; }
        public string JobTitle { get; set; }
        public List<Certificate> Certificates { get; set; }
        public List<Course> CoursesInAction { get; set; }
        public List<Course> SolvedCourses { get; set; }
        public List<Lesson> SolvedLessons { get; set; }
        public string AboutMe { get; set; }//Сделать ограничение
        public User(string name, string lastName, string surName, string photoPath, bool sex, string email, Key key, string jobTitle, List<Certificate> certificates, List<Course> coursesInAction, List<Course> solvedCourses, List<Lesson> solvedLessons, string aboutMe)
        {
            Name = name;
            LastName = lastName;
            SurName = surName;
            PhotoPath = photoPath;
            Sex = sex;
            Email = email;
            Key = key;
            JobTitle = jobTitle;
            Certificates = certificates;
            CoursesInAction = coursesInAction;
            SolvedCourses = solvedCourses;
            SolvedLessons = solvedLessons;
            AboutMe = aboutMe;
        }
    }
}
