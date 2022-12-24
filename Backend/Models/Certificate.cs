namespace Backend.Models
{
    public class Certificate : Entity
    {
        public string UserName { get; set; }
        public string UserSurname { get; set; }
        public string UserLastname { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public string Content { get; set; }
        public string ImageTemplatePath { get; set; }
        public Certificate(string userName, string userSurname, string userLastname, int userId, User user, string description, string imageTemplatePath)
        {
            UserName = userName;
            UserSurname = userSurname;
            UserLastname = userLastname;
            UserId = userId;
            User = user;
            Description = description;
            ImageTemplatePath = imageTemplatePath;
        }
    }
}