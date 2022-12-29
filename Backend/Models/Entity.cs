using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    public abstract class Entity
    {
        [Key]
        public long Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt{ get; set; }
        public DateTime ModifiedAt { get; set;}
        public bool Deleted { get; set; }
    }
}
