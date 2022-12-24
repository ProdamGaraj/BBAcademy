namespace Backend.Models
{
    public class Organisation : Entity
    {
        public List<Key> Keys { get; set; }
        public Organisation(List<Key> keys, string name)
        {
            Keys = keys;
            Name = name;
        }
    }
}