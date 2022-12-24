namespace Backend.Models
{
    public class Lesson:Entity
    {
        public enum type { get; set; }
        public string Description { get; set; }
        public string TextContent { get; set; }//?ТЫ ГОВОРИШЬ НЕ ПРО МОДЕЛИ!!!!!!!!!
        public string MediaContentPath { get; set; }
    }
}