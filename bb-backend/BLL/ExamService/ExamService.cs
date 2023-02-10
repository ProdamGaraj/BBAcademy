using System.Collections;
using AutoMapper;
using BLL.Models.CourseForCart;
using BLL.Models.CourseForExamView;
using BLL.Models.Exam;
using Infrastructure.Models;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BLL.ExamService;

public class ExamService : IExamService
{
    private readonly IRepository<User> _userRepository;
    private readonly IRepository<Course> _courseRepository;
    private readonly IRepository<Question> _questionRepository;
    private readonly IRepository<Exam> _examRepository;
    private readonly ILogger<ExamService> _logger;
    private readonly IMapper _mapper;
    private readonly IRepository<AnswerOption> _answerOptionsRepository;

    public ExamService(IRepository<User> userRepository, IRepository<Course> courseRepository,
        IRepository<Question> questionRepository, IRepository<Exam> examRepository, IMapper mapper,
        ILogger<ExamService> logger, IRepository<AnswerOption> answerOptionsRepository)
    {
        _userRepository = userRepository;
        _courseRepository = courseRepository;
        _questionRepository = questionRepository;
        _examRepository = examRepository;
        _mapper = mapper;
        _logger = logger;
        _answerOptionsRepository = answerOptionsRepository;
    }

    public async Task<bool> SolveExam(ExamDto dto)
    {
        return true;
    }

    public async Task<CourseForExamViewDto> GetByCourse(long courseId)
    {
        try
        {
            var exam = _mapper.Map<ExamDto>(_courseRepository.GetAll().FirstOrDefault(c => c.Id == courseId).Exam);
            var data = new CourseForExamViewDto()
            {
                CourseID = courseId,
                Exam = exam
            };
            return data;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
        throw new NotImplementedException();
    }
}