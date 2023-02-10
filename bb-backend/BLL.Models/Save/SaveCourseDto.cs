﻿namespace BLL.Models.Save;

public class SaveCourseDto
{
    public string MediaPath { get; set; }

    public float DurationHours { get; set; }

    public string Description { get; set; }

    public decimal Price { get; set; }

    public SaveExamDto Exam { get; set; }

    public ICollection<SaveLessonDto> Lessons { get; set; }
}