namespace BLL.Models.SaveCourseEdit;

public class SaveCourseEditDto
{
    public string MediaPath { get; set; }

    public float DurationHours { get; set; }

    public string Description { get; set; }

    public decimal Price { get; set; }

    public SaveExamEditDto ExamEdit { get; set; }

    public ICollection<SaveLessonEditDto> Lessons { get; set; }
}