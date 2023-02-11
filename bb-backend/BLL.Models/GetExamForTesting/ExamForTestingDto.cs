namespace BLL.Models.GetExamForTesting;

public class ExamForTestingDto
{
    public string Title { get; set; }

    public ICollection<QuestionForTestingDto> Questions { get; set; }
}