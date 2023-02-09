﻿using AutoMapper;
using BLL.Models.GetCourseForView;
using BLL.Models.Save;
using Infrastructure.Models;

namespace WebApi.Mapping;

public class MainMapperProfile : Profile
{
    public MainMapperProfile()
    {
        // IN
        CreateMap<SaveAnswerOptionDto, AnswerOption>();
        CreateMap<SaveCourseDto, Course>();
        CreateMap<SaveExamDto, Exam>();
        CreateMap<SaveLessonDto, Course>();
        CreateMap<SaveQuestionDto, Course>();

        // OUT
        CreateMap<AnswerOption, GetAnswerOptionForViewDto>();
        CreateMap<Course, GetCourseForViewDto>();
        CreateMap<Exam, GetExamForViewDto>();
        CreateMap<Course, GetLessonForViewDto>();
        CreateMap<Course, GetQuestionForViewDto>();
    }
}