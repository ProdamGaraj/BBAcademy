using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Models
{
    public abstract class Entity
    {
        public long Id { get; set; }
        public DateTime CreatedAt{ get; set; }
        public DateTime ModifiedAt { get; set;}
        public bool Deleted { get; set; }
    }
}
