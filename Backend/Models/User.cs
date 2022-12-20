namespace Backend.Models
{
    public class User : Entity
    {

        public string LastName { get; set; }
        public string SurName { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string JobTitle { get; set; }
        public UserType Status { get; set; }
        public List<Certificate> Certificates { get; set; }
        public List<Course> Courses { get; set; }
        public string AboutMe { get; set; }//Сделать ограничение
    }
}
