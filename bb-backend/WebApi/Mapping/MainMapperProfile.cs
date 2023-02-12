using AutoMapper;
using BLL.Models.GetCourseForLearning;
using BLL.Models.GetUserForAccount;
using Infrastructure.Models;
using BLL.Models.CertificateOut;
using BLL.Models.CreateCertificate;
using BLL.Models.GetCoursesForCart;
using BLL.Models.GetExamForTesting;
using BLL.Models.SaveCourseEdit;

namespace WebApi.Mapping;

public class MainMapperProfile : Profile
{
    public MainMapperProfile()
    {
        // SaveCourse - IN
        CreateMap<SaveAnswerOptionEditDto, AnswerOption>();
        CreateMap<SaveCourseEditDto, Course>();
        CreateMap<SaveExamEditDto, Exam>();
        CreateMap<SaveLessonEditDto, Course>();
        CreateMap<SaveQuestionEditDto, Course>();

        CreateMap<GetUserShortForAccountDto , User>();
        CreateMap<CourseForCartDto, Course>();
        
        CreateMap<CertificateOutDto,Certificate>();
        CreateMap<CreateCertificateDto,Certificate>();


        // GetForLearning - OUT
        CreateMap<AnswerOption, GetAnswerOptionForLearningDto>();
        CreateMap<Question, GetQuestionForLearningDto>();
        CreateMap<Lesson, GetLessonForLearningDto>();
        CreateMap<Course, GetCourseForLearningDto>();
        CreateMap<Exam, GetExamForLearningDto>();
        CreateMap<User, GetUserShortForAccountDto>();
    }
}