namespace BLL.Models.Save;

public class SaveAnswerOptionDto
{
    public string Title { get; set; }

    public bool IsCorrect { get; set; }

    public int Weight { get; set; }
}