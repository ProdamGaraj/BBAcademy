using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    public abstract class Entity
    {
        [Key]
        public long ID { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt{ get; set; }
        public DateTime ChangedAt { get; set;}
    }
}
