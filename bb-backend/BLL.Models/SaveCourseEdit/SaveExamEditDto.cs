namespace BLL.Models.SaveCourseEdit;

public class SaveExamEditDto
{
    public string Description { get; set; }

    public int MinimumPassingGrade { get; set; }

    public ICollection<SaveQuestionEditDto> Questions { get; set; }
}