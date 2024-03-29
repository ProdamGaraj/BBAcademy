﻿namespace BLL.Models.GetCourseForLearning;

public class GetCourseForLearningDto
{
    public long Id { get; set; }
    public string MediaPath { get; set; }

    public float DurationHours { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public decimal Price { get; set; }

    public GetExamForLearningDto Exam { get; set; }

    public ICollection<GetLessonForLearningDto> Lessons { get; set; }
    public string CertName { get; set; }
}