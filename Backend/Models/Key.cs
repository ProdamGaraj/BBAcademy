namespace Backend.Models
{
    public class Key:Entity
    {
        public Profession Profession { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public string Data { get; set; }
        public bool Available { get; set; }//
        public DateTime EndDate { get; set; }//

        public Key(Profession profession, int userId, User user, string data, bool available, DateTime endDate)
        {
            Profession = profession;
            UserId = userId;
            User = user;
            Data = data;
            Available = available;
            EndDate = endDate;
        }
    }
}