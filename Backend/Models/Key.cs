namespace Backend.Models
{
    public class Key:Entity
    {
        public Profession Profession { get; set; }
        public string Data { get; set; }
        public bool Available { get; set; }//
        public DateTime EndDate { get; set; }//Users?????

        public Key(Profession profession, string data, bool available, DateTime endDate)
        {
            Profession = profession;

            Data = data;
            Available = available;
            EndDate = endDate;
        }
    }
}