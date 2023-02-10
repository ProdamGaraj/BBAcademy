using AutoMapper;
using BLL.Models.CourseForCart;
using BLL.Models.GetCourseForView;
using BLL.Models.GetUserForAccount;
using BLL.Models.Save;
using Infrastructure.Models;
using BLL.Models.CertificateOut;
using BLL.Models.CreateCertificate;
using BLL.Models.Exam;

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
        CreateMap<GetUserShortForAccountDto , User>();
        CreateMap<CourseForCartDto, Course>();
        
        CreateMap<CertificateOutDto,Certificate>();
        CreateMap<CreateCertificateDto,Certificate>();
        CreateMap<ExamDto, Exam>();
        CreateMap<AnswerOptionDto, AnswerOption>();
        
        
        

        // OUT
        CreateMap<AnswerOption, GetAnswerOptionForViewDto>();
        CreateMap<Course, GetCourseForViewDto>();
        CreateMap<User, GetUserShortForAccountDto>();
        CreateMap<Exam, GetExamForViewDto>();
        CreateMap<Course, GetLessonForViewDto>();
        CreateMap<Course, GetQuestionForViewDto>();
        CreateMap<Course, CourseForCartDto>();
        
        CreateMap<Certificate, CertificateOutDto>();
        CreateMap<Certificate, CreateCertificateDto>();
        CreateMap<Exam, ExamDto>();
        CreateMap<AnswerOption, AnswerOptionDto>();

        
    }
}