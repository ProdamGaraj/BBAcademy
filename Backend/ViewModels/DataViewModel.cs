using Backend.Models;

namespace Backend.ViewModels
{
    public class DataViewModel
    {
        public Course Course { get; set; }
        public Certificate Certificate { get; set; }
        public string CurrentStruct { get; set; }
        public int CurrentQuestion { get; set; }
    }
}
