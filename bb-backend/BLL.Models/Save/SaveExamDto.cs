namespace BLL.Models.Save;

public class SaveExamDto
{
    public string Description { get; set; }

    public int MinimumPassingGrade { get; set; }

    public ICollection<SaveQuestionDto> Questions { get; set; }
}